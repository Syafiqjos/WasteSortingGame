using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDatabaseLoad(Dictionary<string, object> data);
public delegate void OnDatabaseSave(Dictionary<string, object> data);
public delegate void OnDatabaseFail();

public interface IDatabase
{
    void Initialize();
    bool IsInitialized();

    void LoadData(string collection, string key, OnDatabaseLoad onDatabaseLoad = null, OnDatabaseFail onDatabaseFail = null);
    void SaveData(string collection, string key, Dictionary<string, object> data, OnDatabaseSave onDatabaseSave = null, OnDatabaseFail onDatabaseFail = null);
}

public class DatabaseManager : MonoBehaviour
{
    IDatabase database;

    private void Awake()
    {
        database = new FirebaseManager();
        database.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        if (database != null)
        {
            Debug.Log("Try To Load Data from Database");
            database.LoadData("saveData", GetUserUniqueIdentifier(), LoadDataHook);
        }
    }

    private void LoadDataHook(Dictionary<string, object> data)
    {
        if (data != null)
        {
            if (data.ContainsKey("levels"))
            {
                LoadLevelsData(data["levels"].ToString());
                Debug.Log(data["levels"].ToString());
            }

            if (data.ContainsKey("scores"))
            {
                LoadScoresData(data["scores"].ToString());
                Debug.Log(data["scores"].ToString());
            }

            if (data.ContainsKey("achievements"))
            {
                LoadAchievementsData(data["achievements"].ToString());
                Debug.Log(data["achievements"].ToString());
            }
        }
    }

    private void LoadLevelsData(string json)
    {
        DatabaseLevelsData.LoadJSONToLocal(json);
    }

    private void LoadScoresData(string json)
    {
        DatabaseScoresData.LoadJSONToLocal(json);
    }

    private void LoadAchievementsData(string json)
    {
        DatabaseAchievementsData.LoadJSONToLocal(json);
    }

    public void SaveData()
    {
        if (database != null)
        {
            Debug.Log("Try To Save Data to Database");
            database.SaveData("saveData", GetUserUniqueIdentifier(), GetSavedData(), SaveDataHook);
        }
    }

    private void SaveDataHook(Dictionary<string, object> data)
    {

    }

    private string GetUserUniqueIdentifier()
    {
        return "U_" + SystemInfo.deviceUniqueIdentifier;
    }

    private Dictionary<string, object> GetSavedData()
    {
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            ["levels"] = DatabaseLevelsData.GetJSON(),
            ["scores"] = DatabaseScoresData.GetJSON(),
            ["achievements"] = DatabaseAchievementsData.GetJSON()
        };

        return data;
    }
}

[System.Serializable]
public class DatabaseLevelsData
{
    public int level1;
    public int level2;
    public int level3;
    public int level4;
    public int level5;
    public int level6;
    public int level7;
    public int level8;
    public int level9;
    public int level10;

    public static string GetJSON()
    {
        DatabaseLevelsData data = new DatabaseLevelsData
        {
            level1 = GetLevelStatus("Level1"),
            level2 = GetLevelStatus("Level2"),
            level3 = GetLevelStatus("Level3"),
            level4 = GetLevelStatus("Level4"),
            level5 = GetLevelStatus("Level5"),
            level6 = GetLevelStatus("Level6"),
            level7 = GetLevelStatus("Level7"),
            level8 = GetLevelStatus("Level8"),
            level9 = GetLevelStatus("Level9"),
            level10 = GetLevelStatus("Level10")
        };

        return JsonUtility.ToJson(data);
    }

    public static void LoadJSONToLocal(string json)
    {
        DatabaseLevelsData data = JsonUtility.FromJson<DatabaseLevelsData>(json);

        PlayerPrefs.SetInt("Level1", data.level1);
        PlayerPrefs.SetInt("Level2", data.level2);
        PlayerPrefs.SetInt("Level3", data.level3);
        PlayerPrefs.SetInt("Level4", data.level4);
        PlayerPrefs.SetInt("Level5", data.level5);
        PlayerPrefs.SetInt("Level6", data.level6);
        PlayerPrefs.SetInt("Level7", data.level7);
        PlayerPrefs.SetInt("Level8", data.level8);
        PlayerPrefs.SetInt("Level9", data.level9);
        PlayerPrefs.SetInt("Level10", data.level10);
    }

    public static int GetLevelStatus(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }

        return 0;
    }
}

[System.Serializable]
public class DatabaseScoresData
{
    public int level1;
    public int level2;
    public int level3;
    public int level4;
    public int level5;
    public int level6;
    public int level7;
    public int level8;
    public int level9;
    public int level10;

    public static string GetJSON()
    {
        DatabaseScoresData data = new DatabaseScoresData
        {
            level1 = GetLevelScore("Level1_score"),
            level2 = GetLevelScore("Level2_score"),
            level3 = GetLevelScore("Level3_score"),
            level4 = GetLevelScore("Level4_score"),
            level5 = GetLevelScore("Level5_score"),
            level6 = GetLevelScore("Level6_score"),
            level7 = GetLevelScore("Level7_score"),
            level8 = GetLevelScore("Level8_score"),
            level9 = GetLevelScore("Level9_score"),
            level10 = GetLevelScore("Level10_score")
        };

        return JsonUtility.ToJson(data);
    }

    public static void LoadJSONToLocal(string json)
    {
        DatabaseScoresData data = JsonUtility.FromJson<DatabaseScoresData>(json);

        PlayerPrefs.SetInt("Level1_score", data.level1);
        PlayerPrefs.SetInt("Level2_score", data.level2);
        PlayerPrefs.SetInt("Level3_score", data.level3);
        PlayerPrefs.SetInt("Level4_score", data.level4);
        PlayerPrefs.SetInt("Level5_score", data.level5);
        PlayerPrefs.SetInt("Level6_score", data.level6);
        PlayerPrefs.SetInt("Level7_score", data.level7);
        PlayerPrefs.SetInt("Level8_score", data.level8);
        PlayerPrefs.SetInt("Level9_score", data.level9);
        PlayerPrefs.SetInt("Level10_score", data.level10);
    }

    public static int GetLevelScore(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetInt(key);
        }

        return 0;
    }
}

[System.Serializable]
public class DatabaseAchievementsData
{
    public static string GetJSON()
    {
        return GetAchievementsData("achievement");
    }

    public static void LoadJSONToLocal(string json)
    {
        PlayerPrefs.SetString("achievement", json);
    }

    public static string GetAchievementsData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }

        return null;
    }
}
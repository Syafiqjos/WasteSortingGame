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

    // level unlock
    // achievement
    // score

    public void LoadData()
    {
        if (database != null)
        {
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
            }

            if (data.ContainsKey("scores"))
            {
                LoadScoresData(data["scores"].ToString());
            }

            if (data.ContainsKey("achievements"))
            {
                LoadAchievementsData(data["achievements"].ToString());
            }
        }
    }

    private void LoadLevelsData(string json)
    {
        // bikin class buat dapetin data
    }

    private void LoadScoresData(string json)
    {
        // bikin class buat dapetin data
    }

    private void LoadAchievementsData(string json)
    {
        // bikin class buat dapetin data
    }

    public void SaveData()
    {
        if (database != null)
        {
            database.SaveData("saveData", GetUserUniqueIdentifier(), GetSavedData(), SaveDataHook);
        }
    }

    private void SaveDataHook(Dictionary<string, object> data)
    {

    }

    private string GetUserUniqueIdentifier()
    {
        return "U_" + Application.identifier;
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

    public static string GetJSON()
    {
        DatabaseLevelsData data = new DatabaseLevelsData
        {
            level1 = GetLevelStatus("level1"),
            level2 = GetLevelStatus("level2"),
            level3 = GetLevelStatus("level3"),
            level4 = GetLevelStatus("level4"),
            level5 = GetLevelStatus("level5"),
            level6 = GetLevelStatus("level6")
        };

        return JsonUtility.ToJson(data);
    }

    public static void LoadJSONToLocal(string json)
    {
        DatabaseLevelsData data = JsonUtility.FromJson<DatabaseLevelsData>(json);

        if (data.level1 == 1) PlayerPrefs.SetInt("level1", 1);
        if (data.level2 == 1) PlayerPrefs.SetInt("level2", 1);
        if (data.level3 == 1) PlayerPrefs.SetInt("level3", 1);
        if (data.level4 == 1) PlayerPrefs.SetInt("level4", 1);
        if (data.level5 == 1) PlayerPrefs.SetInt("level5", 1);
        if (data.level6 == 1) PlayerPrefs.SetInt("level6", 1);
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

    public static string GetJSON()
    {
        DatabaseLevelsData data = new DatabaseLevelsData
        {
            level1 = GetLevelScore("level1_score"),
            level2 = GetLevelScore("level2_score"),
            level3 = GetLevelScore("level3_score"),
            level4 = GetLevelScore("level4_score"),
            level5 = GetLevelScore("level5_score"),
            level6 = GetLevelScore("level6_score")
        };

        return JsonUtility.ToJson(data);
    }

    public static void LoadJSONToLocal(string json)
    {
        DatabaseLevelsData data = JsonUtility.FromJson<DatabaseLevelsData>(json);

        if (data.level1 == 1) PlayerPrefs.SetInt("level1_score", 1);
        if (data.level2 == 1) PlayerPrefs.SetInt("level2_score", 1);
        if (data.level3 == 1) PlayerPrefs.SetInt("level3_score", 1);
        if (data.level4 == 1) PlayerPrefs.SetInt("level4_score", 1);
        if (data.level5 == 1) PlayerPrefs.SetInt("level5_score", 1);
        if (data.level6 == 1) PlayerPrefs.SetInt("level6_score", 1);
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
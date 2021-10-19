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
        return null;
    }
}
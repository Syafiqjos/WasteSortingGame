using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AchievementData
{
    public string title;
    public string description;
    public int value;
    public int max;

    public AchievementData(string title, string description, int max)
    {
        this.title = title;
        this.description = description;
        this.max = max;

        value = 0;
    }

    public bool Unlocked()
    {
        return value >= max;
    }
}

[System.Serializable]
public class AchievementCountingTrashData : AchievementData
{
    private TrashType trashType;
    private int trashID;

    public AchievementCountingTrashData(string title, string description, int max, TrashType trashType, int trashID = -1) : base(title, description, max)
    {
        this.trashType = trashType;
        this.trashID = trashID;
    }

    private bool CheckTriggerValid(Trash trash)
    {
        if (trashType == TrashType.Global)
        {
            return true;
        }
        else if (trashID != -1)
        {
            return trashID == trash.trashID;
        }
        else
        {
            return trash.trashType == trashType;
        }
    }

    public void SubscribeTrigger(Trash trash)
    {
        if (value + 1 <= max)
        {
            if (CheckTriggerValid(trash))
            {
                value++;
            }
        }
    }
}

[System.Serializable]
public class AchievementConfig
{
    public Dictionary<string, AchievementData> achievements = new Dictionary<string, AchievementData>();

    public bool ContainsAchievement(string key)
    {
        return achievements.ContainsKey(key);
    }

    public AchievementData GetAchievement(string key)
    {
        if (ContainsAchievement(key))
        {
            return achievements[key];
        }
        return null;
    }

    public void SetAchievement(string key, int value)
    {
        if (ContainsAchievement(key))
        {
            achievements[key].value = value;
        }
    }

    public void AddAchievement(string key, AchievementData data)
    {
        achievements[key] = data;
    }
}

public class AchievementManager : MonoBehaviour
{
    private const string PlayerPrefsKey = "achievement";

    public static AchievementManager Instance { get; private set; }

    public AchievementConfig achievementConfig;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;   
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CreateAchievements();
        LoadAchievement();
    }

    private void CreateAchievements()
    {
        achievementConfig = new AchievementConfig();

        achievementConfig.AddAchievement("all_trash_count", new AchievementCountingTrashData("All Trash Count", "Get a total of 200 trash", 200, TrashType.Global));

        achievementConfig.AddAchievement("green_trash_count", new AchievementCountingTrashData("Green Trash Count", "Get a total of 60 Green bin trash", 60, TrashType.Green));
        achievementConfig.AddAchievement("yellow_trash_count", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of 60 Yellow bin trash", 60, TrashType.Yellow));
        achievementConfig.AddAchievement("red_trash_count", new AchievementCountingTrashData("Red Trash Count", "Get a total of 60 Red bin trash", 60, TrashType.Red));
    }

    public void SaveAchievement()
    {
        PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(achievementConfig));
        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            SaveAchievement();
        } else
        {
            string json = PlayerPrefs.GetString(PlayerPrefsKey);
            AchievementConfig config = JsonUtility.FromJson<AchievementConfig>(json);

            foreach (KeyValuePair<string, AchievementData> pair in achievementConfig.achievements)
            {
                if (config.achievements != null && config.ContainsAchievement(pair.Key))
                {
                    achievementConfig.SetAchievement(pair.Key, config.GetAchievement(pair.Key).value);
                }
            }
        }
    }
}
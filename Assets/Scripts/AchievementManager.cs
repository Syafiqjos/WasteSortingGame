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
    public TrashType trashType;
    public int trashID;

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

        achievementConfig.AddAchievement("green_0_trash_count", new AchievementCountingTrashData("Green 0 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 0));
        achievementConfig.AddAchievement("green_1_trash_count", new AchievementCountingTrashData("Green 1 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 1));
        achievementConfig.AddAchievement("green_2_trash_count", new AchievementCountingTrashData("Green 2 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 2));
        achievementConfig.AddAchievement("green_3_trash_count", new AchievementCountingTrashData("Green 3 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 3));
        achievementConfig.AddAchievement("green_4_trash_count", new AchievementCountingTrashData("Green 4 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 4));
        achievementConfig.AddAchievement("green_5_trash_count", new AchievementCountingTrashData("Green 5 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 5));
        achievementConfig.AddAchievement("green_6_trash_count", new AchievementCountingTrashData("Green 6 Trash Count", "Get a total of ${value}/10 Green trash", 10, TrashType.Green, 6));

        achievementConfig.AddAchievement("yellow_0_trash_count", new AchievementCountingTrashData("Yellow 0 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 0));
        achievementConfig.AddAchievement("yellow_1_trash_count", new AchievementCountingTrashData("Yellow 1 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 1));
        achievementConfig.AddAchievement("yellow_2_trash_count", new AchievementCountingTrashData("Yellow 2 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 2));
        achievementConfig.AddAchievement("yellow_3_trash_count", new AchievementCountingTrashData("Yellow 3 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 3));
        achievementConfig.AddAchievement("yellow_4_trash_count", new AchievementCountingTrashData("Yellow 4 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 4));
        achievementConfig.AddAchievement("yellow_5_trash_count", new AchievementCountingTrashData("Yellow 5 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 5));
        achievementConfig.AddAchievement("yellow_6_trash_count", new AchievementCountingTrashData("Yellow 6 Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, TrashType.Yellow, 6));

        achievementConfig.AddAchievement("red_0_trash_count", new AchievementCountingTrashData("Red 0 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 0));
        achievementConfig.AddAchievement("red_1_trash_count", new AchievementCountingTrashData("Red 1 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 1));
        achievementConfig.AddAchievement("red_2_trash_count", new AchievementCountingTrashData("Red 2 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 2));
        achievementConfig.AddAchievement("red_3_trash_count", new AchievementCountingTrashData("Red 3 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 3));
        achievementConfig.AddAchievement("red_4_trash_count", new AchievementCountingTrashData("Red 4 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 4));
        achievementConfig.AddAchievement("red_5_trash_count", new AchievementCountingTrashData("Red 5 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 5));
        achievementConfig.AddAchievement("red_6_trash_count", new AchievementCountingTrashData("Red 6 Trash Count", "Get a total of ${value}/10 Red bin trash", 10, TrashType.Red, 6));

        achievementConfig.AddAchievement("green_trash_count_10", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/60 Green bin trash", 10, TrashType.Green));
        achievementConfig.AddAchievement("green_trash_count_30", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/60 Green bin trash", 30, TrashType.Green));
        achievementConfig.AddAchievement("green_trash_count_60", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/60 Green bin trash", 60, TrashType.Green));

        achievementConfig.AddAchievement("yellow_trash_count_10", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/60 Yellow bin trash", 10, TrashType.Yellow));
        achievementConfig.AddAchievement("yellow_trash_count_30", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/60 Yellow bin trash", 30, TrashType.Yellow));
        achievementConfig.AddAchievement("yellow_trash_count_60", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/60 Yellow bin trash", 60, TrashType.Yellow));

        achievementConfig.AddAchievement("red_trash_count_10", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/60 Red bin trash", 10, TrashType.Red));
        achievementConfig.AddAchievement("red_trash_count_30", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/60 Red bin trash", 30, TrashType.Red));
        achievementConfig.AddAchievement("red_trash_count_60", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/60 Red bin trash", 60, TrashType.Red));

        achievementConfig.AddAchievement("all_trash_count_50", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/50 trash", 50, TrashType.Global));
        achievementConfig.AddAchievement("all_trash_count_100", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/100 trash", 100, TrashType.Global));
        achievementConfig.AddAchievement("all_trash_count_200", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/200 trash", 200, TrashType.Global));
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
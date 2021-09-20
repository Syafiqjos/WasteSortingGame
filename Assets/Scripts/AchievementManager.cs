using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool trashTypeGlobal = false;
    public TrashType trashType;
    public int trashID;

    public AchievementCountingTrashData(string title, string description, int max, bool trashTypeGlobal, TrashType trashType = TrashType.Green, int trashID = -1) : base(title, description, max)
    {
        this.trashTypeGlobal = trashTypeGlobal;
        this.trashType = trashType;
        this.trashID = trashID;
    }

    private bool CheckTriggerValid(Trash trash)
    {
        if (trashTypeGlobal)
        {
            return true;
        }
        else if (trashID != -1)
        {
            return trash.trashType == trashType && trashID == trash.trashID;
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
                Debug.Log(title);
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
        achievements[key].value = value;
    }

    public void AddAchievement(string key, AchievementData data)
    {
        achievements[key] = data;
    }

    public AchievementData green_0_trash_count;
    public AchievementData green_1_trash_count;
    public AchievementData green_2_trash_count;
    public AchievementData green_3_trash_count;
    public AchievementData green_4_trash_count;
    public AchievementData green_5_trash_count;
    public AchievementData green_6_trash_count;

    public AchievementData yellow_0_trash_count;
    public AchievementData yellow_1_trash_count;
    public AchievementData yellow_2_trash_count;
    public AchievementData yellow_3_trash_count;
    public AchievementData yellow_4_trash_count;
    public AchievementData yellow_5_trash_count;
    public AchievementData yellow_6_trash_count;

    public AchievementData red_0_trash_count;
    public AchievementData red_1_trash_count;
    public AchievementData red_2_trash_count;
    public AchievementData red_3_trash_count;
    public AchievementData red_4_trash_count;
    public AchievementData red_5_trash_count;
    public AchievementData red_6_trash_count;

    public AchievementData green_trash_count_10;
    public AchievementData green_trash_count_30;
    public AchievementData green_trash_count_60;

    public AchievementData yellow_trash_count_10;
    public AchievementData yellow_trash_count_30;
    public AchievementData yellow_trash_count_60;

    public AchievementData red_trash_count_10;
    public AchievementData red_trash_count_30;
    public AchievementData red_trash_count_60;

    public AchievementData all_trash_count_50;
    public AchievementData all_trash_count_100;
    public AchievementData all_trash_count_200;

    public void SaveAchievement()
    {
        green_0_trash_count = GetAchievement("green_0_trash_count");
        green_1_trash_count = GetAchievement("green_1_trash_count");
        green_2_trash_count = GetAchievement("green_2_trash_count");
        green_3_trash_count = GetAchievement("green_3_trash_count");
        green_4_trash_count = GetAchievement("green_4_trash_count");
        green_5_trash_count = GetAchievement("green_5_trash_count");
        green_6_trash_count = GetAchievement("green_6_trash_count");

        yellow_0_trash_count = GetAchievement("yellow_0_trash_count");
        yellow_1_trash_count = GetAchievement("yellow_1_trash_count");
        yellow_2_trash_count = GetAchievement("yellow_2_trash_count");
        yellow_3_trash_count = GetAchievement("yellow_3_trash_count");
        yellow_4_trash_count = GetAchievement("yellow_4_trash_count");
        yellow_5_trash_count = GetAchievement("yellow_5_trash_count");
        yellow_6_trash_count = GetAchievement("yellow_6_trash_count");

        red_0_trash_count = GetAchievement("red_0_trash_count");
        red_1_trash_count = GetAchievement("red_1_trash_count");
        red_2_trash_count = GetAchievement("red_2_trash_count");
        red_3_trash_count = GetAchievement("red_3_trash_count");
        red_4_trash_count = GetAchievement("red_4_trash_count");
        red_5_trash_count = GetAchievement("red_5_trash_count");
        red_6_trash_count = GetAchievement("red_6_trash_count");

        green_trash_count_10 = GetAchievement("green_trash_count_10");
        green_trash_count_30 = GetAchievement("green_trash_count_30");
        green_trash_count_60 = GetAchievement("green_trash_count_60");

        yellow_trash_count_10 = GetAchievement("yellow_trash_count_10");
        yellow_trash_count_30 = GetAchievement("yellow_trash_count_30");
        yellow_trash_count_60 = GetAchievement("yellow_trash_count_60");

        red_trash_count_10 = GetAchievement("red_trash_count_10");
        red_trash_count_30 = GetAchievement("red_trash_count_30");
        red_trash_count_60 = GetAchievement("red_trash_count_60");

        all_trash_count_50 = GetAchievement("all_trash_count_50");
        all_trash_count_100 = GetAchievement("all_trash_count_100");
        all_trash_count_200 = GetAchievement("all_trash_count_200");
    }

    public void LoadAchievement(AchievementConfig config)
    {
        SetAchievement("green_0_trash_count", config.green_0_trash_count.value);
        SetAchievement("green_1_trash_count", config.green_1_trash_count.value);
        SetAchievement("green_2_trash_count", config.green_2_trash_count.value);
        SetAchievement("green_3_trash_count", config.green_3_trash_count.value);
        SetAchievement("green_4_trash_count", config.green_4_trash_count.value);
        SetAchievement("green_5_trash_count", config.green_5_trash_count.value);
        SetAchievement("green_6_trash_count", config.green_6_trash_count.value);

        SetAchievement("yellow_0_trash_count", config.yellow_0_trash_count.value);
        SetAchievement("yellow_1_trash_count", config.yellow_1_trash_count.value);
        SetAchievement("yellow_2_trash_count", config.yellow_2_trash_count.value);
        SetAchievement("yellow_3_trash_count", config.yellow_3_trash_count.value);
        SetAchievement("yellow_4_trash_count", config.yellow_4_trash_count.value);
        SetAchievement("yellow_5_trash_count", config.yellow_5_trash_count.value);
        SetAchievement("yellow_6_trash_count", config.yellow_6_trash_count.value);

        SetAchievement("red_0_trash_count", config.red_0_trash_count.value);
        SetAchievement("red_1_trash_count", config.red_1_trash_count.value);
        SetAchievement("red_2_trash_count", config.red_2_trash_count.value);
        SetAchievement("red_3_trash_count", config.red_3_trash_count.value);
        SetAchievement("red_4_trash_count", config.red_4_trash_count.value);
        SetAchievement("red_5_trash_count", config.red_5_trash_count.value);
        SetAchievement("red_6_trash_count", config.red_6_trash_count.value);

        SetAchievement("green_trash_count_10", config.green_trash_count_10.value);
        SetAchievement("green_trash_count_30", config.green_trash_count_30.value);
        SetAchievement("green_trash_count_60", config.green_trash_count_60.value);

        SetAchievement("yellow_trash_count_10", config.yellow_trash_count_10.value);
        SetAchievement("yellow_trash_count_30", config.yellow_trash_count_30.value);
        SetAchievement("yellow_trash_count_60", config.yellow_trash_count_60.value);

        SetAchievement("red_trash_count_10", config.red_trash_count_10.value);
        SetAchievement("red_trash_count_30", config.red_trash_count_30.value);
        SetAchievement("red_trash_count_60", config.red_trash_count_60.value);

        SetAchievement("all_trash_count_50", config.all_trash_count_50.value);
        SetAchievement("all_trash_count_100", config.all_trash_count_100.value);
        SetAchievement("all_trash_count_200", config.all_trash_count_200.value);
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

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        SaveAchievement();
    }

    private void CreateAchievements()
    {
        achievementConfig = new AchievementConfig();

        achievementConfig.AddAchievement("green_0_trash_count", new AchievementCountingTrashData("Green 0 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 0));
        achievementConfig.AddAchievement("green_1_trash_count", new AchievementCountingTrashData("Green 1 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 1));
        achievementConfig.AddAchievement("green_2_trash_count", new AchievementCountingTrashData("Green 2 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 2));
        achievementConfig.AddAchievement("green_3_trash_count", new AchievementCountingTrashData("Green 3 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 3));
        achievementConfig.AddAchievement("green_4_trash_count", new AchievementCountingTrashData("Green 4 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 4));
        achievementConfig.AddAchievement("green_5_trash_count", new AchievementCountingTrashData("Green 5 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 5));
        achievementConfig.AddAchievement("green_6_trash_count", new AchievementCountingTrashData("Green 6 Trash Count", "Get a total of ${value}/10 showed Green trash", 10, false, TrashType.Green, 6));

        achievementConfig.AddAchievement("yellow_0_trash_count", new AchievementCountingTrashData("Yellow 0 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 0));
        achievementConfig.AddAchievement("yellow_1_trash_count", new AchievementCountingTrashData("Yellow 1 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 1));
        achievementConfig.AddAchievement("yellow_2_trash_count", new AchievementCountingTrashData("Yellow 2 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 2));
        achievementConfig.AddAchievement("yellow_3_trash_count", new AchievementCountingTrashData("Yellow 3 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 3));
        achievementConfig.AddAchievement("yellow_4_trash_count", new AchievementCountingTrashData("Yellow 4 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 4));
        achievementConfig.AddAchievement("yellow_5_trash_count", new AchievementCountingTrashData("Yellow 5 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 5));
        achievementConfig.AddAchievement("yellow_6_trash_count", new AchievementCountingTrashData("Yellow 6 Trash Count", "Get a total of ${value}/10 showed Yellow bin trash", 10, false, TrashType.Yellow, 6));

        achievementConfig.AddAchievement("red_0_trash_count", new AchievementCountingTrashData("Red 0 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 0));
        achievementConfig.AddAchievement("red_1_trash_count", new AchievementCountingTrashData("Red 1 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 1));
        achievementConfig.AddAchievement("red_2_trash_count", new AchievementCountingTrashData("Red 2 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 2));
        achievementConfig.AddAchievement("red_3_trash_count", new AchievementCountingTrashData("Red 3 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 3));
        achievementConfig.AddAchievement("red_4_trash_count", new AchievementCountingTrashData("Red 4 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 4));
        achievementConfig.AddAchievement("red_5_trash_count", new AchievementCountingTrashData("Red 5 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 5));
        achievementConfig.AddAchievement("red_6_trash_count", new AchievementCountingTrashData("Red 6 Trash Count", "Get a total of ${value}/10 showed Red bin trash", 10, false, TrashType.Red, 6));

        achievementConfig.AddAchievement("green_trash_count_10", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/10 Green bin trash", 10, false, TrashType.Green));
        achievementConfig.AddAchievement("green_trash_count_30", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/30 Green bin trash", 30, false, TrashType.Green));
        achievementConfig.AddAchievement("green_trash_count_60", new AchievementCountingTrashData("Green Trash Count", "Get a total of ${value}/60 Green bin trash", 60, false, TrashType.Green));

        achievementConfig.AddAchievement("yellow_trash_count_10", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/10 Yellow bin trash", 10, false, TrashType.Yellow));
        achievementConfig.AddAchievement("yellow_trash_count_30", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/30 Yellow bin trash", 30, false, TrashType.Yellow));
        achievementConfig.AddAchievement("yellow_trash_count_60", new AchievementCountingTrashData("Yellow Trash Count", "Get a total of ${value}/60 Yellow bin trash", 60, false, TrashType.Yellow));

        achievementConfig.AddAchievement("red_trash_count_10", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/10 Red bin trash", 10, false, TrashType.Red));
        achievementConfig.AddAchievement("red_trash_count_30", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/30 Red bin trash", 30, false, TrashType.Red));
        achievementConfig.AddAchievement("red_trash_count_60", new AchievementCountingTrashData("Red Trash Count", "Get a total of ${value}/60 Red bin trash", 60, false, TrashType.Red));

        achievementConfig.AddAchievement("all_trash_count_50", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/50 trash", 50, true));
        achievementConfig.AddAchievement("all_trash_count_100", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/100 trash", 100, true));
        achievementConfig.AddAchievement("all_trash_count_200", new AchievementCountingTrashData("All Trash Count", "Get a total of ${value}/200 trash", 200, true));
    }

    public void SaveAchievement()
    {
        Debug.Log("Save Achievement");

        achievementConfig.SaveAchievement();

        PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(achievementConfig));
        PlayerPrefs.Save();

        Debug.Log(JsonUtility.ToJson(achievementConfig));
    }

    public void LoadAchievement()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey))
        {
            SaveAchievement();
        } else
        {
            Debug.Log("Load Achievement");

            string json = PlayerPrefs.GetString(PlayerPrefsKey);
            AchievementConfig config = JsonUtility.FromJson<AchievementConfig>(json);

            Debug.Log(json);

            achievementConfig.LoadAchievement(config);

            /*
            foreach (KeyValuePair<string, AchievementData> pair in achievementConfig.achievements)
            {
                if (config.achievements != null && config.ContainsAchievement(pair.Key))
                {
                    achievementConfig.SetAchievement(pair.Key, config.GetAchievement(pair.Key).value);
                }
            }
            */
        }
    }
}
using UnityEngine;
using System.Collections.Generic;

public class AchievementCountingTrashRegisterer : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        RegisterAchievement();
    }

    private void RegisterAchievement()
    {
        AchievementManager achievementManager = AchievementManager.Instance;

        foreach (KeyValuePair<string, AchievementData> data in achievementManager.achievementConfig.achievements)
        {
            if (data.Value is AchievementCountingTrashData)
            {
                if (gameManager)
                {
                    gameManager.OnRightTrash.AddListener((data.Value as AchievementCountingTrashData).SubscribeTrigger);
                }
            }
        }
    }
}

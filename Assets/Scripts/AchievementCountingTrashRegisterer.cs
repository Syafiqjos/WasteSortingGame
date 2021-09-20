using UnityEngine;
using System.Collections.Generic;

public class AchievementCountingTrashRegisterer : MonoBehaviour
{
    public TrashReceiver trashReceiver;

    private void Start()
    {
        RegisterAchievement();
    }

    private void RegisterAchievement()
    {
        AchievementManager achievementManager = AchievementManager.Instance;

        foreach (KeyValuePair<string, AchievementData> data in achievementManager.achievementConfig.achievements)
        {
            if (data.Value is AchievementCountingTrashData)
            {
                trashReceiver.OnTrashDestroy.AddListener((data.Value as AchievementCountingTrashData).SubscribeTrigger);
            }
        }
    }
}

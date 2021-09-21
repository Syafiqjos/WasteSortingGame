using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Extensione.Audio;

public class AchievementMenu : MonoBehaviour
{
    public GameObject achievementButton;
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI descriptionText;

    public Sprite achievementLocked;
    public Sprite achievementUnlocked;

    [Header("Special Counting Trash")]
    public Image specialTrashPreview;
    public Sprite[] specialTrashbin;
    public Sprite[] specialGreenTrashes;
    public Sprite[] specialYellowTrashes;
    public Sprite[] specialRedTrashes;

    private void Start()
    {
        GenerateAchievementButtons();
    }

    private void GenerateAchievementButtons()
    {
        if (AchievementManager.Instance)
        {
            foreach (var x in AchievementManager.Instance.achievementConfig.achievements)
            {
                GameObject ne = Instantiate(achievementButton);
                ne.transform.SetParent(transform);

                /*
                Button button = ne.GetComponent<Button>();
                if (button)
                {
                    AchievementData data = x.Value;
                    button.onClick.AddListener(() => {
                        RefreshAchievementDetails(button, data);
                    });
                    button.image.sprite = data.Unlocked() ? achievementUnlocked : achievementLocked;

                    if (AudioManager.Instance)
                    {
                        button.onClick.AddListener(AudioManager.Instance.PlayButtonTapSFX);
                    }
                }
                */

                AchievementData data = x.Value;
                AchievementProgress progress = ne.GetComponent<AchievementProgress>();
                if (progress)
                {
                    progress.Initialize(data, GetSpecialCountingTrashAchievementSprite(data));
                }

                ne.transform.localScale = Vector3.one;
            }
        }
    }

    private void RefreshAchievementDetails(Button button, AchievementData data)
    {
        descriptionText.text = data.description.Replace("${value}", data.value.ToString());

        CheckSpecialCountingTrashAchievement(data);
    }

    private Sprite GetSpecialCountingTrashAchievementSprite(AchievementData d)
    {
        if (d is AchievementCountingTrashData)
        {
            AchievementCountingTrashData data = d as AchievementCountingTrashData;

            if (data.trashTypeGlobal)
            {
                return specialTrashbin[3];
            }
            else if (data.trashID != -1)
            {
                if (data.trashType == TrashType.Green)
                {
                    return specialGreenTrashes[data.trashID];
                }
                else if (data.trashType == TrashType.Yellow)
                {
                    return specialYellowTrashes[data.trashID];
                }
                else if (data.trashType == TrashType.Red)
                {
                    return specialRedTrashes[data.trashID];
                }
            }
            else
            {
                if (data.trashType == TrashType.Green)
                {
                    return specialTrashbin[0];
                }
                else if (data.trashType == TrashType.Yellow)
                {
                    return specialTrashbin[1];
                }
                else if (data.trashType == TrashType.Red)
                {
                    return specialTrashbin[2];
                }
            }
        }

        return null;
    }

    private void CheckSpecialCountingTrashAchievement(AchievementData d)
    {
        if (d is AchievementCountingTrashData)
        {
            AchievementCountingTrashData data = d as AchievementCountingTrashData;

            specialTrashPreview.sprite = GetSpecialCountingTrashAchievementSprite(data);
        } else
        {
            specialTrashPreview.sprite = null;
        }
    }
}

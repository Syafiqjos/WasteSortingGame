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
            }
        }
    }

    private void RefreshAchievementDetails(Button button, AchievementData data)
    {
        descriptionText.text = data.description.Replace("${value}", data.value.ToString());

        CheckSpecialCountingTrashAchievement(data);
    }

    private void CheckSpecialCountingTrashAchievement(AchievementData d)
    {
        if (d is AchievementCountingTrashData)
        {
            AchievementCountingTrashData data = d as AchievementCountingTrashData;

            if (data.trashTypeGlobal)
            {
                int x = Random.Range(0, 3);
                if (x == 0)
                {
                    specialTrashPreview.sprite = specialGreenTrashes[Random.Range(0, specialGreenTrashes.Length)];
                }
                else if (x == 1)
                {
                    specialTrashPreview.sprite = specialYellowTrashes[Random.Range(0, specialYellowTrashes.Length)];
                }
                else if (x == 2)
                {
                    specialTrashPreview.sprite = specialRedTrashes[Random.Range(0, specialRedTrashes.Length)];
                }
            }
            else if (data.trashID != -1)
            {
                if (data.trashType == TrashType.Green)
                {
                    specialTrashPreview.sprite = specialGreenTrashes[data.trashID];
                }
                else if (data.trashType == TrashType.Yellow)
                {
                    specialTrashPreview.sprite = specialYellowTrashes[data.trashID];
                }
                else if (data.trashType == TrashType.Red)
                {
                    specialTrashPreview.sprite = specialRedTrashes[data.trashID];
                }
            }
            else
            {
                if (data.trashType == TrashType.Green)
                {
                    specialTrashPreview.sprite = specialGreenTrashes[Random.Range(0, specialGreenTrashes.Length)];
                } else if (data.trashType == TrashType.Yellow)
                {
                    specialTrashPreview.sprite = specialYellowTrashes[Random.Range(0, specialYellowTrashes.Length)];
                } else if (data.trashType == TrashType.Red)
                {
                    specialTrashPreview.sprite = specialRedTrashes[Random.Range(0, specialRedTrashes.Length)];
                }
            }
        } else
        {
            specialTrashPreview.sprite = null;
        }
    }
}

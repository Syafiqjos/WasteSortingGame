using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementMenu : MonoBehaviour
{
    public GameObject achievementButton;
    public TMPro.TextMeshProUGUI titleText;
    public TMPro.TextMeshProUGUI descriptionText;

    public Sprite achievementLocked;
    public Sprite achievementUnlocked;

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
                ne.transform.parent = transform;

                Button button = ne.GetComponent<Button>();
                if (button)
                {
                    AchievementData data = x.Value;
                    button.onClick.AddListener(() => {
                        RefreshAchievementDetails(button, data);
                    });
                    button.image.sprite = data.Unlocked() ? achievementUnlocked : achievementLocked;
                }
            }
        }
    }

    private void RefreshAchievementDetails(Button button, AchievementData data)
    {
        descriptionText.text = data.description;
    }
}

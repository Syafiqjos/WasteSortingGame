using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementProgress : MonoBehaviour
{
    public Image displayImage;
    public Image statusImage;
    public Slider progressSlider;

    public TMPro.TextMeshProUGUI descriptionText;

    public AchievementData achievementData;

    [SerializeField] private Sprite unlockedImage;
    [SerializeField] private Sprite lockedImage;

    public void Initialize(AchievementData data, Sprite sprite)
    {
        achievementData = data;
        descriptionText.text = achievementData.description.Replace("${value}", achievementData.value.ToString());

        displayImage.sprite = sprite;
        statusImage.sprite = achievementData.Unlocked() ? unlockedImage : lockedImage;

        progressSlider.value = achievementData.value / (float) achievementData.max;
    }
}

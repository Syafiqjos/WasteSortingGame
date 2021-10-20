using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionLockButton : MonoBehaviour
{
    public bool checkLock = true;
    public string prevLevelID = "Level";

    public Button button;
    public GameObject buttonText;

    public Sprite unlockedButton;
    public Sprite lockedButton;

    private void Start()
    {
        if (checkLock)
        {
            if (!PlayerPrefs.HasKey(prevLevelID) || PlayerPrefs.GetInt(prevLevelID) != 1)
            {
                buttonText.SetActive(false);
                button.image.sprite = lockedButton;
                button.interactable = false;
            } else
            {
                button.image.sprite = unlockedButton;
                button.interactable = true;
            }
        } else
        {
            button.image.sprite = unlockedButton;
            button.interactable = true;
        }
    }
}

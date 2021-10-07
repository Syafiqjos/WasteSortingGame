using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static bool HasBeenWatchHowToPlay { get; set; }  = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GotoLevelSelection()
    {
        if (PlayerPrefs.HasKey("Tutorial"))
        {
            HasBeenWatchHowToPlay = true;
        }

        if (HasBeenWatchHowToPlay)
        {
            SceneManager.LoadScene("LevelSelection");
        } else
        {
            GotoHowToPlay();
        }
    }

    public void GotoHowToPlay()
    {
        HasBeenWatchHowToPlay = true;
        SceneManager.LoadScene("HowToPlay");
    }

    public void GotoAchievementMenu()
    {
        SceneManager.LoadScene("AchievementMenu");
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");

        Application.Quit();
    }
}

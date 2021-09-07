using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static bool HasBeenWatchHowToPlay { get; set; }  = false;

    public void GotoLevelSelection()
    {
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
}

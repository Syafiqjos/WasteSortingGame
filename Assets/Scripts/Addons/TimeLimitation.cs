using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimitation : MonoBehaviour
{
    public static TimeLimitation Instance { get; private set; }

    private DateTime timeStart;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

            Initial();
        }
    }

    void Update() {
        
    }

    void Initial(){
        timeStart = DateTime.Now;

        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    public void CheckTimeLimit(){
        if (SceneManager.GetActiveScene().name != "TimeLimitation") {
            DateTime timeCurrent = DateTime.Now;
            double deltaTime = (timeCurrent - timeStart).TotalMinutes;

            if (deltaTime >= 120) { // 2 hours
            // if (deltaTime >= 1) { // 1 minute
                SceneManager.LoadScene("TimeLimitation");
            }
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        string currentName = current.name;

        CheckTimeLimit();
    }
}

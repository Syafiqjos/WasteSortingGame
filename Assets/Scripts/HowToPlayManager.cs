using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject[] tutorialsStep;
    public Trashbin trashbin;

    int step = 0;

    private void Start()
    {
        RefreshTutorialStep();
    }

    public void NextTutorialStep()
    {
        step++;
        RefreshTutorialStep();
    }

    private void RefreshTutorialStep()
    {
        foreach (GameObject o in tutorialsStep)
        {
            o.SetActive(false);
        }

        if (step >= tutorialsStep.Length)
        {
            BackToMainMenu();
        }
        else if (step >= 0)
        {
            tutorialsStep[step].SetActive(true);
        }


        //Special Condition
        if (step == 2) trashbin.ChangeTrashType(TrashType.Green);
        else if (step == 3) trashbin.ChangeTrashType(TrashType.Yellow);
        else if (step == 4) trashbin.ChangeTrashType(TrashType.Red);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

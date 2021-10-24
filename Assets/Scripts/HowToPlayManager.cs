using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Extensione.Audio;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject[] tutorialsStep;
    public GameObject[] tutorialObjects;

    public Trashbin trashbin;
    public TrashbinController trashbinController;
    public TrashReceiver trashReceiver;

    public AudioClip rightTrashSFX;
    public AudioClip wrongTrashSFX;

    int step = 0;

    private void Start()
    {
        trashReceiver.OnTrashDestroy.AddListener(CheckReceivedTrash);

        SaveHasBeenTutorial();
        RefreshTutorialStep();
        RefreshTutorialObject();
    }

    private void SaveHasBeenTutorial()
    {
        PlayerPrefs.SetInt("Tutorial", 1);
        PlayerPrefs.Save();
    }

    public void NextTutorialStep()
    {
        step++;
        RefreshTutorialStep();
        RefreshTutorialObject();
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
        if (step == 2)
        {
            trashbin.ChangeTrashType(TrashType.Green);
            // trashbinController.Moveable = false;
            // trashbinController.ResetPosition();
        }
        else if (step == 3)
        {
            trashbin.ChangeTrashType(TrashType.Yellow);
            // trashbinController.Moveable = false;
            // trashbinController.ResetPosition();
        }
        else if (step == 4)
        {
            trashbin.ChangeTrashType(TrashType.Red);
            // trashbinController.Moveable = false;
            // trashbinController.ResetPosition();
        }
        else if (step == 5)
        {
            // Sekarang kita coba main

            // trashbin.ChangeTrashType(TrashType.Red);
            // nextButton.gameObject.SetActive(false);
        }
        else if (step == 6)
        {
            // Ganti ke warna hijau dan tangkap sampah

            trashbin.ChangeTrashType(TrashType.Green);
        }
        else if (step == 7)
        {
            // Ganti ke warna kuning dan tangkap sampah

            trashbin.ChangeTrashType(TrashType.Yellow);
        }
        else if (step == 8)
        {
            // Ganti ke warna merah dan tangkap sampah

            trashbin.ChangeTrashType(TrashType.Red);
        }
        else if (step == 9)
        {
            // Keren, kamu bisa terus berlatih atau next untuk mulai permainannya


        }
        else if (step == 10)
        {
            // Back main menu
        }
    }

    private void RefreshTutorialObject()
    {
        foreach (GameObject o in tutorialObjects)
        {
            if (o)
            {
                o.SetActive(false);
            }
        }

        if (step >= 0)
        {
            if (tutorialObjects[step])
            {
                tutorialObjects[step].SetActive(true);
            }
        }
    }

    private void CheckReceivedTrash(Trash trash)
    {
        if (trash.trashType == trashbin.trashType)
        {
            if (step < 9) {
                NextTutorialStep();
            }

            AudioManager.Instance?.PlaySFXOnce(rightTrashSFX);
        } else
        {
            AudioManager.Instance?.PlaySFXOnce(wrongTrashSFX);
        }

        Destroy(trash.gameObject);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

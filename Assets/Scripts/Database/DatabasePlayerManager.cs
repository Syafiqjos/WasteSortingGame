using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Extensione.Window;

public class DatabasePlayerManager : MonoBehaviour
{
    public GameObject uploadModalWindow;
    public GameObject downloadModalWindow;

    public InputField uploadInputField;
    public InputField downloadInputField;

    public string username;

    public DatabaseManager databaseManager;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Username"))
        {
            username = PlayerPrefs.GetString("Username");
        }

        uploadInputField.onValueChanged.AddListener(InputChange);
        downloadInputField.onValueChanged.AddListener(InputChange);

        uploadInputField.text = username;
        downloadInputField.text = username;
    }

    public void OpenUploadWindow()
    {
        uploadInputField.text = username;
        uploadModalWindow.SetActive(true);
    }

    public void OpenDownloadWindow()
    {
        downloadInputField.text = username;
        downloadModalWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        uploadModalWindow.SetActive(false);
        downloadModalWindow.SetActive(false);
    }

    public void UploadData()
    {
        if (username.Length >= 5)
        {
            PlayerPrefs.SetString("Username", username);
            PlayerPrefs.Save();

            databaseManager.SaveData();
        }
        else
        {
            WindowMaster.Instance.Show("Please insert username at least 5 characters!");
        }
    }

    public void DownloadData()
    {
        if (username.Length >= 5)
        {
            PlayerPrefs.SetString("Username", username);
            PlayerPrefs.Save();

            databaseManager.LoadData();
        }
        else
        {
            WindowMaster.Instance.Show("Please insert username at least 5 characters!");
        }
    }

    private void InputChange(string x)
    {
        username = x;
    }
}

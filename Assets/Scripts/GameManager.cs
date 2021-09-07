using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string levelID;

    public bool isGameOver { get; private set; } = false;
    public int gameScore { get; private set; }
    public int remainingLife { get; private set; }

    [SerializeField] private int lifeCount = 3;
    [SerializeField] private int scoreMultiplier = 10;

    public TrashReceiver trashReceiver;
    public Trashbin trashbin;

    private void Start()
    {
        trashReceiver.OnTrashDestroy.AddListener(DestroyTrash);
    }

    private void IncreaseScore()
    {
        gameScore += scoreMultiplier;
    }

    private void DecreaseLife()
    {
        remainingLife--;
        if (remainingLife < 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        SaveHighscore();
    }

    private void SaveHighscore()
    {
        string levelHighscoreKey = $"{levelID}_score";
        int lastHighscore = gameScore;

        if (PlayerPrefs.HasKey(levelHighscoreKey))
        {
            lastHighscore = PlayerPrefs.GetInt(levelHighscoreKey);
        }

        if (gameScore >= lastHighscore)
        {
            PlayerPrefs.SetInt(levelHighscoreKey, gameScore);
        }
    }

    public void DestroyTrash(Trash trash)
    {
        if (trash)
        {
            if (trash.trashType == trashbin.trashType)
            {
                IncreaseScore();
            } else
            {
                DecreaseLife();
            }

            Destroy(trash.gameObject);
        }
    }
}

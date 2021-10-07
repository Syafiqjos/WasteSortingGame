using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Extensione.Audio;

public class QuizQuestion {
    public string question;
    public List<string> answer;

    public QuizQuestion(string question, List<string> answer){
        this.question = question;
        this.answer = answer;
    }
}

public class PopupQuiz : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject quizUI;

    public TMP_Text questionText;
    
    private List<QuizQuestion> questions;
    public List<Button> buttons;

    public bool refreshQuiz = false;

    void Start()
    {
        RegisterQuestion();

        quizUI.SetActive(false);
    }

    void Update()
    {
        if (gameManager.remainingTime <= 0 && !refreshQuiz)
        {
            quizUI.SetActive(true);
            ShowQuiz();

            refreshQuiz = true;
        }
    }

    public void ShowQuiz(){
        QuizQuestion q = GetRandomQuestion();

        questionText.text = q.question;

        int randomFactor = Random.Range(0, 100);

        for (int i = 0; i < buttons.Count; i++){
            int index = (i + randomFactor) % buttons.Count;
            TMP_Text btext = buttons[i].GetComponentInChildren<TMP_Text>();
            btext.text = q.answer[index];

            int id = index;
            buttons[i].onClick.AddListener(() => {
                CheckAnswer(id);
            });
        }
    }

    void RegisterQuestion(){
        questions = new List<QuizQuestion>();

        // Kunci jawaban taruh di index 0
        questions.Add(new QuizQuestion("What rubbish that is not recycleable?", new List<string>(){ "Kitchen Waste", "Plastic Bottle", "Metal" }));
        questions.Add(new QuizQuestion("Berapakah satu ditambah satu?", new List<string>(){ "Dua", "Satu", "Tiga" }));
        questions.Add(new QuizQuestion("Pilih yang merah?", new List<string>(){ "Red", "Green", "Blue" }));
    }

    QuizQuestion GetRandomQuestion() {
        return questions[Random.Range(0, questions.Count)];
    }

    public void CheckAnswer(int index){
        if (index == 0) {
            Debug.Log("Quiz True");

            AudioManager.Instance.PlayButtonRightSFX();
            gameManager.IncreaseQuizScore();
        } else {
            Debug.Log("Quiz False");

            AudioManager.Instance.PlayButtonWrongSFX();
        }

        gameManager.GameOverPopQuiz();
        quizUI.SetActive(false);
    }
}

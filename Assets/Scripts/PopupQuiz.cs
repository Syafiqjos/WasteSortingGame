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
        // questions.Add(new QuizQuestion("What rubbish that is not recycleable?", new List<string>(){ "Kitchen Waste", "Plastic Bottle", "Metal" }));
        // questions.Add(new QuizQuestion("Berapakah satu ditambah satu?", new List<string>(){ "Dua", "Satu", "Tiga" }));
        // questions.Add(new QuizQuestion("Pilih yang merah?", new List<string>(){ "Red", "Green", "Blue" }));
        questions.Add(new QuizQuestion("Which one of these waste that is organic?", new List<string>() { "Grass and Leaves", "Metal", "Plastic" }));
        questions.Add(new QuizQuestion("Which one of these waste that is organic?", new List<string>() { "Food Leftover", "Soda Can", "Glass" }));
        questions.Add(new QuizQuestion("Which one of these waste that is organic?", new List<string>() { "Banana Skin", "Broken Joystick", "Sandals" }));

        questions.Add(new QuizQuestion("Which one of these waste that is hard to decompose?", new List<string>() { "Plastic", "Rotten Apple", "Tree Branch" }));
        questions.Add(new QuizQuestion("Which one of these waste that is hard to decompose?", new List<string>() { "Glass", "Leaves", "Grass" }));

        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Green Trashbin?", new List<string>() { "Food Leftover", "Paper Bag", "Plastic Bag" }));
        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Green Trashbin?", new List<string>() { "Fish", "Paper Bag", "Window Glass" }));

        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Yellow Trashbin?", new List<string>() { "Paper Bag", "Broken Glass", "Food Leftover" }));
        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Yellow Trashbin?", new List<string>() { "Brochure", "Broken Glass", "Styrofoam" }));

        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Red Trashbin?", new List<string>() { "Broken Glass", "Newspaper", "Rotten Pear" }));
        questions.Add(new QuizQuestion("Which one of these waste that is belong to the Red Trashbin?", new List<string>() { "Styrofoam", "Paper Bag", "Rotten Apple" }));
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

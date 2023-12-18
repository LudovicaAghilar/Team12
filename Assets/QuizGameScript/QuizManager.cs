using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI scoreText;
    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public Text QuestionTxt;
    public bool isCorrect = false;
    public float score;
    public float Score => score;
    public GameObject Quizpanel;
    public GameObject GoPanel;
    public bool isAnswered = false;
    public bool flag = false;
    public Text finalScoreText;

    private void Start()
    {
            score = 0;
            GoPanel.SetActive(false);
            currentQuestion = 0;
            generateQuestion();

    }

    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        Quizpanel.SetActive(false);
        GoPanel.SetActive(true);

        Text finalScoreText = GoPanel.GetComponentInChildren<Text>();
        if (finalScoreText != null)
        {
            finalScoreText.text = "Final Score: " + Mathf.FloorToInt(score).ToString("D5");
            PersistentData.Gummy += Mathf.FloorToInt(score);
        }
    }

    public void correct(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            score += 2;
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        }
        else
        {
            Debug.Log("Wrong Answer");
        }

        isAnswered = true;
        StartCoroutine(NextQuestionWithDelay(1.0f));

    }

    private void Update()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }
        }
    }

    //The function continuously generates questions until "currentQuestion" is less than the predefined QnA.Count set at 10
    void generateQuestion()
    {
        Debug.Log(currentQuestion);
        Debug.Log(QnA.Count);
        if (currentQuestion < QnA.Count)
        {

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();

            NextQuestionWithDelay(1.0f);
        }
        else
        {

            QuestionTxt.text = "";
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
            foreach (var option in options)
            {
                option.SetActive(false);
            }

            Debug.Log("out of questions");
            GameOver();
        }
    }

    IEnumerator NextQuestionWithDelay(float delay)
    {
        yield return new WaitUntil(() => isAnswered);
        currentQuestion= currentQuestion+1;
        yield return new WaitForSeconds(delay);
        generateQuestion();

    }
}

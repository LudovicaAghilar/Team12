using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerScript : MonoBehaviour
{
    public bool isCorrect;
    private Image buttonImage;

    public QuizManager quizManager;

    private void Start()
        {
            buttonImage = GetComponent<Image>();
            buttonImage.color = Color.white;
        }

    public void Answer()
    {
        if(isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.correct(true);
            buttonImage.color = Color.green;
            StartCoroutine(ResetButtonColor(1.0f));
        }
        else
        {
            Debug.Log("Wrong Answer");
            quizManager.correct(false);
            buttonImage.color = Color.red;
            StartCoroutine(ResetButtonColor(1.0f));
            
        }
    }

    IEnumerator ResetButtonColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonImage.color = Color.white;
    }

}
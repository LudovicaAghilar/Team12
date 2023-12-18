using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionnaireEnvi : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Dropdown dropdown4;
    public Text resultText;

    private void Start()
    {
        // Initialize your InputFields and resultText references
        dropdown1 = GameObject.Find("Dropdown1").GetComponent<Dropdown>();
        dropdown2 = GameObject.Find("Dropdown2").GetComponent<Dropdown>();
        dropdown3 = GameObject.Find("Dropdown3").GetComponent<Dropdown>();
        dropdown4 = GameObject.Find("Dropdown4").GetComponent<Dropdown>();
        //resultText = GameObject.Find("ResultText").GetComponent<Text>();
        LoadSavedScore();
    }

    private void LoadSavedScore()
    {
        if (PlayerPrefs.HasKey("SavedScorePHQ"))
        {
            int savedScorePhq = PlayerPrefs.GetInt("SavedScorePHQ");
        }
    }

    public void CalculateSum()
    {
        // Parse input values and calculate sum
        int value1 = dropdown1.value;
        int value2 = dropdown2.value;
        int value3 = dropdown3.value;
        int value4 = dropdown4.value;

        int sum_envi = value1 + value2 + value3 + value4 - 4;

        //resultText.text = "Sum: " + sum_envi;


        // Save the result
        SaveScore(sum_envi);

    }
    public void CompleteQuestionnaire()
    {
        // Call CalculateSum when the questionnaire is completed
        CalculateSum();
    }

    private void SaveScore(int sum_envi)
    {
        // Save the score using PlayerPrefs
        PlayerPrefs.SetInt("SavedScoreEnvi", sum_envi);
    }



}



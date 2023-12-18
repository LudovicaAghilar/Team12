using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class PHQQuiz : MonoBehaviour
{
    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Dropdown dropdown3;
    public Dropdown dropdown4;
    public Text resultText;

    private void Start()
    {

        dropdown1 = GameObject.Find("Dropdown1").GetComponent<Dropdown>();
        dropdown2 = GameObject.Find("Dropdown2").GetComponent<Dropdown>();
        dropdown3 = GameObject.Find("Dropdown3").GetComponent<Dropdown>();
        dropdown4 = GameObject.Find("Dropdown4").GetComponent<Dropdown>();

    }

    public void CalculateScore()
    {
        // Parse input values and calculate sum
        int value1 = dropdown1.value;
        int value2 = dropdown2.value;
        int value3 = dropdown3.value;
        int value4 = dropdown4.value;

        int depression_score = value1 + value2 - 2;
        int anxiety_score = value3 + value4 - 2;

        //resultText.text = "Sum: " + depression_score;

        string playerFlag = DeterminePlayerFlag(depression_score, anxiety_score);

        // Save the result
        SaveScore(playerFlag);

    }

    private string DeterminePlayerFlag(int depression_score, int anxiety_score)
    {
        if (depression_score < 3 && anxiety_score < 3)
        {
            return "Persona1";
        }
        else if (depression_score < 3 && anxiety_score >= 3)
        {
            return "Persona2";
        }
        else if (depression_score >= 3 && anxiety_score >= 3)
        {
            return "Persona3";
        }
        return "DefaultPersona";
    }

    public void CompleteQuestionnaire()
    {
        // Call CalculateSum when the questionnaire is completed
        CalculateScore();
    }

    public void SaveScore(string playerFlag)
    {
        // Save the score using PlayerPrefs
        PlayerPrefs.SetString("PlayerFlag", playerFlag);

        // Change scene based on the player's flag
        switch (playerFlag)
        {
            case "Persona1":
                SceneManager.LoadScene(4); // Change to scene "CityEasy"
                break;

            case "Persona2":
                SceneManager.LoadScene(15); // Change to scene "CityMid"
                break;

            case "Persona3":
                SceneManager.LoadScene(25); // Change to scene "CityDifficult"
                break;

            default:
                // You can handle a default case or do nothing if needed
                break;
        }
    }
}


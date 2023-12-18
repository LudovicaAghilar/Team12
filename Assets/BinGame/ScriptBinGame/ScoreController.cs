using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;

    private int score;

    void Start()
    {
     gameOverPanel.SetActive(false);
    }

    void Update()
    {
        scoreText.text = score.ToString();

    }
    // the function handles events for 'Bomb' and 'Garbage' objects triggering game over and score updates
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bomb")
            gameOverPanel.SetActive(true);
            PersistentData.Gummy += Mathf.FloorToInt(score);

    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "Garbage")
        {
            Destroy(target.gameObject);
            score++;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

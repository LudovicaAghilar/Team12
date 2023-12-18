using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI gameOverText;
    [SerializeField] public Button retryButton;

    private Player player_dino;
    private Spawner spawner;

    private float score;
    private bool hasEncounteredFirstGummy = false;
    public float Score => score;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player_dino = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        player_dino.OnObstacleHit += GameOver;
        player_dino.OnGummyPassed += IncreaseScore;

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        Gummy[] gummies = FindObjectsOfType<Gummy>();
        foreach (var gummy in gummies)
        {
            Destroy(gummy.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player_dino.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);

    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        PersistentData.Gummy += Mathf.FloorToInt(score);
        player_dino.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);


    }

    public void IncreaseScore()
    {

        if (hasEncounteredFirstGummy)
        {
            score += 2;
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        }
        else
        {

            hasEncounteredFirstGummy = true;
            score += 2;
            scoreText.text = Mathf.FloorToInt(score).ToString("D5");

        }
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }
}

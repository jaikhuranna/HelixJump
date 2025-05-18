using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelManager levelManager;

    [SerializeField] private BallController ball;
    [SerializeField] private TMP_Text highScoreTMP;
    [SerializeField] private TMP_Text currentScoreTMP;
    [SerializeField] private GameObject gameOverScreen;
    private bool isGameActive = true;
    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreTMP.text = "High Score: \n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        print("High Score: " + highScore);
    }
    public void GameOver()
    {
        if (!isGameActive) return;
        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save(); 
            print("New High Score: " + currentScore);
        }
    
        isGameActive = false;
        print("Game Over! Score: " + currentScore);
        highScoreTMP.text = "High Score: \n" + PlayerPrefs.GetInt("HighScore", 0).ToString();
        gameOverScreen.SetActive(true);
    }

      public void RestartGame()
    {
        currentScore = 0;
        isGameActive = true;
            ball.ResetBall();

        if (levelManager != null)
        {
            levelManager.ResetLevel();
        }
        gameOverScreen.SetActive(false);
        
        currentScoreTMP.text = "Current Score: \n" + currentScore.ToString();
    }

    public void AddScore()
    {
        if (isGameActive)
        {
            currentScore++;
            levelManager.loadPlatformFromPool(1);
            print("Score: " + currentScore);
            currentScoreTMP.text = "Current Score: \n" + currentScore.ToString();

        }
    }
}
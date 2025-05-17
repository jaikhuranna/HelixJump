using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LevelManager levelManager;
    private bool isGameActive = true;
    private int currentScore = 0;
    
    void Awake()
    {
        Instance = this;
    }
    
    public void GameOver()
    {
        if (!isGameActive) return;
        
        isGameActive = false;
        Debug.Log("Game Over! Score: " + currentScore);
        
        Invoke("RestartGame", 1.5f);
    }
    
    public void RestartGame()
    {
        currentScore = 0;
        // levelManager.GenerateLevel();
        isGameActive = true;
    }
    
    public void AddScore()
    {
        if (isGameActive)
        {
            currentScore++;
            Debug.Log("Score: " + currentScore);
        }
    }
}

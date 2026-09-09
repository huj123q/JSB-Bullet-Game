using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 游戏管理器 - 控制游戏的全局状态和流程
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Playing, Paused, GameOver }
    
    private GameState currentState = GameState.MainMenu;
    private float currentScore = 0f;
    private int currentLevel = 0;
    private int playerHealth = 3;
    
    public const int MAX_HEALTH = 3;
    public const int INITIAL_HEALTH = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerHealth = INITIAL_HEALTH;
    }

    private void Update()
    {
        if (currentState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else if (currentState == GameState.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
    }

    /// <summary>
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        currentState = GameState.Playing;
        currentScore = 0f;
        playerHealth = INITIAL_HEALTH;
        Time.timeScale = 1f;
        
        AudioManager.Instance?.PlayGameMusic();
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.Paused;
            Time.timeScale = 0f;
            AudioManager.Instance?.PauseMusic();
        }
    }

    /// <summary>
    /// 恢复游戏
    /// </summary>
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            currentState = GameState.Playing;
            Time.timeScale = 1f;
            AudioManager.Instance?.ResumeMusic();
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void EndGame(bool isVictory)
    {
        currentState = GameState.GameOver;
        Time.timeScale = 0f;
        AudioManager.Instance?.StopMusic();
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    public void PlayerTakeDamage()
    {
        playerHealth--;
        if (playerHealth <= 0)
        {
            EndGame(false);
        }
    }

    /// <summary>
    /// 添加分数
    /// </summary>
    public void AddScore(float points)
    {
        currentScore += points;
    }

    // 属性访问器
    public GameState CurrentState => currentState;
    public float CurrentScore => currentScore;
    public int CurrentLevel => currentLevel;
    public int PlayerHealth => playerHealth;
}

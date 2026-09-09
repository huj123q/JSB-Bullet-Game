using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI管理器 - 管理游戏UI显示
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Text scoreText;
    [SerializeField] private Text comboText;
    [SerializeField] private Text healthText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text timerText;

    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas gameOverMenuCanvas;
    [SerializeField] private Canvas mainMenuCanvas;

    private float levelStartTime = 0f;
    private float levelDuration = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            UpdateGameplayUI();
        }
    }

    /// <summary>
    /// 更新游戏进行中的UI
    /// </summary>
    private void UpdateGameplayUI()
    {
        // 更新分数
        if (scoreText != null)
            scoreText.text = $"Score: {ScoreManager.Instance.CurrentScore:F0}";

        // 更新连击
        if (comboText != null)
        {
            if (ScoreManager.Instance.CurrentCombo > 1)
                comboText.text = $"Combo: {ScoreManager.Instance.CurrentCombo}x";
            else
                comboText.text = "";
        }

        // 更新血量
        if (healthText != null)
            healthText.text = $"Health: {GameManager.Instance.PlayerHealth}/{GameManager.MAX_HEALTH}";

        // 更新关卡
        if (levelText != null)
            levelText.text = $"Level: {GameManager.Instance.CurrentLevel + 1}";

        // 更新计时器
        if (timerText != null && LevelManager.Instance != null)
        {
            LevelManager.LevelData level = LevelManager.Instance.GetCurrentLevel();
            if (level != null)
            {
                float elapsedTime = Time.time - levelStartTime;
                float remainingTime = Mathf.Max(0, level.duration - elapsedTime);
                int minutes = (int)(remainingTime / 60f);
                int seconds = (int)(remainingTime % 60f);
                timerText.text = $"Time: {minutes:00}:{seconds:00}";
            }
        }
    }

    /// <summary>
    /// 更新UI
    /// </summary>
    private void UpdateUI()
    {
        UpdateGameplayUI();
    }

    /// <summary>
    /// 显示暂停菜单
    /// </summary>
    public void ShowPauseMenu()
    {
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏暂停菜单
    /// </summary>
    public void HidePauseMenu()
    {
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示游戏结束菜单
    /// </summary>
    public void ShowGameOverMenu(bool victory)
    {
        if (gameOverMenuCanvas != null)
        {
            gameOverMenuCanvas.gameObject.SetActive(true);
            
            // 可以添加更多UI元素更新逻辑
            Text gameOverText = gameOverMenuCanvas.GetComponentInChildren<Text>();
            if (gameOverText != null)
            {
                gameOverText.text = victory ? "Level Complete!" : "Game Over!";
            }
        }
    }

    /// <summary>
    /// 显示主菜单
    /// </summary>
    public void ShowMainMenu()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏主菜单
    /// </summary>
    public void HideMainMenu()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.gameObject.SetActive(false);
    }

    public void SetLevelStartTime(float startTime)
    {
        levelStartTime = startTime;
    }

    public void SetLevelDuration(float duration)
    {
        levelDuration = duration;
    }
}

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡管理器 - 管理游戏关卡和难度
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public AudioClip musicClip;
        public BulletPattern[] bulletPatterns;
        public float duration; // 关卡时长（秒）
        public int difficulty; // 难度等级
    }

    [SerializeField] private LevelData[] levels;
    [SerializeField] private int currentLevelIndex = 0;

    private float levelStartTime = 0f;
    private float levelEndTime = 0f;
    private int bulletsSpawned = 0;

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
        if (levels.Length == 0)
        {
            Debug.LogError("No levels defined!");
        }
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
        {
            UpdateLevel();
        }
    }

    /// <summary>
    /// 启动关卡
    /// </summary>
    public void StartLevel(int levelIndex = 0)
    {
        if (levelIndex >= levels.Length)
        {
            Debug.LogWarning($"Level index {levelIndex} out of range");
            return;
        }

        currentLevelIndex = levelIndex;
        LevelData level = levels[currentLevelIndex];

        levelStartTime = Time.time;
        levelEndTime = levelStartTime + level.duration;
        bulletsSpawned = 0;

        // 播放音乐
        AudioManager.Instance?.PlayGameMusic(level.musicClip);

        Debug.Log($"Starting level: {level.levelName}");
    }

    /// <summary>
    /// 更新关卡逻辑
    /// </summary>
    private void UpdateLevel()
    {
        if (currentLevelIndex >= levels.Length)
            return;

        LevelData level = levels[currentLevelIndex];
        float elapsedTime = Time.time - levelStartTime;

        // 生成弹幕
        foreach (var pattern in level.bulletPatterns)
        {
            if (pattern.spawnTime <= elapsedTime && pattern.spawnTime > elapsedTime - Time.deltaTime)
            {
                BulletManager.Instance.SpawnBullet(pattern);
                bulletsSpawned++;
            }
        }

        // 检查关卡是否完成
        if (elapsedTime >= level.duration)
        {
            CompleteLevelSuccess();
        }
    }

    /// <summary>
    /// 关卡成功完成
    /// </summary>
    private void CompleteLevelSuccess()
    {
        float timeRemaining = Mathf.Max(0, levelEndTime - Time.time);
        ScoreManager.Instance.AddLevelCompleteBonus(currentLevelIndex + 1, timeRemaining);
        
        GameManager.Instance.EndGame(true);
        Debug.Log($"Level {currentLevelIndex + 1} completed!");
    }

    /// <summary>
    /// 获取当前关卡数据
    /// </summary>
    public LevelData GetCurrentLevel()
    {
        if (currentLevelIndex < levels.Length)
            return levels[currentLevelIndex];
        return null;
    }

    /// <summary>
    /// 获取下一关
    /// </summary>
    public bool NextLevel()
    {
        if (currentLevelIndex + 1 < levels.Length)
        {
            StartLevel(currentLevelIndex + 1);
            return true;
        }
        return false;
    }

    public int CurrentLevelIndex => currentLevelIndex;
    public int TotalLevels => levels.Length;
}

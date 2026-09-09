using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 分数管理器 - 处理游戏分数、连击和排行榜
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private float basePointsPerBulletDodge = 10f;
    [SerializeField] private float comboMultiplier = 1.1f;

    private float currentScore = 0f;
    private int currentCombo = 0;
    private float lastComboTime = 0f;
    private const float COMBO_TIMEOUT = 2f; // 连击超时（秒）

    private List<ScoreEntry> leaderboard = new List<ScoreEntry>();

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public float score;
        public int level;
        public System.DateTime date;
    }

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
        ResetScore();
        LoadLeaderboard();
    }

    private void Update()
    {
        // 检查连击是否超时
        if (Time.time - lastComboTime > COMBO_TIMEOUT && currentCombo > 0)
        {
            currentCombo = 0;
        }
    }

    /// <summary>
    /// 重置分数
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0f;
        currentCombo = 0;
        lastComboTime = Time.time;
    }

    /// <summary>
    /// 躲避弹幕时获得分数
    /// </summary>
    public void AddDodgeScore()
    {
        currentCombo++;
        lastComboTime = Time.time;

        // 计算连击倍数
        float multiplier = Mathf.Pow(comboMultiplier, currentCombo - 1);
        float points = basePointsPerBulletDodge * multiplier;

        currentScore += points;
        GameManager.Instance.AddScore(points);
    }

    /// <summary>
    /// 玩家被击中时重置连击
    /// </summary>
    public void ResetCombo()
    {
        currentCombo = 0;
    }

    /// <summary>
    /// 关卡完成时的奖励
    /// </summary>
    public void AddLevelCompleteBonus(int level, float timeRemaining)
    {
        float bonus = 1000f * level * Mathf.Max(0.5f, timeRemaining / 60f);
        currentScore += bonus;
        GameManager.Instance.AddScore(bonus);
    }

    /// <summary>
    /// 保存分数到排行榜
    /// </summary>
    public void SaveScore(string playerName)
    {
        ScoreEntry entry = new ScoreEntry()
        {
            playerName = playerName,
            score = currentScore,
            level = GameManager.Instance.CurrentLevel,
            date = System.DateTime.Now
        };

        leaderboard.Add(entry);
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        // 只保留前10个
        if (leaderboard.Count > 10)
        {
            leaderboard.RemoveAt(leaderboard.Count - 1);
        }

        SaveLeaderboard();
    }

    /// <summary>
    /// 保存排行榜到本地
    /// </summary>
    private void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new LeaderboardData { entries = leaderboard });
        PlayerPrefs.SetString("Leaderboard", json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 加载排行榜
    /// </summary>
    private void LoadLeaderboard()
    {
        if (PlayerPrefs.HasKey("Leaderboard"))
        {
            string json = PlayerPrefs.GetString("Leaderboard");
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);
            leaderboard = data.entries;
        }
    }

    [System.Serializable]
    private class LeaderboardData
    {
        public List<ScoreEntry> entries = new List<ScoreEntry>();
    }

    public float CurrentScore => currentScore;
    public int CurrentCombo => currentCombo;
    public List<ScoreEntry> Leaderboard => leaderboard;
}

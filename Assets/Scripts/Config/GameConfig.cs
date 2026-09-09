using UnityEngine;

/// <summary>
/// 游戏配置常量
/// </summary>
public class GameConfig
{
    // 游戏设置
    public const float TARGET_FRAME_RATE = 60f;
    public const bool USE_V_SYNC = false;

    // 玩家设置
    public const float PLAYER_MOVE_SPEED = 5f;
    public const float PLAYER_FAST_MOVE_SPEED = 8f;
    public const float PLAYER_SIZE = 0.5f;

    // 弹幕设置
    public const int MAX_BULLETS = 1000;
    public const int INITIAL_BULLET_POOL_SIZE = 500;
    public const float BULLET_SIZE = 0.3f;

    // 音频设置
    public const float MASTER_VOLUME = 1f;
    public const float MUSIC_VOLUME = 0.7f;
    public const float SFX_VOLUME = 0.8f;
    public const float AUDIO_DELAY_OFFSET = 0.05f; // 秒

    // 分数设置
    public const float BASE_POINTS_PER_DODGE = 10f;
    public const float COMBO_MULTIPLIER = 1.1f;
    public const float COMBO_TIMEOUT = 2f; // 秒

    // UI设置
    public const float UI_ANIMATION_DURATION = 0.3f;

    // 难度设置
    public const int TOTAL_LEVELS = 10;
    public const float DIFFICULTY_MULTIPLIER = 1.15f;
}

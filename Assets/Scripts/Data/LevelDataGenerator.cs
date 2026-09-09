using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 示例关卡配置生成器
/// </summary>
public class LevelDataGenerator : MonoBehaviour
{
    /// <summary>
    /// 生成示例关卡数据
    /// </summary>
    public static LevelManager.LevelData GenerateSampleLevel(int levelIndex)
    {
        LevelManager.LevelData level = new LevelManager.LevelData();
        level.levelName = $"Level {levelIndex + 1}";
        level.difficulty = levelIndex + 1;
        level.duration = 30f + (levelIndex * 10f); // 根据难度增加时长

        // 生成随机弹幕模式
        List<BulletPattern> patterns = new List<BulletPattern>();
        
        float spawnInterval = 0.5f - (levelIndex * 0.05f); // 难度越高，弹幕越密集
        float currentSpawnTime = 1f;

        while (currentSpawnTime < level.duration)
        {
            BulletPattern pattern = GenerateRandomBulletPattern(currentSpawnTime, levelIndex);
            patterns.Add(pattern);
            currentSpawnTime += spawnInterval;
        }

        level.bulletPatterns = patterns.ToArray();
        return level;
    }

    /// <summary>
    /// 生成随机弹幕模式
    /// </summary>
    private static BulletPattern GenerateRandomBulletPattern(float spawnTime, int difficulty)
    {
        BulletPattern pattern = new BulletPattern();
        pattern.spawnTime = spawnTime;
        pattern.type = (BulletType)(Random.Range(0, 2)); // 只使用圆形和方形
        
        // 随机生成位置
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHeight = Camera.main.orthographicSize;
        
        pattern.position = new Vector3(
            Random.Range(-screenWidth * 0.8f, screenWidth * 0.8f),
            Random.Range(-screenHeight * 0.8f, screenHeight * 0.8f),
            0f
        );

        // 随机方向和速度
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        pattern.direction = randomDirection;
        pattern.speed = 3f + (difficulty * 0.5f);
        pattern.lifetime = 10f;
        pattern.scale = 1f;

        return pattern;
    }
}

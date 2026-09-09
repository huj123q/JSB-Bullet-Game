using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 弹幕类型枚举
/// </summary>
public enum BulletType
{
    Circle,      // 圆形
    Square,      // 方形
    Wave,        // 波形
    Spiral,      // 螺旋
    Laser        // 激光束
}

/// <summary>
/// 弹幕数据结构
/// </summary>
[System.Serializable]
public class BulletPattern
{
    public float spawnTime;          // 生成时间（秒）
    public BulletType type;          // 弹幕类型
    public Vector3 position;         // 生成位置
    public Vector3 direction;        // 移动方向
    public float speed;              // 移动速度
    public float lifetime;           // 生存时间
    public float scale = 1f;         // 大小缩放
}

/// <summary>
/// 弹幕管理器 - 管理所有弹幕的生成和更新
/// </summary>
public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    [SerializeField] private int maxBullets = 1000;
    [SerializeField] private Transform bulletContainer;

    private Queue<Bullet> bulletPool = new Queue<Bullet>();
    private List<Bullet> activeBullets = new List<Bullet>();
    private Bullet bulletPrefab;

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
        if (bulletContainer == null)
            bulletContainer = transform;

        InitializeBulletPool();
    }

    private void Update()
    {
        UpdateActiveBullets();
    }

    /// <summary>
    /// 初始化弹幕对象池
    /// </summary>
    private void InitializeBulletPool()
    {
        // 从Resources或Prefabs加载弹幕预制体
        if (bulletPrefab == null)
        {
            bulletPrefab = Resources.Load<Bullet>("Prefabs/Bullet");
        }

        for (int i = 0; i < maxBullets / 2; i++)
        {
            CreateNewBullet();
        }
    }

    /// <summary>
    /// 创建新的弹幕对象
    /// </summary>
    private void CreateNewBullet()
    {
        if (bulletPrefab == null)
            return;

        Bullet bullet = Instantiate(bulletPrefab, bulletContainer);
        bullet.gameObject.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    /// <summary>
    /// 生成弹幕
    /// </summary>
    public void SpawnBullet(BulletPattern pattern)
    {
        Bullet bullet;

        if (bulletPool.Count > 0)
        {
            bullet = bulletPool.Dequeue();
        }
        else if (activeBullets.Count < maxBullets)
        {
            CreateNewBullet();
            bullet = bulletPool.Dequeue();
        }
        else
        {
            return; // 池已满
        }

        bullet.Initialize(pattern);
        bullet.gameObject.SetActive(true);
        activeBullets.Add(bullet);
    }

    /// <summary>
    /// 更新所有活跃弹幕
    /// </summary>
    private void UpdateActiveBullets()
    {
        for (int i = activeBullets.Count - 1; i >= 0; i--)
        {
            Bullet bullet = activeBullets[i];

            if (bullet.IsAlive)
            {
                bullet.UpdateBullet();
            }
            else
            {
                // 回收弹幕
                activeBullets.RemoveAt(i);
                bullet.gameObject.SetActive(false);
                bulletPool.Enqueue(bullet);
            }
        }
    }

    /// <summary>
    /// 清除所有弹幕
    /// </summary>
    public void ClearAllBullets()
    {
        foreach (var bullet in activeBullets)
        {
            bullet.gameObject.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
        activeBullets.Clear();
    }

    public List<Bullet> GetActiveBullets() => activeBullets;
}

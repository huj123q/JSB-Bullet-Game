using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 弹幕工厂 - 创建不同类型的弹幕
/// </summary>
public class BulletFactory : MonoBehaviour
{
    private Dictionary<BulletType, System.Type> bulletTypeMap = new Dictionary<BulletType, System.Type>();

    private void Start()
    {
        InitializeBulletTypeMap();
    }

    /// <summary>
    /// 初始化弹幕类型映射
    /// </summary>
    private void InitializeBulletTypeMap()
    {
        bulletTypeMap[BulletType.Circle] = typeof(CircleBullet);
        bulletTypeMap[BulletType.Square] = typeof(SquareBullet);
        // 后续可添加更多弹幕类型
    }

    /// <summary>
    /// 根据类型创建弹幕
    /// </summary>
    public Bullet CreateBullet(BulletType type, BulletPattern pattern)
    {
        if (bulletTypeMap.ContainsKey(type))
        {
            System.Type bulletClass = bulletTypeMap[type];
            GameObject bulletObj = new GameObject("Bullet_" + type.ToString());
            Bullet bullet = bulletObj.AddComponent(bulletClass) as Bullet;
            
            if (bullet != null)
            {
                bullet.Initialize(pattern);
            }
            
            return bullet;
        }

        Debug.LogWarning($"Unknown bullet type: {type}");
        return null;
    }
}

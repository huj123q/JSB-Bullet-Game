using UnityEngine;

/// <summary>
/// 圆形弹幕
/// </summary>
public class CircleBullet : Bullet
{
    protected override void SetBulletColor()
    {
        spriteRenderer.color = Color.red;
    }

    public override void Initialize(BulletPattern bulletPattern)
    {
        base.Initialize(bulletPattern);
        
        // 绘制圆形精灵
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = CreateCircleSprite();
        }
    }

    /// <summary>
    /// 动态创建圆形精灵
    /// </summary>
    private Sprite CreateCircleSprite()
    {
        // 这里可以从Resources加载预制的圆形精灵
        return Resources.Load<Sprite>("Sprites/Circle");
    }
}

using UnityEngine;

/// <summary>
/// 方形弹幕
/// </summary>
public class SquareBullet : Bullet
{
    protected override void SetBulletColor()
    {
        spriteRenderer.color = Color.blue;
    }

    public override void Initialize(BulletPattern bulletPattern)
    {
        base.Initialize(bulletPattern);
        
        // 绘制方形精灵
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = CreateSquareSprite();
        }
    }

    /// <summary>
    /// 动态创建方形精灵
    /// </summary>
    private Sprite CreateSquareSprite()
    {
        // 这里可以从Resources加载预制的方形精灵
        return Resources.Load<Sprite>("Sprites/Square");
    }
}

using UnityEngine;

/// <summary>
/// 弹幕基类 - 所有弹幕的基础
/// </summary>
public class Bullet : MonoBehaviour
{
    protected BulletPattern pattern;
    protected float spawnTime;
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;
    protected CircleCollider2D circleCollider;

    protected bool isAlive = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider == null)
            circleCollider = gameObject.AddComponent<CircleCollider2D>();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    /// <summary>
    /// 初始化弹幕
    /// </summary>
    public virtual void Initialize(BulletPattern bulletPattern)
    {
        pattern = bulletPattern;
        spawnTime = Time.time;
        transform.position = pattern.position;
        transform.localScale = Vector3.one * pattern.scale;
        
        // 根据方向设置速度
        rb.velocity = pattern.direction.normalized * pattern.speed;

        // 设置颜色（可由子类覆盖）
        SetBulletColor();

        isAlive = true;
    }

    /// <summary>
    /// 更新弹幕
    /// </summary>
    public virtual void UpdateBullet()
    {
        // 检查生存时间
        if (Time.time - spawnTime > pattern.lifetime)
        {
            isAlive = false;
            return;
        }

        // 检查是否超出屏幕
        if (IsOutOfBounds())
        {
            isAlive = false;
        }
    }

    /// <summary>
    /// 检查是否超出屏幕
    /// </summary>
    protected bool IsOutOfBounds()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x < -0.1f || screenPoint.x > 1.1f ||
               screenPoint.y < -0.1f || screenPoint.y > 1.1f;
    }

    /// <summary>
    /// 设置弹幕颜色
    /// </summary>
    protected virtual void SetBulletColor()
    {
        spriteRenderer.color = Color.white;
    }

    /// <summary>
    /// 处理碰撞
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage();
                isAlive = false;
            }
        }
    }

    public bool IsAlive => isAlive;
}

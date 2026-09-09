using UnityEngine;

/// <summary>
/// 玩家控制器 - 处理玩家移动和碰撞
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float fastMoveSpeed = 8f;
    [SerializeField] private float screenPadding = 0.5f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector3 screenBounds;
    private int health;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player") >= 0 ? LayerMask.NameToLayer("Player") : 0;
    }

    private void Start()
    {
        health = GameManager.Instance.PlayerHealth;
        
        // 计算屏幕边界
        Camera mainCamera = Camera.main;
        float height = mainCamera.orthographicSize;
        float width = height * mainCamera.aspect;
        screenBounds = new Vector3(width, height, 0f);

        // 设置玩家外观
        spriteRenderer.color = Color.green;
        if (spriteRenderer.sprite == null)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/Player");
        }

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// 处理玩家移动
    /// </summary>
    private void HandleMovement()
    {
        Vector2 inputDirection = Vector2.zero;

        // 获取输入
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            inputDirection.y += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            inputDirection.y -= 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            inputDirection.x -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            inputDirection.x += 1f;

        // 规范化输入
        if (inputDirection.magnitude > 0)
        {
            inputDirection = inputDirection.normalized;
        }

        // 检查快速移动
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) 
            ? fastMoveSpeed 
            : moveSpeed;

        // 应用速度
        rb.velocity = inputDirection * currentSpeed;

        // 限制在屏幕范围内
        ClampPlayerPosition();
    }

    /// <summary>
    /// 限制玩家位置在屏幕范围内
    /// </summary>
    private void ClampPlayerPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenBounds.x + screenPadding, screenBounds.x - screenPadding);
        pos.y = Mathf.Clamp(pos.y, -screenBounds.y + screenPadding, screenBounds.y - screenPadding);
        transform.position = pos;
    }

    /// <summary>
    /// 玩家受伤
    /// </summary>
    public void TakeDamage()
    {
        health--;
        GameManager.Instance.PlayerTakeDamage();
        
        // 播放伤害动画或音效
        AudioManager.Instance?.PlaySFX(Resources.Load<AudioClip>("Audio/SFX/Hit"));
        
        Debug.Log($"Player took damage! Health: {health}");
    }

    /// <summary>
    /// 回复生命值
    /// </summary>
    public void Heal(int amount = 1)
    {
        health = Mathf.Min(health + amount, GameManager.MAX_HEALTH);
    }

    public int Health => health;
}

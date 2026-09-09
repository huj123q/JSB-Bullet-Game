# 快速开始指南

## 项目设置步骤

### 1. 创建文件夹结构

在 `Assets` 文件夹中创建以下目录：

```
Assets/
├── Scripts/
│   ├── Core/
│   ├── Player/
│   ├── Bullets/
│   ├── Managers/
│   ├── UI/
│   ├── Config/
│   └── Data/
├── Scenes/
├── Prefabs/
├── Sprites/
├── Audio/
│   ├── Music/
│   └── SFX/
├── Materials/
└── Resources/
```

### 2. 创建精灵和图形

需要创建以下精灵：

**Sprites 文件夹：**
- `Circle.png` - 圆形弹幕（32x32 像素）
- `Square.png` - 方形弹幕（32x32 像素）
- `Player.png` - 玩家角色（48x48 像素）
- `Background.png` - 背景（可选）

**颜色设置：**
- 圆形：红色 (#FF0000)
- 方形：蓝色 (#0000FF)
- 玩家：绿色 (#00FF00)

### 3. 创建预制体

**Prefabs 文件夹中创建：**

#### Bullet.prefab
1. 创建空对象 `Bullet`
2. 添加 Sprite Renderer 组件
3. 添加 Rigidbody 2D 组件
4. 添加 Circle Collider 2D 组件
5. 添加 `Bullet.cs` 脚本
6. 设置标签为 "Bullet"

#### Player.prefab
1. 创建空对象 `Player`
2. 添加 Sprite Renderer 组件
3. 添加 Rigidbody 2D 组件
4. 添加 Circle Collider 2D 组件（勾选 Is Trigger）
5. 添加 `PlayerController.cs` 脚本
6. 设置标签为 "Player"

### 4. 创建场景

#### MainMenu.unity
1. 创建新场景
2. 添加 Canvas
3. 添加 Button（"开始游戏"、"设置"、"退出"）
4. 添加 Text（标题）
5. 添加 `SceneController.cs` 脚本

#### GameScene.unity
1. 创建新场景
2. 创建 Camera 并设置为正交模式
3. 创建 Player 预制体实例
4. 创建空对象 "BulletContainer"
5. 创建 Canvas 用于 UI
6. 添加以下空对象并挂载脚本：
   - `GameManager` (GameManager.cs)
   - `AudioManager` (AudioManager.cs)
   - `BulletManager` (BulletManager.cs)
   - `ScoreManager` (ScoreManager.cs)
   - `LevelManager` (LevelManager.cs)
   - `UIManager` (UIManager.cs)

### 5. 配置管理器

#### GameManager
- 确保为单例

#### AudioManager
- 添加两个 AudioSource 组件
- 关闭 "Play On Awake"

#### BulletManager
- 将 BulletContainer 拖放到 Bullet Container 字段
- 设置 Max Bullets 为 1000

#### LevelManager
- 添加示例关卡数据

#### UIManager
- 链接所有 UI 元素
- 设置 Canvas 的 Render Mode 为 Screen Space - Overlay

### 6. 项目设置

1. **Edit > Project Settings**

2. **Player**
   - 默认方向设置：自定义
   - 屏幕宽度：1920
   - 屏幕高度：1080

3. **Physics 2D**
   - Gravity: (0, 0)
   - Default Material - Friction: 0
   - Default Material - Bounciness: 0

4. **Audio**
   - 默认音量：1.0

### 7. 导入音乐和音效

创建以下文件夹结构并添加音频文件：

```
Assets/Audio/
├── Music/
│   ├── Level1.mp3
│   ├── Level2.mp3
│   └── ...
└── SFX/
    ├── Hit.wav
    ├── Dodge.wav
    ├── LevelComplete.wav
    └── GameOver.wav
```

### 8. 测试游戏

1. 打开 GameScene.unity
2. 按 Play 键运行
3. 使用 WASD 移动，Shift 快速移动
4. 避开弹幕获得分数
5. 按 ESC 暂停

## 常见问题

### Q: 脚本找不到？
A: 确保所有脚本都在 Assets/Scripts 文件夹中

### Q: 预制体显示紫色？
A: 需要为精灵分配材质或直接使用 Sprite Renderer

### Q: 弹幕不动？
A: 检查 Rigidbody 2D 的 Body Type 是否为 Dynamic

### Q: 玩家无法移动？
A: 检查 Camera.main 是否正确配置

## 下一步

1. 调整游戏平衡参数
2. 添加更多弹幕类型
3. 创建音乐可视化效果
4. 添加成就系统
5. 优化移动设备性能

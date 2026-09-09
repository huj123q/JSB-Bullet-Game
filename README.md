# JSB Bullet Game - Just Shapes & Beats 风格音乐弹幕游戏

一款由Unity开发的亚音游（Audio-Visual Game）弹幕躲避游戏，灵感来自加拿大Berzerk Studio的《Just Shapes & Beats》。

## 🎮 项目特性

- 🎵 **音乐同步**：游戏内容与音乐节奏完全同步
- 🎨 **极简美术风格**：彩色几何图形设计（受JSB启发）
- 🎮 **弹幕躲避玩法**：躲避复杂的弹幕图案
- 🎵 **节奏感强**：需要玩家根据音乐节拍反应
- 📊 **多个关卡**：包含多首歌曲和关卡
- 🏆 **记分系统**：评分和排行榜
- ⚙️ **易于扩展**：清晰的代码架构便于添加新内容

## 📋 项目结构

```
Assets/
├── Scripts/
│   ├── Core/              # 核心游戏逻辑
│   │   ├── GameManager.cs       # 游戏状态管理
│   │   ├── AudioManager.cs      # 音频管理
│   │   └── BulletManager.cs     # 弹幕管理
│   ├── Player/            # 玩家相关脚本
│   │   └── PlayerController.cs  # 玩家控制
│   ├── Bullets/           # 弹幕系统
│   │   ├── Bullet.cs            # 弹幕基类
│   │   ├── CircleBullet.cs      # 圆形弹幕
│   │   ├── SquareBullet.cs      # 方形弹幕
│   │   └── BulletFactory.cs     # 弹幕工厂
│   ├── UI/                # UI系统
│   │   ├── UIManager.cs         # UI管理
│   │   └── SceneController.cs   # 场景切换
│   ├── Managers/          # 游戏管理器
│   │   ├── ScoreManager.cs      # 分数系统
│   │   └── LevelManager.cs      # 关卡管理
│   ├── Config/            # 配置文件
│   │   └── GameConfig.cs        # 游戏常量配置
│   └── Data/              # 数据脚本
│       └── LevelDataGenerator.cs # 关卡生成器
├── Scenes/
│   ├── MainMenu.unity     # 主菜单场景
│   ├── GameScene.unity    # 游戏场景
│   └── GameOver.unity     # 游戏结束场景
├── Prefabs/               # 预制体
│   ├── Player.prefab      # 玩家预制体
│   └── Bullet.prefab      # 弹幕预制体
├── Sprites/               # 精灵图片
│   ├── Circle.png         # 圆形弹幕图
│   ├── Square.png         # 方形弹幕图
│   ├── Player.png         # 玩家角色图
│   └── Background.png     # 背景图
├── Audio/                 # 音频文件
│   ├── Music/             # 背景音乐
│   │   ├── Level1.mp3
│   │   └── ...
│   └── SFX/               # 音效
│       ├── Hit.wav
│       ├── Dodge.wav
│       └── ...
└── Materials/             # 材质
```

## 🛠️ 技术栈

- **游戏引擎**：Unity 2021.3 LTS 或更高版本
- **编程语言**：C# 9.0+
- **物理系统**：Unity Physics 2D
- **音频系统**：Unity Audio Engine
- **UI框架**：Unity UI (uGUI)

## 🚀 快速开始

### 系统要求
- **Unity**：2021.3 LTS 或更高版本
- **.NET Framework**：4.7.1 或更高版本
- **内存**：至少 2GB RAM
- **存储**：至少 2GB 可用空间

### 安装步骤

1. **克隆仓库**
```bash
git clone https://github.com/huj123q/JSB-Bullet-Game.git
cd JSB-Bullet-Game
```

2. **用Unity打开项目**
   - 打开 Unity Hub
   - 点击 "Add" 选择项目文件夹
   - 选择 Unity 2021.3 LTS 版本打开

3. **打开场景**
   - 在 Project 窗口中导航到 `Assets/Scenes/`
   - 双击打开 `GameScene.unity`

4. **运行游戏**
   - 点击 Play 按钮或按 `Ctrl+P` 运行

### 初始化项目

详见 [QUICKSTART.md](./QUICKSTART.md) 获取完整的项目设置指南。

## 🎮 游戏控制

| 操作 | 按键 |
|------|------|
| 上移 | W / ↑ |
| 下移 | S / ↓ |
| 左移 | A / ← |
| 右移 | D / → |
| 快速移动 | Shift |
| 暂停/恢复 | ESC |
| 手柄支持 | Xbox 控制器兼容 |

## 📚 核心系统说明

### 1. 弹幕系统
- **支持多种弹幕类型**：圆形、矩形、波形、螺旋等
- **动态对象池**：自动创建和回收弹幕
- **精确时间同步**：基于音乐时间的弹幕生成
- **高性能**：支持 1000+ 弹幕同时显示

### 2. 音乐同步
- **实时音乐时间追踪**：使用 `AudioSource.time`
- **延迟补偿**：配置 `audioDelayOffset` 补偿系统延迟
- **节奏检测**：精确的节拍点计算

### 3. 记分系统
- **躲避加分**：每躲避一发弹幕得分
- **连击倍数**：连续躲避得分逐级递增
- **关卡奖励**：完成关卡获得大量奖励
- **排行榜**：保存和显示玩家最高分

### 4. 关卡系统
- **动态生成**：关卡难度随序列递增
- **弹幕编排**：灵活的弹幕模式定制
- **多难度**：支持自定义关卡难度

## 🔧 开发指南

### 添加新弹幕类型

1. 在 `BulletType` 枚举中添加新类型
```csharp
public enum BulletType
{
    Circle,
    Square,
    Wave,      // 新增
    Spiral,    // 新增
    Laser      // 新增
}
```

2. 创建新的弹幕类（继承自 `Bullet`）
```csharp
public class WaveBullet : Bullet
{
    protected override void SetBulletColor()
    {
        spriteRenderer.color = Color.yellow;
    }
    
    public override void UpdateBullet()
    {
        base.UpdateBullet();
        // 添加波形运动逻辑
    }
}
```

3. 在 `BulletFactory` 中注册新类型
```csharp
private void InitializeBulletTypeMap()
{
    bulletTypeMap[BulletType.Wave] = typeof(WaveBullet);
}
```

### 创建新关卡

1. 准备音乐文件（MP3 或 WAV 格式）
2. 在 `LevelManager` 中添加关卡数据
3. 设计弹幕节奏表
4. 测试并调整关卡难度

详见 [DEVELOPMENT.md](./DEVELOPMENT.md)

## 📊 性能优化

### 已实现的优化
- ✅ 对象池管理弹幕
- ✅ 2D 精灵渲染而非 3D 模型
- ✅ 碰撞体合并减少物理计算
- ✅ Camera Culling 优化渲染
- ✅ 异步音乐加载

### 推荐设置
- 目标帧率：60 FPS（移动设备可降至 30 FPS）
- 垂直同步：关闭（为获得最高性能）
- 阴影：关闭
- 抗锯齿：2x MSAA

## 🐛 常见问题

### Q: 游戏延迟太高
**A:** 
1. 检查 Unity 是否以优化模式运行
2. 减少弹幕数量或禁用某些视觉效果
3. 检查 CPU 使用率是否过高
4. 在 Build Settings 中优化脚本编译

### Q: 音乐和弹幕不同步
**A:**
1. 调整 `AudioManager` 中的 `audioDelayOffset` 参数
2. 确保音乐文件采样率为 44.1kHz 或 48kHz
3. 检查 AudioSource 的设置（勾选 "Sync to Other Sources"）
4. 验证弹幕生成时间是否正确

### Q: 内存占用过高
**A:**
1. 减少 `MAX_BULLETS` 的值
2. 检查是否有资源未正确释放
3. 使用 Profiler 检查内存泄漏
4. 定期清理未使用的场景和资源

### Q: 精灵显示为紫色
**A:**
1. 检查精灵是否正确导入
2. 为精灵分配材质
3. 验证 Sprite Renderer 的着色器为 "Sprites/Default"
4. 确保 Import Settings 中 "Texture Type" 设置为 "Sprite (2D and UI)"

## 🎯 发展路线图

- [ ] **v0.2** - 更多弹幕类型和关卡
- [ ] **v0.3** - 音乐可视化效果
- [ ] **v0.4** - 成就系统
- [ ] **v0.5** - 多人模式
- [ ] **v1.0** - 完整游戏发布

### 计划功能
- 🎨 粒子系统和视觉效果增强
- 🎵 谱面编辑器
- 🏆 全球排行榜
- 🎮 更多游戏模式
- 📱 移动平台优化
- 🌍 多语言支持

## 📄 许可证

MIT License - 详见 [LICENSE](./LICENSE)

## 👥 贡献指南

欢迎提交 Issue 和 Pull Request！

### 贡献步骤
1. Fork 本仓库
2. 创建特性分支 (`git checkout -b feature/AmazingFeature`)
3. 提交更改 (`git commit -m 'Add some AmazingFeature'`)
4. 推送到分支 (`git push origin feature/AmazingFeature`)
5. 打开 Pull Request

### 代码规范
- 遵循 [DEVELOPMENT.md](./DEVELOPMENT.md) 中的代码规范
- 为所有公共方法添加 XML 注释
- 使用 PascalCase 命名类和方法
- 使用 camelCase 命名字段和局部变量

## 🙏 致谢

- **灵感来源**：[Just Shapes & Beats](https://www.justshapesandbeats.com/) by Berzerk Studio
- **音乐**：感谢所有使用的创意共用音乐
- **社区**：感谢 Unity 开发者社区的支持

## 📞 联系方式

- **问题报告**：[GitHub Issues](https://github.com/huj123q/JSB-Bullet-Game/issues)
- **功能建议**：[GitHub Discussions](https://github.com/huj123q/JSB-Bullet-Game/discussions)
- **电子邮件**：huj123q@users.noreply.github.com

## 📈 项目统计

```
总代码行数: ~2000+ 
核心脚本: 15+ 
场景: 3+ 
支持平台: Windows, macOS, Linux, WebGL, Android, iOS
最小 Unity 版本: 2021.3 LTS
```

---

**开发者**：huj123q

**最后更新**：2026年9月9日

**状态**：🟡 开发中

如果这个项目对您有帮助，请给个 ⭐ Star！

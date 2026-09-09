# 开发指南

## 项目架构

### 核心模块

#### 1. GameManager
主要游戏管理器，负责：
- 游戏状态管理（开始、运行、暂停、结束）
- 场景加载和卸载
- 全局数据管理

#### 2. AudioManager
音乐和音效管理：
- 加载和播放背景音乐
- 音乐时间同步
- 音效效果

#### 3. BulletManager
弹幕管理系统：
- 弹幕生成和销毁
- 弹幕运动更新
- 碰撞检测

#### 4. PlayerController
玩家角色控制：
- 移动和输入处理
- 碰撞判定
- 生命值管理

#### 5. ScoreManager
计分系统：
- 分数计算
- 连击系统
- 排行榜管理

## 开发流程

### 创建新关卡

1. 准备音乐文件（MP3或WAV格式）
2. 创建新Scene
3. 配置LevelData脚本
4. 设计弹幕节奏表
5. 测试和调整

### 弹幕节奏表格式

```csharp
[System.Serializable]
public class BulletPattern
{
    public float spawnTime;      // 生成时间（秒）
    public BulletType type;      // 弹幕类型
    public Vector3 position;     // 生成位置
    public Vector3 direction;    // 移动方向
    public float speed;          // 移动速度
    public float lifetime;       // 生存时间
}
```

### 添加新的弹幕类型

1. 在`BulletType`枚举中添加新类型
2. 创建新的Prefab
3. 在`BulletFactory`中添加生成逻辑
4. 实现自定义移动逻辑

## 代码规范

### 命名约定
- 类名：PascalCase（PlayerController、BulletManager）
- 方法名：PascalCase（OnPlayerHit、UpdateBullets）
- 字段名：camelCase（playerHealth、bulletSpeed）
- 常量：UPPER_SNAKE_CASE（MAX_HEALTH、BULLET_POOL_SIZE）

### 注释规范
```csharp
/// <summary>
/// 简要说明方法功能
/// </summary>
/// <param name="parameter">参数说明</param>
/// <returns>返回值说明</returns>
public void ExampleMethod(int parameter)
{
    // 实现代码
}
```

## 性能优化

### 对象池
- 使用对象池管理弹幕，避免频繁的创建和销毁
- 预分配弹幕对象

### 渲染优化
- 使用Sprite Renderer而非3D模型
- 合并物理体以减少碰撞检测
- 使用Camera Culling

### 内存管理
- 及时销毁不需要的对象
- 避免Update中的频繁分配
- 使用对象池

## 调试技巧

### 音乐同步调试
```csharp
// 在AudioManager中打印当前时间
Debug.Log($"Current Time: {audioSource.time:F2}s");
```

### 弹幕生成调试
```csharp
// 启用弹幕路径绘制
void OnDrawGizmos()
{
    // 绘制弹幕运动路径
}
```

## 常见问题

### Q: 游戏延迟太高
A: 检查帧率是否稳定，优化弹幕数量，减少物理计算

### Q: 音乐和弹幕不同步
A: 调整音乐延迟偏移值，检查AudioSource设置

### Q: 内存占用过高
A: 增加对象池大小的初值，检查是否有内存泄漏

## 发布检查清单

- [ ] 所有关卡已测试
- [ ] 性能检查完成
- [ ] 音乐同步验证
- [ ] UI本地化完成
- [ ] 构建设置配置
- [ ] 平台特定优化

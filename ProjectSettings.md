# JSB Bullet Game 项目配置

## Unity 版本
- 推荐版本：2021.3 LTS 或更高
- 最低版本：2020.3 LTS

## 项目设置

### 分辨率
- 推荐：1920 x 1080 (16:9)
- 支持：1280 x 720, 2560 x 1440

### 物理系统
- 物理类型：2D
- 重力：(0, 0)
- 默认材质：无摩擦力

### 层级设置
- Player: 玩家层
- Bullet: 弹幕层
- UI: UI层

### 标签设置
- Player: 玩家标签
- Bullet: 弹幕标签

## 性能设置

### 目标帧率
- PC: 60 FPS
- 移动设备: 30-60 FPS

### 质量设置
- 抗锯齿：2x MSAA
- 阴影：关闭
- 粒子系统：最小化

## 音频设置

### 音乐
- 格式：MP3 或 WAV
- 比特率：192 kbps 或更高
- 采样率：44.1 kHz 或 48 kHz

### 音效
- 格式：WAV
- 比特率：16-bit PCM
- 采样率：44.1 kHz

## 构建设置

### 目标平台
- Windows (x86_64)
- macOS (Intel 和 Apple Silicon)
- Linux
- WebGL
- Android
- iOS

### 脚本后端
- Mono (推荐)
- IL2CPP (用于移动设备)

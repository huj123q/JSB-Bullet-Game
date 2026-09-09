using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 音频管理器 - 处理游戏的音乐和音效
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private float audioDelayOffset = 0.05f; // 音频延迟补偿（秒）

    private float musicStartTime = 0f;
    private AudioClip currentMusicClip;
    private bool isMusicPlaying = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.playOnAwake = false;
        sfxSource.playOnAwake = false;
    }

    /// <summary>
    /// 播放游戏音乐
    /// </summary>
    public void PlayGameMusic(AudioClip clip = null)
    {
        if (clip != null)
        {
            currentMusicClip = clip;
            musicSource.clip = clip;
        }

        if (musicSource.clip != null)
        {
            musicStartTime = Time.time;
            musicSource.Play();
            isMusicPlaying = true;
        }
    }

    /// <summary>
    /// 暂停音乐
    /// </summary>
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPlaying = false;
        }
    }

    /// <summary>
    /// 恢复音乐
    /// </summary>
    public void ResumeMusic()
    {
        if (!musicSource.isPlaying && currentMusicClip != null)
        {
            musicSource.Play();
            isMusicPlaying = true;
        }
    }

    /// <summary>
    /// 停止音乐
    /// </summary>
    public void StopMusic()
    {
        musicSource.Stop();
        isMusicPlaying = false;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    /// <summary>
    /// 获取当前音乐时间（考虑延迟补偿）
    /// </summary>
    public float GetMusicTime()
    {
        if (!isMusicPlaying)
            return 0f;

        return musicSource.time;
    }

    /// <summary>
    /// 获取预测的音乐时间（用于生成弹幕）
    /// </summary>
    public float GetPredictedMusicTime()
    {
        return GetMusicTime() + audioDelayOffset;
    }

    public bool IsMusicPlaying => isMusicPlaying;
    public AudioSource MusicSource => musicSource;
}

using UnityEngine;

/// <summary>
/// BGM管理クラス
/// </summary>
static public class BgmManager
{
    //BGMのAudioSource
    static AudioSource m_BgmAud = null;

    /// <summary>
    /// BGMのボリュームをセット
    /// </summary>
    /// <param name="_Volume">ボリューム</param>
    static public void SetVolume(float _Volume)
    {
        SoundManager.GetInstance().SetVolume("BGM", _Volume);
    }

    /// <summary>
    /// BGMのボリュームの取得
    /// </summary>
    /// <returns>BGMのボリューム</returns>
    static public float GetVolume()
    {
        return SoundManager.GetInstance().GetVolume("BGM");
    }

    /// <summary>
    /// AudioClipのセット
    /// </summary>
    /// <param name="_Clip">AudioClip</param>
    static public void SetAudioClip(AudioClip _Clip)
    {
        if (!m_BgmAud) AudioSourceGenerate();
        m_BgmAud.clip = _Clip;
    }

    /// <summary>
    /// 再生
    /// </summary>
    static public void Play()
    {
        if (!m_BgmAud) return;
        m_BgmAud.Play();
    }

    /// <summary>
    /// 停止
    /// </summary>
    static public void Stop()
    {
        if (!m_BgmAud) return;
        m_BgmAud.Stop();
    }

    /// <summary>
    /// AUdioSourceを生成する
    /// </summary>
    static void AudioSourceGenerate()
    {
        if (m_BgmAud) return;
        m_BgmAud = DontDestroyObject.Generate("BGM").AddComponent<AudioSource>();
        m_BgmAud.loop = true;
        m_BgmAud.outputAudioMixerGroup = SoundManager.GetInstance().GetAudioMixerGroup("BGM");
    }
}
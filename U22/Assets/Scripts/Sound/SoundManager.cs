using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// サウンドの管理クラス
/// </summary>
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField, Header("AudioMixer")]
    AudioMixer m_AudioMixer = null;

    /// <summary>
    /// ボリュームのセット
    /// </summary>
    /// <param name="_Name">エクスポート名</param>
    /// <param name="_Volume">ボリューム</param>
    public void SetVolume(string _Name, float _Volume)
    {
        m_AudioMixer.SetFloat(_Name, Mathf.Clamp01(_Volume) * 80.0f - 80.0f);
    }

    /// <summary>
    /// ボリュームの取得
    /// </summary>
    /// <param name="_Name">エクスポート名</param>
    /// <returns>ボリューム</returns>
    public float GetVolume(string _Name)
    {
        float volume;
        bool isGet = m_AudioMixer.GetFloat(_Name, out volume);
        return isGet ? ((volume + 80.0f) / 80.0f) : 0.0f;
    }

    /// <summary>
    /// AudioMixerGroupを取得
    /// </summary>
    /// <param name="_Name">エクスポート名</param>
    /// <returns>AudioMixerGroup</returns>
    public AudioMixerGroup GetAudioMixerGroup(string _Name)
    {
        return m_AudioMixer.FindMatchingGroups(_Name)[0];
    }
}

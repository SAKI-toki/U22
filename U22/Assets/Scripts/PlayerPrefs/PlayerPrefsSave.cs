using UnityEngine;

/// <summary>
/// PlayerPrefsのSaveのラッピング
/// </summary>
static public class PlayerPrefsSave
{
    /// <summary>
    /// int型のSave
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <param name="_N">Saveする値</param>
    static public void SaveInt(string _Key, int _N)
    {
        PlayerPrefs.SetInt(_Key, _N);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// float型のSave
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <param name="_N">Saveする値</param>
    static public void SaveFloat(string _Key, float _N)
    {
        PlayerPrefs.SetFloat(_Key, _N);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// string型のSave
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <param name="_N">Saveする値</param>
    static public void SaveString(string _Key, string _N)
    {
        PlayerPrefs.SetString(_Key, _N);
        PlayerPrefs.Save();
    }
}
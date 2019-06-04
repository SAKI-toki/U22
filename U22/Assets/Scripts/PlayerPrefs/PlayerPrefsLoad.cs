using UnityEngine;

/// <summary>
/// PlayerPrefsのGetのラッピング
/// </summary>
static public class PlayerPrefsLoad
{
    /// <summary>
    /// 指定されたキーが存在するかどうか(int,float,stringの型の指定はしない(出来ない))
    /// </summary>
    /// <param name="_Key">存在をチェックするキー</param>
    /// <returns>存在するならtrue</returns>
    static public bool HasKey(string _Key)
    {
        return PlayerPrefs.HasKey(_Key);
    }

    /// <summary>
    /// int型の読み込み
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <returns>キーに対応した値</returns>
    static public int LoadInt(string _Key)
    {
        if (!HasKey(_Key)) throw new System.Collections.Generic.KeyNotFoundException();
        return PlayerPrefs.GetInt(_Key);
    }

    /// <summary>
    /// int型の読み込み
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <returns>キーに対応した値</returns>
    static public float LoadFloat(string _Key)
    {
        if (!HasKey(_Key)) throw new System.Collections.Generic.KeyNotFoundException();
        return PlayerPrefs.GetFloat(_Key);
    }

    /// <summary>
    /// int型の読み込み
    /// </summary>
    /// <param name="_Key">キー</param>
    /// <returns>キーに対応した値</returns>
    static public string LoadString(string _Key)
    {
        if (!HasKey(_Key)) throw new System.Collections.Generic.KeyNotFoundException();
        return PlayerPrefs.GetString(_Key);
    }
}
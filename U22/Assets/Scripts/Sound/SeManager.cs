/// <summary>
/// SE管理クラス
/// </summary>
static public class SeManager
{
    /// <summary>
    /// SEのボリュームをセット
    /// </summary>
    /// <param name="_Volume">ボリューム</param>
    static public void SetVolume(float _Volume)
    {
        SoundManager.GetInstance().SetVolume("SE", _Volume);
    }

    /// <summary>
    /// SEのボリュームの取得
    /// </summary>
    /// <returns>SEのボリューム</returns>
    static public float GetVolume()
    {
        return SoundManager.GetInstance().GetVolume("SE");
    }
}
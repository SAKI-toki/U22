/// <summary>
/// Master管理クラス
/// </summary>
static public class MasterManager
{
    /// <summary>
    /// Masterのボリュームをセット
    /// </summary>
    /// <param name="_Volume">ボリューム</param>
    static public void SetVolume(float _Volume)
    {
        SoundManager.GetInstance().SetVolume("Master", _Volume);
    }

    /// <summary>
    /// Masterのボリュームの取得
    /// </summary>
    /// <returns>Masterのボリューム</returns>
    static public float GetVolume()
    {
        return SoundManager.GetInstance().GetVolume("Master");
    }
}
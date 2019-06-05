using UnityEngine;

/// <summary>
/// 各設定を保持するクラス
/// </summary> 
public class Config : Singleton<Config>
{
    [SerializeField, Header("プレイヤーオブジェクト")]
    GameObject m_PlayerObject = null;

    /// <summary>
    /// プレイヤーオブジェクトのプロパティ
    /// </summary>
    /// <value>プレイヤーオブジェクト</value>
    public GameObject PlayerObject { get { return this.m_PlayerObject; } }

    public override void MyStart()
    {
        if (!m_PlayerObject.GetComponent<PlayerController>())
        {
            Debug.LogError("プレイヤーオブジェクトにPlayerControllerがComponentされていません");
        }
    }
}
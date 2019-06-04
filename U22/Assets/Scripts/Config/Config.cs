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


    [SerializeField, Header("プレイヤーの移動速度")]
    float m_PlayerMoveSpeed = 0.0f;

    /// <summary>
    /// プレイヤーの移動速度のプロパティ
    /// </summary>
    /// <value>プレイヤーの移動速度</value>
    public float PlayerMoveSpeed { get { return this.m_PlayerMoveSpeed; } }

    public override void MyStart()
    {
        if (!m_PlayerObject.GetComponent<PlayerController>())
        {
            Debug.LogError("プレイヤーオブジェクトにPlayerControllerがComponentされていません");
        }
        if (m_PlayerMoveSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの移動速度が0以下です");
        }
    }
}
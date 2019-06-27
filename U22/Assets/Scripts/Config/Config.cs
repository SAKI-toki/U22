using UnityEngine;

/// <summary>
/// 各設定を保持するクラス
/// </summary> 
public class Config : Singleton<Config>
{
    [SerializeField, Header("プレイヤーの移動速度")]
    float m_PlayerMoveSpeed = 0.0f;
    public float PlayerMoveSpeed { get { return m_PlayerMoveSpeed; } }
    [SerializeField, Header("プレイヤーの回転速度")]
    float m_PlayerRotationSpeed = 0.0f;
    public float PlayerRotationSpeed { get { return m_PlayerRotationSpeed; } }
    [SerializeField, Header("プレイヤーの接続時の回転速度")]
    float m_ConnectPlayerRotationSpeed = 0.0f;
    public float ConnectPlayerRotationSpeed { get { return m_ConnectPlayerRotationSpeed; } }
    [SerializeField, Header("ジャイロのデッドゾーン")]
    float m_GyroDeadZone = 0.0f;
    public float GyroDeadZone { get { return m_GyroDeadZone; } }

    public override void MyStart()
    {
        if (m_PlayerMoveSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの移動速度が0以下です");
        }
        if (m_PlayerRotationSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの回転速度が0以下です");
        }
        if (m_ConnectPlayerRotationSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの接続時の回転速度が0以下です");
        }
        if (m_GyroDeadZone <= 0.0f)
        {
            Debug.LogError("ジャイロのデッドゾーンが0以下です");
        }
    }
}
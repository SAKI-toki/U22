using UnityEngine;

public class ConnectPlayerController : MonoBehaviour
{
    [SerializeField, Header("プレイヤー1")]
    GameObject m_Player1 = null;
    [SerializeField, Header("プレイヤー2")]
    GameObject m_Player2 = null;
    [SerializeField, Header("接続の当たり判定の管理クラス")]
    ConnectColliderManager m_ConnectColliderManager = null;

    void Start()
    {
        ErrorCheck();
    }

    void Update()
    {
        if (!m_ConnectColliderManager.IsConnect) return;
        Movement();
        Rotation();
    }

    void Movement()
    {
        //各コントローラーの水平、垂直
        float horizontal1 = SwitchInput.GetHorizontal(0);
        float horizontal2 = SwitchInput.GetHorizontal(1);
        float vertical1 = SwitchInput.GetVertical(0);
        float vertical2 = SwitchInput.GetVertical(1);
        //平均
        float avgHorizontal = (horizontal1 + horizontal2) / 2;
        float avgVertical = (vertical1 + vertical2) / 2;
        //移動
        m_Player1.transform.position = m_Player1.transform.position +
            new Vector3(avgHorizontal, avgVertical, 0.0f) * Config.GetInstance().PlayerMoveSpeed;
        m_Player2.transform.position = m_Player2.transform.position +
            new Vector3(avgHorizontal, avgVertical, 0.0f) * Config.GetInstance().PlayerMoveSpeed;
    }

    void Rotation()
    {
        //ジャイロのリセット
        if (SwitchInput.GetButtonDown(0, SwitchButton.Up))
            SwitchGyro.SetBaseGyro(0);
        if (SwitchInput.GetButtonDown(1, SwitchButton.Up))
            SwitchGyro.SetBaseGyro(1);
        //ジャイロの取得
        float gyro1 = SwitchGyro.GetGyroX(0);
        float gyro2 = SwitchGyro.GetGyroX(1);
        //デッドゾーン
        if (Mathf.Abs(gyro1) > Config.GetInstance().GyroDeadZone)
        {
            m_Player1.transform.RotateAround(m_ConnectColliderManager.GetConnectPosition(true), Vector3.forward,
                gyro1 / 100 * Config.GetInstance().ConnectPlayerRotationSpeed);
        }
        if (Mathf.Abs(gyro2) > Config.GetInstance().GyroDeadZone)
        {
            m_Player2.transform.RotateAround(m_ConnectColliderManager.GetConnectPosition(false), Vector3.forward,
                gyro2 / 100 * Config.GetInstance().ConnectPlayerRotationSpeed);
        }
    }

    /// <summary>
    /// エラーチェック
    /// </summary>
    void ErrorCheck()
    {
        if (!m_Player1)
        {
            Debug.LogError("プレイヤー1が入っていません");
        }
        if (!m_Player2)
        {
            Debug.LogError("プレイヤー2が入っていません");
        }
        if (!m_ConnectColliderManager)
        {
            Debug.LogError("接続の当たり判定の管理クラスが入っていません");
        }
    }
}
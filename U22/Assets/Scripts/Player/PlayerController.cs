using UnityEngine;

/// <summary>
/// プレイヤーを制御するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの番号")]
    int m_ThisPlayerNumber = 0;
    [SerializeField, Header("腕の制御")]
    ArmManager m_ArmManager = new ArmManager();
    [SerializeField, Header("接続の当たり判定の管理クラス")]
    ConnectColliderManager m_ConnectColliderManager = null;

    void Start()
    {
        ErrorCheck();
        //腕の初期化
        m_ArmManager.ArmInitialize(m_ThisPlayerNumber);
    }

    void Update()
    {
        if (m_ConnectColliderManager.IsConnect) return;
        Movement();
        m_ArmManager.ArmUpdate();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Movement()
    {
        float horizontal = SwitchInput.GetHorizontal(m_ThisPlayerNumber);
        float vertical = SwitchInput.GetVertical(m_ThisPlayerNumber);
        //移動量を保持
        Vector3 m_MoveValue = new Vector3(horizontal, vertical, 0.0f);
        transform.position = transform.position + m_MoveValue * Config.GetInstance().PlayerMoveSpeed;
        //移動方向を向く
        Rotation(m_MoveValue);
    }

    /// <summary>
    /// 移動方向を向く回転処理
    /// </summary>
    /// <param name="_MoveDirection">移動方向</param>
    void Rotation(Vector2 _MoveDirection)
    {
        if (_MoveDirection == Vector2.zero) return;
        //終了の回転
        Quaternion endQuaternion = Quaternion.Euler(0.0f, 0.0f,
            Mathf.Atan2(_MoveDirection.y, _MoveDirection.x) * Mathf.Rad2Deg - 90.0f);
        //補間したZを取得
        var rot = Quaternion.Lerp(transform.rotation, endQuaternion,
        Config.GetInstance().PlayerRotationSpeed / 100 * _MoveDirection.magnitude);
        //移動方向を向く
        transform.rotation = rot;
    }

    /// <summary>
    /// エラーチェック
    /// </summary>
    void ErrorCheck()
    {
        if (!m_ConnectColliderManager)
        {
            Debug.LogError("接続の当たり判定の管理クラスが入っていません");
        }
    }
}
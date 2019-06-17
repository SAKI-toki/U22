using UnityEngine;

/// <summary>
/// プレイヤーを制御するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの番号")]
    int m_ThisPlayerNumber = 0;
    [SerializeField, Header("プレイヤーの移動速度")]
    float m_PlayerMoveSpeed = 0.0f;
    [SerializeField, Header("プレイヤーの回転速度")]
    float m_PlayerRotationSpeed = 0.0f;
    [SerializeField, Header("腕の制御")]
    ArmManager m_ArmManager = new ArmManager();
    Rigidbody2D m_RigidBody = null;
    //移動量を保持
    Vector2 m_MoveValue = new Vector2();

    void Start()
    {
        if (m_PlayerMoveSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの移動速度が0以下です");
        }
        if (m_PlayerRotationSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの回転速度が0以下です");
        }
        //腕の初期化
        m_ArmManager.ArmInitialize(m_ThisPlayerNumber);
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        m_ArmManager.ArmUpdate();
    }

    void FixedUpdate()
    {
        //移動
        m_RigidBody.MovePosition(m_RigidBody.position + m_MoveValue * Time.deltaTime * m_PlayerMoveSpeed);
        //移動方向を向く
        Rotation(m_MoveValue);
        m_MoveValue = Vector2.zero;
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Movement()
    {
        float horizontal = SwitchInput.GetHorizontal(m_ThisPlayerNumber);
        float vertical = SwitchInput.GetVertical(m_ThisPlayerNumber);
        //移動量
        m_MoveValue += new Vector2(horizontal, vertical);
    }

    /// <summary>
    /// 移動方向を向く回転処理
    /// </summary>
    /// <param name="_MoveDirection">移動方向</param>
    void Rotation(Vector2 _MoveDirection)
    {
        if (_MoveDirection == Vector2.zero)
        {
            m_RigidBody.MoveRotation(m_RigidBody.rotation);
            return;
        }
        //始まりと終わりの回転をセット
        Quaternion startQuaternion = Quaternion.Euler(0.0f, 0.0f, m_RigidBody.rotation);
        Quaternion endQuaternion = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(_MoveDirection.y, _MoveDirection.x) * Mathf.Rad2Deg - 90.0f);
        //補間したZを取得
        float rotZ = Quaternion.Lerp(startQuaternion, endQuaternion,
        m_PlayerRotationSpeed / Quaternion.Angle(startQuaternion, endQuaternion) * _MoveDirection.magnitude).eulerAngles.z;
        //移動方向を向く
        m_RigidBody.MoveRotation(rotZ);
    }
}
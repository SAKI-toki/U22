using UnityEngine;

/// <summary>
/// プレイヤーを制御するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    //プレイヤーの番号
    int m_ThisPlayerNumber = 0;
    //プレイヤーの番号のプロパティ
    public int ThisPlayerNumber { set { this.m_ThisPlayerNumber = value; } }

    [SerializeField, Header("プレイヤーの移動速度")]
    float m_PlayerMoveSpeed = 0.0f;
    [SerializeField, Header("腕の制御")]
    ArmManager m_ArmController = new ArmManager();

    void Start()
    {
        if (m_PlayerMoveSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの移動速度が0以下です");
        }
        //腕の初期化
        m_ArmController.ArmInitialize(m_ThisPlayerNumber);
    }

    void Update()
    {
        Movement();
        m_ArmController.ArmUpdate();
    }

    /// <summary>
    /// 移動処理
    /// </summary>
    void Movement()
    {
        float horizontal = SwitchInput.GetHorizontal(m_ThisPlayerNumber);
        float vertical = SwitchInput.GetVertical(m_ThisPlayerNumber);
        //入力がない場合は処理しない
        if (horizontal == 0.0f && vertical == 0.0f) return;
        //移動量
        var moveValue = new Vector3(horizontal, vertical, 0.0f);
        //移動
        transform.position += moveValue * Time.deltaTime * m_PlayerMoveSpeed;
        //移動方向を向く
        Rotation(moveValue.normalized);
    }

    /// <summary>
    /// 移動方向を向く回転処理
    /// </summary>
    /// <param name="_MoveDirection">移動方向</param>
    void Rotation(Vector3 _MoveDirection)
    {
        //移動方向を向く
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Mathf.Atan2(_MoveDirection.y, _MoveDirection.x) * Mathf.Rad2Deg - 90.0f);
    }
}
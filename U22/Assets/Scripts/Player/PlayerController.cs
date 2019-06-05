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
    ArmController m_ArmController = new ArmController();

    void Start()
    {
        if (m_PlayerMoveSpeed <= 0.0f)
        {
            Debug.LogError("プレイヤーの移動速度が0以下です");
        }
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
        //移動量
        var moveValue = new Vector3(SwitchInput.GetHorizontal(m_ThisPlayerNumber), SwitchInput.GetVertical(m_ThisPlayerNumber), 0.0f);
        //移動
        transform.Translate(moveValue * Time.deltaTime * m_PlayerMoveSpeed);
    }
}

/// <summary>
/// 腕の制御
/// </summary>
[System.Serializable]
public class ArmController
{
    //現在の回転(z軸)
    float m_RotationValue = 0.0f;
    [SerializeField, Header("回転速度")]
    float m_RotationSpeed = 0.0f;
    [SerializeField, Header("最小角度")]
    float m_MinAngle = 0.0f;
    [SerializeField, Header("最大角度")]
    float m_MaxAngle = 0.0f;
    [SerializeField, Header("右腕")]
    GameObject m_RightArmObject = null;
    [SerializeField, Header("左腕")]
    GameObject m_LeftArmObject = null;

    int m_PlayerNumber = 0;

    /// <summary>
    /// 腕の初期化
    /// </summary>
    /// <param name="_PlayerNumber">プレイヤーの番号</param>
    public void ArmInitialize(int _PlayerNumber)
    {
        m_PlayerNumber = _PlayerNumber;
        //エラーチェックやNullチェック
        if (m_RotationSpeed <= 0.0f)
        {
            Debug.LogError("腕の回転速度が0以下です");
        }
        if (m_MinAngle > m_MaxAngle)
        {
            Debug.LogError("最小角度が最大角度より大きいです");
        }
        if (!m_RightArmObject)
        {
            Debug.LogError("右腕のオブジェクトがありません");
        }
        if (!m_LeftArmObject)
        {
            Debug.LogError("左腕のオブジェクトがありません");
        }
    }

    /// <summary>
    /// 腕の更新
    /// </summary>
    public void ArmUpdate()
    {
        ArmRotation();
    }

    /// <summary>
    /// 回転処理
    /// </summary>
    void ArmRotation()
    {
        //Rなら開く
        if (SwitchInput.GetButton(m_PlayerNumber, SwitchButton.SR))
        {
            m_RotationValue += m_RotationSpeed * Time.deltaTime;
            Debug.Log("sr");
        }
        //Lなら閉じる
        if (SwitchInput.GetButton(m_PlayerNumber, SwitchButton.SL))
        {
            m_RotationValue -= m_RotationSpeed * Time.deltaTime;
            Debug.Log("sl");
        }
        //最小、最大角度にClamp
        m_RotationValue = Mathf.Clamp(m_RotationValue, m_MinAngle, m_MaxAngle);
        //角度を設定
        m_RightArmObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -m_RotationValue);
        m_LeftArmObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_RotationValue);
    }
}
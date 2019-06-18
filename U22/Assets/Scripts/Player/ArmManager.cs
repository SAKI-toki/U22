using UnityEngine;

/// <summary>
/// 腕の管理クラス
/// </summary>
[System.Serializable]
public class ArmManager
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
        ErrorCheck();
    }

    /// <summary>
    /// 腕の更新
    /// </summary>
    public void ArmUpdate()
    {
        ArmRotation();
    }

    /// <summary>
    /// 腕の固定更新
    /// </summary>
    public void ArmFixedUpdate()
    {
        //最小、最大角度にClamp
        m_RotationValue = Mathf.Clamp(m_RotationValue, m_MinAngle, m_MaxAngle);
        //角度を設定
        m_RightArmObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, -m_RotationValue);
        m_LeftArmObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, m_RotationValue);
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
        }
        //Lなら閉じる
        if (SwitchInput.GetButton(m_PlayerNumber, SwitchButton.SL))
        {
            m_RotationValue -= m_RotationSpeed * Time.deltaTime;
        }
    }

    /// <summary>
    /// エラーチェック
    /// </summary>
    void ErrorCheck()
    {
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
}
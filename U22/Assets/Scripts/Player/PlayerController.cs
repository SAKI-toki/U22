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

    void Start()
    {

    }

    void Update()
    {
        var moveValue = new Vector3(SwitchInput.GetHorizontal(m_ThisPlayerNumber), SwitchInput.GetVertical(m_ThisPlayerNumber), 0.0f);
        transform.Translate(moveValue * Time.deltaTime * Config.GetInstance().PlayerMoveSpeed);
    }
}
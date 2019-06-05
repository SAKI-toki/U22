using UnityEngine;

public class PlayerNumberCheckManager : MonoBehaviour
{
    //プレイヤーの人数
    static public int m_PlayerNumber = 0;
    [SerializeField]
    InputPlayerNumber m_InputPlayerNumber = null;

    IPlayerNumberCheckStateBase m_State;

    void Start()
    {
        m_State = m_InputPlayerNumber;
        m_State.StateInit();
    }

    void Update()
    {
        var _NextState = m_State.StateUpdate();
        if (_NextState != m_State)
        {
            m_State.StateDestroy();
            m_State = _NextState;
            m_State.StateInit();
        }
    }
}

/// <summary>
/// プレイヤーの人数をチェックするステートの基底クラス
/// </summary>
public interface IPlayerNumberCheckStateBase
{
    void StateInit();
    IPlayerNumberCheckStateBase StateUpdate();
    void StateDestroy();
}
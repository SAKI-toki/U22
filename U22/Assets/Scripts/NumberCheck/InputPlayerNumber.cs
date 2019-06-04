using UnityEngine;

/// <summary>
/// プレイヤーの人数を入力するクラス
/// </summary>
public class InputPlayerNumber : MonoBehaviour, IPlayerNumberCheckStateBase
{
    [SerializeField, Header("プレイ人数のオブジェクト")]
    GameObject[] m_PlayerNumObject = null;
    [SerializeField, Header("カーソル")]
    GameObject m_CursorObject = null;
    [SerializeField, Header("次のステート")]
    InputPlayerCheck m_NextState = null;
    //現在選んでいるオブジェクト
    int m_Index = 0;

    void Start()
    {
        SetActiveNumberObject(false);
    }

    public void StateInit()
    {
        SetActiveNumberObject(true);
        CursorUpdate();
        //オブジェクトにPlayerNumberObjectがComponentされているかどうかチェックする
        foreach (var obj in m_PlayerNumObject)
        {
            if (!obj.GetComponent<PlayerNumberObject>())
                Debug.LogError("PlayerNumberObjectがComponentされてません");
        }
    }

    public IPlayerNumberCheckStateBase StateUpdate()
    {
        //カーソルの移動
        if (SwitchInput.GetButtonDown(0, SwitchButton.StickRight))
        {
            ++m_Index;
        }
        if (SwitchInput.GetButtonDown(0, SwitchButton.StickLeft))
        {
            --m_Index;
        }
        //範囲内に収める
        m_Index = Mathf.Clamp(m_Index, 0, m_PlayerNumObject.Length - 1);
        CursorUpdate();

        //決定
        if (SwitchInput.GetButtonDown(0, SwitchButton.Ok))
        {
            //プレイ人数を静的変数に格納
            PlayerNumberCheckManager.m_PlayerNumber =
                m_PlayerNumObject[m_Index].GetComponent<PlayerNumberObject>().m_PlayerNumber;
            return m_NextState;
        }

        return this;
    }

    public void StateDestroy()
    {
        SetActiveNumberObject(false);
    }

    /// <summary>
    /// カーソルの更新
    /// </summary>
    void CursorUpdate()
    {
        m_CursorObject.transform.position = m_PlayerNumObject[m_Index].transform.position;
    }

    /// <summary>
    /// アクティブのセット
    /// </summary>
    /// <param name="_IsActive">アクティブにするかどうか</param>
    void SetActiveNumberObject(bool _IsActive)
    {
        foreach (var obj in m_PlayerNumObject)
        {
            obj.SetActive(_IsActive);
        }
        m_CursorObject.SetActive(_IsActive);
    }
}
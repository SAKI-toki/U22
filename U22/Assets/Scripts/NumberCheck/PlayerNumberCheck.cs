using UnityEngine;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// プレイヤーが二人いるか確認する
/// </summary>
public class PlayerNumberCheck : MonoBehaviour
{
    //プレイヤーの数
    public const int m_PlayerNumber = 2;
    [SerializeField, Header("入力をチェックするオブジェクト")]
    GameObject[] m_InputCheckObject = new GameObject[m_PlayerNumber];

    //各プレイヤーがOKかどうかを格納する配列
    bool[] m_IsOk = new bool[m_PlayerNumber];

    void Start()
    {
        foreach (var obj in m_InputCheckObject)
        {
            obj.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    void Update()
    {
        for (int i = 0; i < m_PlayerNumber; ++i)
        {
            //既にOKならcontinue
            if (m_IsOk[i]) continue;
            if (SwitchInput.GetButtonDown(i, SwitchButton.Ok))
                SetOk(i);
        }

        //全てOKか確認する
        try
        {
            foreach (var isOk in m_IsOk)
            {
                if (!isOk) throw new Exception();
            }
            //全てOKならシーン遷移
            SceneManager.LoadScene("GameScene");
        }
        catch (Exception) { }
    }

    /// <summary>
    /// OKになったら各情報を更新する
    /// </summary>
    /// <param name="_Index">コントローラーの番号</param>
    void SetOk(int _Index)
    {
        m_IsOk[_Index] = true;
        m_InputCheckObject[_Index].GetComponent<SpriteRenderer>().color = Color.white;
    }
}
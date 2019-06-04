using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 入力でプレイヤーがそろっているか確認するクラス
/// </summary>
public class InputPlayerCheck : MonoBehaviour, IPlayerNumberCheckStateBase
{
    [SerializeField, Header("入力チェックオブジェクト")]
    GameObject[] m_InputCheckObject = new GameObject[4];
    [SerializeField, Header("前のステート")]
    InputPlayerNumber m_InputPlayerNumber = null;
    //Okを入力したかのフラグを管理する
    bool[] m_IsInputCheckOk;

    void Start()
    {
        SetActiveNumberObject(false);
    }

    public void StateInit()
    {
        //プレイ人数分配列の要素確保
        m_IsInputCheckOk = new bool[PlayerNumberCheckManager.m_PlayerNumber];
        SetinputObjectPosition(PlayerNumberCheckManager.m_PlayerNumber);
        SetActiveNumberObject(true, PlayerNumberCheckManager.m_PlayerNumber);
    }

    public IPlayerNumberCheckStateBase StateUpdate()
    {
        //キャンセルしたら一つ前のステートに戻る
        if (SwitchInput.GetButtonDown(0, SwitchButton.Cancel))
        {
            return m_InputPlayerNumber;
        }
        //各プレイヤーの入力チェック
        for (int i = 0; i < PlayerNumberCheckManager.m_PlayerNumber; ++i)
        {
            //既にOKならcontinue
            if (m_IsInputCheckOk[i]) continue;
            //OKを押したらフラグを立てる
            if (SwitchInput.GetButtonDown(i, SwitchButton.Ok))
            {
                m_IsInputCheckOk[i] = true;
                CheckChangeObject(m_InputCheckObject[i]);
            }
        }
        try
        {
            foreach (var isOk in m_IsInputCheckOk)
            {
                //OKじゃないのがあればシーン遷移しない
                if (!isOk)
                    throw new System.Exception();
            }
            //全てOKならシーン遷移
            SceneManager.LoadScene("GameScene");
        }
        catch (System.Exception)
        { }
        return this;
    }

    public void StateDestroy()
    {
        SetActiveNumberObject(false);
    }

    /// <summary>
    /// 入力確認のオブジェクトの位置のセット
    /// </summary>
    /// <param name="_PlayerNumber">プレイ人数</param>
    void SetinputObjectPosition(int _PlayerNumber)
    {
        switch (_PlayerNumber)
        {
            case 2:
                m_InputCheckObject[0].transform.position = new Vector3(-3, 0, 0);
                m_InputCheckObject[1].transform.position = new Vector3(3, 0, 0);
                break;
            case 3:
                m_InputCheckObject[0].transform.position = new Vector3(-6, 0, 0);
                m_InputCheckObject[1].transform.position = new Vector3(0, 0, 0);
                m_InputCheckObject[2].transform.position = new Vector3(6, 0, 0);
                break;
            case 4:
                m_InputCheckObject[0].transform.position = new Vector3(-3, 3, 0);
                m_InputCheckObject[1].transform.position = new Vector3(3, 3, 0);
                m_InputCheckObject[2].transform.position = new Vector3(-3, -3, 0);
                m_InputCheckObject[3].transform.position = new Vector3(3, -3, 0);
                break;
            default:
                Debug.LogError("想定外のプレイ人数です");
                break;
        }
    }

    /// <summary>
    /// チェックしたオブジェクトの変化
    /// </summary>
    /// <param name="_ChangeObject">変化するオブジェクト</param>
    void CheckChangeObject(GameObject _ChangeObject)
    {
        _ChangeObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    /// <summary>
    /// アクティブのセット
    /// </summary>
    /// <param name="_IsActive">アクティブにするかどうか</param>
    /// /// <param name="_PlayerNumber">プレイ人数</param>
    void SetActiveNumberObject(bool _IsActive, int _PlayerNumber = 4)
    {
        for (int i = 0; i < _PlayerNumber; ++i)
        {
            m_InputCheckObject[i].GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            m_InputCheckObject[i].SetActive(_IsActive);
        }
        for (int i = _PlayerNumber; i < m_InputCheckObject.Length; ++i)
        {
            m_InputCheckObject[i].SetActive(false);
        }
    }
}
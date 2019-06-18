using UnityEngine;

/// <summary>
/// 接続の当たり判定の管理クラス
/// </summary>
public class ConnectColliderManager : MonoBehaviour
{
    [System.Serializable]
    public class ConnectPair
    {
        public GameObject m_Right = null, m_Left = null;
    }
    [SerializeField, Header("接続リスト")]
    ConnectPair[] m_ConnectPairs = new ConnectPair[PlayerNumberCheck.m_PlayerNumber];
    [SerializeField, Header("プレイヤー1")]
    GameObject m_Player1 = null;
    [SerializeField, Header("プレイヤー2")]
    GameObject m_Player2 = null;
    //円コライダの半径
    float m_ColliderRadius = 0.0f;

    //1Pを基準に接続されているか判定する
    bool m_IsRightConnect = false, m_IsLeftConnect = false;
    //両方の接続がそろったフラグ
    bool m_IsBothConnect = false;
    //Joint
    FixedJoint2D m_Player1Joint2D = null;

    /// <summary>
    /// 接続かどうかのプロパティ
    /// </summary>
    public bool IsConnect { get { return !m_IsBothConnect && (m_IsRightConnect || m_IsLeftConnect); } }
    public Vector2 ConnectPosition { get { return (m_Player1Joint2D) ? m_Player1Joint2D.anchor : Vector2.zero; } }

    void Start()
    {
        ErrorCheck();
        //当たり判定用の値
        m_ColliderRadius =
            Mathf.Pow(m_ConnectPairs[0].m_Right.GetComponent<CircleCollider2D>().radius *
            m_ConnectPairs[0].m_Right.transform.lossyScale.x * 2, 2);
    }

    void FixedUpdate()
    {
        //右の接続
        m_IsRightConnect =
            (m_ConnectPairs[0].m_Right.transform.position -
            m_ConnectPairs[1].m_Left.transform.position).sqrMagnitude < m_ColliderRadius;
        //左の接続
        m_IsLeftConnect =
            (m_ConnectPairs[0].m_Left.transform.position -
            m_ConnectPairs[1].m_Right.transform.position).sqrMagnitude < m_ColliderRadius;

        //両方接続したフラグ
        if (m_IsBothConnect)
        {
            //どちらも離れたらfalse
            if (!m_IsRightConnect && !m_IsLeftConnect) m_IsBothConnect = false;
        }
        //どちらも接続している
        else if (m_IsRightConnect && m_IsLeftConnect)
        {
            GeneratePolygonCollider();
            Destroy(m_Player1Joint2D);
            m_IsBothConnect = true;
        }
        //どちらか接続している
        else if (m_IsRightConnect || m_IsLeftConnect)
        {
            if (!m_Player1Joint2D)
            {
                m_Player1Joint2D = m_Player1.AddComponent<FixedJoint2D>();
                m_Player1Joint2D.connectedBody = m_Player2.GetComponent<Rigidbody2D>();
            }
            //anchorの設定
            if (m_IsRightConnect)
            {
                m_Player1Joint2D.anchor = (m_ConnectPairs[0].m_Right.transform.position +
                                                m_ConnectPairs[1].m_Left.transform.position) / 2;
            }
            else if (m_IsLeftConnect)
            {
                m_Player1Joint2D.anchor = (m_ConnectPairs[0].m_Left.transform.position +
                                                m_ConnectPairs[1].m_Right.transform.position) / 2;
            }
        }
        else
        {
            Destroy(m_Player1Joint2D);
        }
    }

    /// <summary>
    /// 当たり判定の生成
    /// </summary>
    void GeneratePolygonCollider()
    {
        var obj = new GameObject("PolygonCollider");
        //当たり判定を生成
        var polygonCollider = obj.AddComponent<PolygonCollider2D>();
        //頂点情報
        Vector2[] points = new Vector2[4];
        points[0] = (m_ConnectPairs[0].m_Right.transform.position + m_ConnectPairs[1].m_Left.transform.position) / 2;
        points[1] = m_Player1.transform.position;
        points[2] = (m_ConnectPairs[0].m_Left.transform.position + m_ConnectPairs[1].m_Right.transform.position) / 2;
        points[3] = m_Player2.transform.position;
        //頂点をセット
        polygonCollider.points = points;
        //istrigger
        polygonCollider.isTrigger = true;
        //0.5秒後に破棄
        Destroy(obj, 0.5f);
    }

    /// <summary>
    /// エラーチェック
    /// </summary>
    void ErrorCheck()
    {
        foreach (var connectPair in m_ConnectPairs)
        {
            if (!connectPair.m_Right || !connectPair.m_Left)
            {
                Debug.LogError("接続が入っていません");
            }
        }
        if (!m_Player1)
        {
            Debug.LogError("プレイヤー1が入っていません");
        }
        if (!m_Player2)
        {
            Debug.LogError("プレイヤー2が入っていません");
        }
    }
}
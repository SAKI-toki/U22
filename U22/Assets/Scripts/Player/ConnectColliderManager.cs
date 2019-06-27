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
        public SpriteRenderer m_RightSpriteRenderer = null, m_LeftSpriteRenderer = null;
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
    bool m_IsCurrentRightConnect = false, m_IsCurrentLeftConnect = false,
    m_IsPrevRightConnect = false, m_IsPrevLeftConnect = false;
    //両方の接続がそろったフラグ
    bool m_IsBothConnect = false;
    Vector3 m_ConnectPosition = new Vector3();
    /// <summary>
    /// 接続かどうかのプロパティ
    /// </summary>
    public bool IsConnect { get { return !m_IsBothConnect && (m_IsCurrentRightConnect || m_IsCurrentLeftConnect); } }
    public Vector3 ConnectPosition { get { return m_ConnectPosition; } }

    void Start()
    {
        ErrorCheck();
        //当たり判定用の値
        m_ColliderRadius =
            Mathf.Pow(m_ConnectPairs[0].m_Right.GetComponent<CircleCollider2D>().radius *
            m_ConnectPairs[0].m_Right.transform.lossyScale.x * 2, 2);
    }

    void Update()
    {
        m_IsPrevRightConnect = m_IsCurrentRightConnect;
        m_IsPrevLeftConnect = m_IsCurrentLeftConnect;
        //右の接続
        m_IsCurrentRightConnect =
            (m_ConnectPairs[0].m_Right.transform.position -
            m_ConnectPairs[1].m_Left.transform.position).sqrMagnitude < m_ColliderRadius;
        //左の接続
        m_IsCurrentLeftConnect =
            (m_ConnectPairs[0].m_Left.transform.position -
            m_ConnectPairs[1].m_Right.transform.position).sqrMagnitude < m_ColliderRadius;

        m_ConnectPairs[0].m_RightSpriteRenderer.color = (m_IsCurrentRightConnect) ? Color.red : Color.white;
        m_ConnectPairs[1].m_RightSpriteRenderer.color = (m_IsCurrentLeftConnect) ? Color.red : Color.white;
        m_ConnectPairs[0].m_LeftSpriteRenderer.color = (m_IsCurrentLeftConnect) ? Color.red : Color.white;
        m_ConnectPairs[1].m_LeftSpriteRenderer.color = (m_IsCurrentRightConnect) ? Color.red : Color.white;

        //両方接続したフラグ
        if (m_IsBothConnect)
        {
            //どちらも離れたらfalse
            if (!m_IsCurrentRightConnect && !m_IsCurrentLeftConnect) m_IsBothConnect = false;
        }
        //どちらも接続している
        else if (m_IsCurrentRightConnect && m_IsCurrentLeftConnect)
        {
            GeneratePolygonCollider();
            m_IsBothConnect = true;
        }
        //どちらか接続している
        else if (m_IsCurrentRightConnect || m_IsCurrentLeftConnect)
        {
            //接続点の設定
            if (!m_IsPrevRightConnect && m_IsCurrentRightConnect)
            {
                m_ConnectPosition = (m_ConnectPairs[0].m_Right.transform.position +
            m_ConnectPairs[1].m_Left.transform.position) / 2;
            }
            else if (!m_IsPrevLeftConnect && m_IsCurrentLeftConnect)
            {
                m_ConnectPosition = (m_ConnectPairs[0].m_Left.transform.position +
            m_ConnectPairs[1].m_Right.transform.position) / 2;
            }
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

    public Vector3 GetConnectPosition(bool _IsPlayer1)
    {
        if (m_IsCurrentRightConnect)
        {
            return (_IsPlayer1) ? m_ConnectPairs[0].m_Right.transform.position : m_ConnectPairs[1].m_Left.transform.position;
        }
        if (m_IsCurrentLeftConnect)
        {
            return (_IsPlayer1) ? m_ConnectPairs[0].m_Left.transform.position : m_ConnectPairs[1].m_Right.transform.position;
        }
        return Vector3.zero;
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
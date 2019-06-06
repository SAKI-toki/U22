using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy : MonoBehaviour
{

    GameObject m_Target;

    //画面範囲の上限と下限
    Vector2 m_DisplayMin;
    Vector2 m_DisplayMax;
    Vector2 m_VecDifference;
    Vector2 m_RandomMoveVec;

    [SerializeField, Range(0, 360), Header("最大視野角")]
    int m_MaxSight;

    float m_RandomMoveInterval = 0;
    float m_Angle;
    const float m_Speed = 1;

    bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        m_DisplayMax = Camera.main.ViewportToWorldPoint(Vector2.one);
        m_DisplayMin = Camera.main.ViewportToWorldPoint(Vector2.zero);

        RandomMove();
    }

    // Update is called once per frame
    void Update()
    {
        NearPlayerSearch();
        //追跡するターゲットがNull以外だったら
        if (m_Target != null)
        {
            //ターゲットへの方向ベクトルを求める
            m_VecDifference = (m_Target.transform.position - transform.position).normalized;

            //敵の向いている方向からターゲットへの角度を求める
            m_Angle = Vector2.Angle(transform.up, m_VecDifference);

            //ターゲットへの角度が視野角以内だったら追尾を開始する
            if (m_Angle < m_MaxSight / 2)
            {
                transform.up = Vector2.Lerp(transform.up, m_VecDifference, Time.deltaTime * 10);
            }
        }
        else
        {
            m_RandomMoveInterval += Time.deltaTime;
            if (m_RandomMoveInterval > 2f)
            {
                if (!isMoving)
                {
                    RandomMove();
                    isMoving = !isMoving;
                }
                else
                {
                    isMoving = !isMoving;
                }
                m_RandomMoveInterval = 0;
            }
            transform.up = Vector2.Lerp(transform.up, m_RandomMoveVec, Time.deltaTime * 5);
        }
        if (!isMoving)
        {
            transform.position += transform.up.normalized * Time.deltaTime * m_Speed;
        }

    }

    //敵と一番距離が近いPlayerを探す
    void NearPlayerSearch()
    {
        float nearDistance = 0;

        GameObject targetPlayer = null;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);

            if ((nearDistance == 0 || nearDistance > distance) && distance < 3)
            {
                nearDistance = distance;
                targetPlayer = player;
                isMoving = false;
            }
        }
        m_Target = targetPlayer;
    }

    //敵のランダム移動
    void RandomMove()
    {
        //ランダム方向のベクトルを格納
        Vector2 randomMoveVec = Vector2.zero;
        //画面内のランダムな位置を所得
        randomMoveVec = new Vector2(Random.Range(m_DisplayMax.x, m_DisplayMin.x), Random.Range(m_DisplayMax.y, m_DisplayMin.y));

        m_RandomMoveVec = randomMoveVec;

    }
}

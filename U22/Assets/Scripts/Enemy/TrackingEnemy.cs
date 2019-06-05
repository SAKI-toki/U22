using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy : MonoBehaviour
{

    GameObject m_Target;

    //画面範囲の上限と下限
    Vector2 m_DisplayMin;
    Vector2 m_DisplayMax;
    Vector2 vecDifference;

    [SerializeField, Range(0, 360), Header("最大視野角")]
    int m_MaxSight;

    float m_Angle;
    const float m_Speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        m_DisplayMax = Camera.main.ViewportToWorldPoint(Vector2.one);
        m_DisplayMin = Camera.main.ViewportToWorldPoint(Vector2.zero);

        InvokeRepeating("NearPlayerSearch", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {

        if (m_Target != null)
        {
            //ターゲットへの方向ベクトルを求める
            vecDifference = (m_Target.transform.position - transform.position).normalized;

            //敵の向いている方向からターゲットへの角度を求める
            m_Angle = Vector2.Angle(transform.up, vecDifference);

            //ターゲットへの角度が視野角以内,だったら追尾を開始する
            if (m_Angle < m_MaxSight / 2 && Vector2.Distance(transform.position,m_Target.transform.position) < 3)
            {
                transform.up = Vector2.Lerp(transform.up, vecDifference, Time.deltaTime * 10);
            }
        }

        transform.position += transform.up * Time.deltaTime * m_Speed;
    }

    //このオブジェクトと一番距離が近いPlayerを探し、返す。
    void NearPlayerSearch()
    {
        float nearDistance = 0;

        GameObject targetPlayer = null;

        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            float distance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);

            if (nearDistance == 0 || nearDistance > distance)
            {
                nearDistance = distance;
                targetPlayer = player;
            }
        }
        m_Target = targetPlayer;
    }
}

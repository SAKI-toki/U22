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

    float m_MaxAngle;
    float m_MinAngle;
    float m_Angle;

    const float m_Speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        m_DisplayMax = Camera.main.ViewportToWorldPoint(Vector2.one);
        m_DisplayMin = Camera.main.ViewportToWorldPoint(Vector2.zero);

        m_MaxAngle = transform.eulerAngles.z + m_MaxSight / 2;
        m_MinAngle = transform.eulerAngles.z - m_MaxSight / 2;
        Debug.Log(m_MaxAngle);
        Debug.Log(m_MinAngle);

        InvokeRepeating("NearPlayerSearch", 0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target != null)
        {
            vecDifference = (m_Target.transform.position - this.gameObject.transform.position).normalized;
            m_Angle = Mathf.Atan2(vecDifference.y, vecDifference.x) * Mathf.Rad2Deg;
            Debug.Log(m_Angle);
            if ((m_Angle < m_MaxAngle && m_Angle > m_MinAngle) && Vector2.Distance(gameObject.transform.position, m_Target.transform.position) < 1)
            {
                transform.up = Vector2.Lerp(transform.position, m_Target.transform.position, Time.deltaTime * m_Speed);
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

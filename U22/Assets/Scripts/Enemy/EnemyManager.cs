using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //Vector2 m_DisplayMin;
    //Vector2 m_DisplayMax;

    const float Speed = 5;  //Enemyの移動速度

    // Start is called before the first frame update
    void Start()
    {
        //m_DisplayMax = Camera.main.ViewportToWorldPoint(Vector2.one);
        //m_DisplayMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        //Enemyを直進させる
        transform.position += transform.up * Time.deltaTime * Speed;
        ////画面の上下を超えた場合反射させる
        //if(transform.position.y >= m_DisplayMax.y || transform.position.y <= m_DisplayMin.y)
        //{
        //    transform.up = new Vector2(transform.up.x, -(transform.up.y));
        //}
        ////画面の左右を超えた場合反射させる
        //if(transform.position.x >= m_DisplayMax.x|| transform.position.x <= m_DisplayMin.x)
        //{
        //    transform.up = new Vector2(-(transform.up.x), transform.up.y);
        //}
    }

    private void OnCollisionEnter2D(Collision2D _Colliion)
    {
        //触れたオブジェクトの法線を基準としてこのオブジェクトのベクトルの反射したベクトルをもとめる
        Vector2 RefrectVec = Vector2.Reflect(transform.up, _Colliion.contacts[0].normal);

        //オブジェクトの向きを求めたベクトルの向きに変える
        transform.up = RefrectVec;
    }
}

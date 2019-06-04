using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField, Header("プレイヤーオブジェクト")]
    GameObject m_PlayerObject = null;

    PlayerController[] m_PlayerControllers = null;


    void Start()
    {
        m_PlayerControllers = new PlayerController[PlayerNumberCheckManager.m_PlayerNumber];
    }

    void Update()
    {

    }
}

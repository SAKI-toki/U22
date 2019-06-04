using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField, Header("プレイヤーの生成位置")]
    Vector3[] m_PlayerGeneratePosition = null;
    //プレイヤーリスト
    PlayerController[] m_PlayerControllers = null;

    void Start()
    {
        if (PlayerNumberCheckManager.m_PlayerNumber < 2 || PlayerNumberCheckManager.m_PlayerNumber > 4)
        {
            Debug.LogError("プレイヤーの人数が2未満、または4より多いです");
            return;
        }
        m_PlayerControllers = new PlayerController[PlayerNumberCheckManager.m_PlayerNumber];
        GeneratePlayers(PlayerNumberCheckManager.m_PlayerNumber);
    }

    void Update()
    {

    }

    /// <summary>
    /// プレイヤーを生成する
    /// </summary>
    /// <param name="_PlayerNum">プレイヤーの数</param>
    void GeneratePlayers(int _PlayerNum)
    {
        GameObject playerParentObject = new GameObject("playerList");

        //プレイヤーを生成
        for (int i = 0; i < _PlayerNum; ++i)
        {
            m_PlayerControllers[i] =
                Instantiate(Config.GetInstance().PlayerObject, m_PlayerGeneratePosition[i], Quaternion.identity).
                GetComponent<PlayerController>();
            //番号をセット
            m_PlayerControllers[i].ThisPlayerNumber = i;
            //親オブジェクトにまとめる
            m_PlayerControllers[i].transform.parent = playerParentObject.transform;
        }
    }
}

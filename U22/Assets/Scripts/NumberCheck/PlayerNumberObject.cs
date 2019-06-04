using UnityEngine;

/// <summary>
/// プレイ人数を格納するクラス
/// </summary>
public class PlayerNumberObject : MonoBehaviour
{
    [SerializeField, Header("プレイ人数")]
    public int m_PlayerNumber = 0;
}
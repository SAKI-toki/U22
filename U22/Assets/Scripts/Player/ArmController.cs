using UnityEngine;

/// <summary>
/// 腕の制御クラス
/// </summary>
public class ArmController : MonoBehaviour
{
    bool m_IsConnect = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerArm")
        {

        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルの制御クラス
/// </summary>
public class TitleController : MonoBehaviour
{
    void Update()
    {
        if (SwitchInput.GetButtonDown(0, SwitchButton.Down))
            SceneManager.LoadScene("SelectScene");
    }
}

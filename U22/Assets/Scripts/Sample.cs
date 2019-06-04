using UnityEngine;

public class Sample : MonoBehaviour
{
    void Update()
    {
        for (int i = (int)KeyCode.Joystick1Button0; i < (int)KeyCode.Joystick2Button0; ++i)
        {
            if (Input.GetKeyDown((KeyCode)i))
                Debug.Log((KeyCode)i);
        }
    }
}
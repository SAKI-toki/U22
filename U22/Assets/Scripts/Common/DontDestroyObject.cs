using UnityEngine;

/// <summary>
/// シーンが破棄されても消えないオブジェクト
/// </summary>
public class DontDestroyObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// DontDestroyObjectを生成する
    /// </summary>
    /// <param name="_ObjectName">オブジェクトの名前</param>
    /// <returns>生成したDontDestroyObject</returns>
    static public GameObject Generate(string _ObjectName)
    {
        var obj = new GameObject(_ObjectName);
        obj.AddComponent<DontDestroyObject>();
        return obj;
    }
}

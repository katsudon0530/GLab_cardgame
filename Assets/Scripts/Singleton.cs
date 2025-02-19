using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    
    public static T Instance
    {
        get
        {
            lock(_lock) //同時アクセスを防ぐ
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name);
                        _instance = obj.AddComponent<T>();
                        DontDestroyOnLoad(obj);
                    }
                }
                return _instance;
            }
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnApplicationQuit()
    {
        _instance = null;
    }
}

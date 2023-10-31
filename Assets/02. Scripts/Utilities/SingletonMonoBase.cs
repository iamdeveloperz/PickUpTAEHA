
using UnityEngine;

public class SingletonMonoBase<Type> : MonoBehaviour where Type : MonoBehaviour
{
    private static bool isShuttingDown = false;
    private static object locked = new object();        // 쓰레드 안전 보장을 위한 lock 객체
    private static Type instance;

    public static Type Instance
    {
        get
        {
            if (isShuttingDown)
                return null;
            lock (locked)            // 여러 쓰레드에서 동시 접근할 때 다중 생성되지 않기 위함
            {
                if (instance == null)
                {
                    instance = (Type)FindObjectOfType(typeof(Type));

                    GameObject singletonObject = null;
                    if (instance == null)
                    {
                        singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<Type>();
                    }
                    else
                        singletonObject = instance.gameObject;

                    DontDestroyOnLoad(singletonObject);
                    singletonObject.name = typeof(Type).ToString() + " (Singleton)";
                }
            }

            return instance;
        }
    }

    public void OnApplicationQuit()
    {
        isShuttingDown = true;
    }

    private void OnDestroy()
    {
        isShuttingDown = true;
    }
}
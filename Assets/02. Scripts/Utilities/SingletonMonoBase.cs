
using UnityEngine;

public class SingletonMonoBase<Type> : MonoBehaviour where Type : MonoBehaviour
{
    private static Type instance;

    public static Type Instance
    {
        get
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
            
            return instance;
        }
    }
}
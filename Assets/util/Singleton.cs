// Not in namespace because should be global

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
    public static T Instance
    {
        get{
            return Object.FindFirstObjectByType<T>();
        }
        protected set{
            Instance=value;
        }
    }
}
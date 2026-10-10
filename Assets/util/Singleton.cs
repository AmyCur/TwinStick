// Not in namespace because should be global

using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{
    public static T instance
    {
        get{
            return Object.FindFirstObjectByType<T>();
        }
        protected set{
            instance=value;
        }
    }
}
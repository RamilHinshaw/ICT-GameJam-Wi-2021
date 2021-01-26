using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    //private static bool m_ShuttingDown = false;
    //private static object m_Lock = new object();
    private static T instance;


    public void Awake()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
        }
        else
            Destroy(this.gameObject);
    }

    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance
    {
        get
        {
            //if (m_ShuttingDown)
            //{
            //    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
            //        "' already destroyed. Returning null.");
            //    return null;
            //}

            if (instance == null)
                print("Instance of GameObject does not exist!");

            return instance;
        }
    }


    //private void OnApplicationQuit()
    //{
    //    m_ShuttingDown = true;
    //}


    //private void OnDestroy()
    //{
    //    m_ShuttingDown = true;
    //}
}
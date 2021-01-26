using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    #region Singleton
    private static DontDestroy instance;
    public static DontDestroy Instance
    {
        get
        {
            //if (instance == null)
                //Debug.LogError("DOESN'T EXIST Input THE SCENE!");

            return instance;
        }

        set
        {
            instance = value;
        }

    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        else
            Destroy(this.gameObject);

    }

    private DontDestroy() { }
    #endregion

}

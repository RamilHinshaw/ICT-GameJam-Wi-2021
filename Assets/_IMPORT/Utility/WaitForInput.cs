using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitForInput : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent OnKeyPress;

    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(key))
            OnKeyPress.Invoke();
    }

}

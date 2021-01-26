using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleButton : MonoBehaviour {

    public UnityEvent OnToggleOn;
    public UnityEvent OnToggleOff;

    private bool toggleType = false;

    public void Toggle()
    {
        if (toggleType == true)
        {
            if (OnToggleOff != null)
                OnToggleOff.Invoke();

            toggleType = false;
        }

        else
        {
            if (OnToggleOn != null)
                OnToggleOn.Invoke();

            toggleType = true;
        }
    }
}

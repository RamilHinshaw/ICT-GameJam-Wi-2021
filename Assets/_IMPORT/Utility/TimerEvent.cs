using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class TimerEvent : MonoBehaviour {

    private bool timerOn = false;
    private float timeLeft = 0f;
    public UnityEvent OnTimerEnd;
    public Text text;

	// Update is called once per frame
	void Update ()
    {
        if (!timerOn) return;

        timeLeft -= Time.deltaTime;
        if (text != null) text.text = ((int) (timeLeft + .5f)).ToString();

        if (timeLeft <= 0f && OnTimerEnd != null)
        {
            timerOn = false;
            OnTimerEnd.Invoke();            
        }
	}

    public void StartTimer(float timer)
    {
        timeLeft = timer;
        timerOn = true;
    }
}

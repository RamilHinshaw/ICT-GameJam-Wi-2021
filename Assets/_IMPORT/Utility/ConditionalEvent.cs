using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

[Serializable]
public class ConditionalEvent : MonoBehaviour {

    [SerializeField] public Condition[] conditions;
    public UnityEvent OnConditionMet;
    private bool conditionsMet = false;

    private void ConditionsMetChecker()
    {
        if (conditionsMet) return;

        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].condition)
                return;

            if (i >= conditions.Length - 1)
                Invoke();
        }
    }

    public void Invoke()
    {
        if (OnConditionMet != null)
            OnConditionMet.Invoke();
        conditionsMet = true;
    }

    public void ToggleOn(int index) { conditions[index].condition = true; ConditionsMetChecker(); }
    public void ToggleOff(int index) { conditions[index].condition = false; }
    public void Toggle(int index) { conditions[index].condition = !conditions[index].condition; ConditionsMetChecker(); }

    public void Reset()
    {
        foreach (var condition in conditions)
            condition.condition = false;
        conditionsMet = false;
    }
}

[Serializable]
public class Condition
{
    [SerializeField] public string discription;
    [SerializeField] public bool condition;
}

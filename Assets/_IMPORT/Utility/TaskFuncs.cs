using System;

public class TaskFuncs
{
    public Action OnStart, MainFunc, OnEnd;
    public float timeLength;

    public TaskFuncs(Action OnStart, Action MainFunc, float timeLength, Action OnEnd)
    {
        OnStart.Invoke();
        //MainFunc.Invoke();
        this.MainFunc = MainFunc;
        this.OnEnd = OnEnd;
        this.timeLength = timeLength;
    }
}

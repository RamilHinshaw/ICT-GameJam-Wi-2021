using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskManager : MonoBehaviour, IManager
{
    //Parameter ( OnStart, Main Function, How long it goes, OnEnd)

    public List<TaskFuncs> tasks = new List<TaskFuncs>();

    public void Start()
    {
        
    }

    public void Update()
    {
        for (int i = 0; i < tasks.Count; i++)
        {
            tasks[i].MainFunc.Invoke();
            //EITHER USE MULTI THREADING OR COMPARE WITH GET TICKS
            tasks[i].timeLength -= Time.deltaTime;

            if (tasks[i].timeLength <= 0)
            {
                tasks[i].OnEnd();
                tasks.RemoveAt(i);
                i--;
            }
        }   
    }
}

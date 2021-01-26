using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public List<GameObject> gameObjects;
    public bool autoIgnore;

    private Collider collider;
    private bool initFinished;
    private float initWait = 0.25f;

    private void Awake()
    {
        collider = GetComponent<Collider>();

        foreach (var go in gameObjects)
            Physics.IgnoreCollision(go.GetComponent<Collider>(), collider);
    }

    private void Update()
    {
        if (autoIgnore && !initFinished)
        {
            initWait -= Time.deltaTime;

            if (initWait <= 0)
                initFinished = false;
        }
    }

    private void LateUpdate()
    {
        if (autoIgnore && !initFinished)
            initFinished = true;
    }


    public void OnCollisionEnter(Collision other)
    {
        if (autoIgnore && !initFinished && gameObjects.Contains(other.gameObject) == false)
        {
            gameObjects.Add(other.gameObject);
            Physics.IgnoreCollision(other.transform.GetComponent<Collider>(), collider);
            //Debug.Log("IGNORE " + other.transform.name);
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class MovePlatform : MonoBehaviour
{
    public bool autostart = true, loopable = true;
    public List<Vector3> path;
    public float speed = 1f;
    [Header("If you don't want to stick to it, good for pistons")]
    public bool notAPlatform = false;

    private bool forceSmoothCamera = false;
    private Rigidbody rb;
    //private CinemachineBrain cinema;
    private bool running = true, prepareLoopStop;

    private int pathCounter = 0;


    void Start()
    {
        running = autostart;

        if (path.Count != 0)
            this.transform.position = path[0];

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        //if (forceSmoothCamera)
            //cinema = Camera.main.GetComponent<CinemachineBrain>();
    }

    void FixedUpdate()
    {
        if (!running) return;
        if (path == null) return;

        PlatformMovement();
    }

    public void StartMovement()
    {
        running = true;
    }


    void OnDrawGizmosSelected()
    {
        if (path == null) return;

        for (int i = 0; i < path.Count; i++)
        {
            //DRAW CUBES
            Gizmos.color = new Color(1, 0, 0, 0.5f);

            if (i == pathCounter)
                Gizmos.color = Color.green;

            Gizmos.DrawCube(path[i], new Vector3(0.75f, 0.75f, 0.75f));

            //DRAW LINES
            if (i + 1 != path.Count)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
            if (i == 0)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(path[0], path[path.Count - 1]);
            }
        }
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (notAPlatform) return;

    //    if (col.transform.tag == "Player")
    //    {
    //        col.transform.parent = transform;
    //    }

    //}

    //void OnCollisionExit(Collision col)
    //{
    //    if (col.transform.tag == "Player")
    //    {
    //        col.transform.parent = null;
    //    }
    //}


    //PRIVATE MEMBERS
    void PlatformMovement()
    {
        if (path.Count == 0) return;

        //Transformation Formula
        transform.position = Vector3.MoveTowards(transform.position, path[pathCounter], Time.deltaTime * speed);


        if (transform.position == path[pathCounter])
        {
            pathCounter++;
        }

        if (pathCounter >= path.Count)
        {
            pathCounter = 0;

            //prepareLoopStop = !loopable;
            if (!loopable)
                running = false;
        }
    }
}
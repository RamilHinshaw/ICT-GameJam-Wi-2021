using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform emit;
    public LineRenderer lineR;
    public bool reflect;

    private Vector3[] points;

    private Ray ray;
    private RaycastHit hit;

    public ParticleSystem lazerHitFX;


    private void Start()
    {
        if (emit == null)
            emit = transform;

        if (lineR == null)
            lineR = GetComponent<LineRenderer>();
    }

    void Update()
    {
        DrawLaser();
    }

    void DrawLaser()
    {
        RaycastHit hit;
        lineR.positionCount = 2;

        if (Physics.Raycast(emit.position, emit.forward, out hit, Mathf.Infinity))
        {

            lineR.enabled = true;
            //Debug.DrawRay(transform.position, transform.forward * Mathf.Infinity);
            lineR.SetPosition(0, transform.position);
            lineR.SetPosition(1, hit.point);

            if (hit.collider.tag == "Player")
            {
                //Do Something!
            }

            //update the lazer pos to the point
            lazerHitFX.transform.position = hit.point;

            //lazer hit fx
            if(hit.collider != null && lazerHitFX.isStopped)
            {
                lazerHitFX.Play();
            }

            if (reflect && hit.collider.tag == "Mirror")
            {
                lineR.positionCount = 3;
                Vector3 pos = Vector3.Reflect(hit.point - this.transform.position, hit.normal);
                lineR.SetPosition(2, pos);
                //lineRenderer.SetPosition(3, pos);
            }
        }
        else
        {
            lineR.SetPosition(0, transform.position);
            lineR.SetPosition(1, emit.forward * 90000f);
        }
    }

    private void DrawLineRendererPoint()
    {
        lineR.positionCount = points.Length;

        for (int i = 0; i < points.Length; i++)
            lineR.SetPosition(i, points[i]);
    }
}

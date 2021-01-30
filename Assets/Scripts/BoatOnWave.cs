using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatOnWave : MonoBehaviour
{
    public float yOffset;

    void FixedUpdate()
    {
        //Ray point down
        //Ray hit normal transform.position.y follows normal pos
        //Rotation based on the normal?

        RaycastHit hit;
        Vector3 rayOrigin = new Vector3(0, 100f, 0);
        Debug.DrawRay(rayOrigin, transform.TransformDirection(Vector3.down) * 1000, Color.red);

        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y + yOffset, transform.position.z);
            print(hit.point.y);
        }



        //transform.Rotate(0, transform.eulerAngles.y, 0);

    }
}

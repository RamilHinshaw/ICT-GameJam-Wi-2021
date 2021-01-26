using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAxis : MonoBehaviour {

    public Vector3 speed = Vector3.one;
    public bool relativeRotation = false;


	
	// Update is called once per frame
	void Update () {
        //transform.RotateAround(transform.position, transform.forward, Time.deltaTime * speed);

        if (relativeRotation)
        {
            transform.Rotate(transform.forward * Time.deltaTime * speed.z);
            transform.Rotate(transform.up * Time.deltaTime * speed.y);
            transform.Rotate(transform.right * Time.deltaTime * speed.x);
        }

        else
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed.z);
            transform.Rotate(Vector3.up * Time.deltaTime * speed.y);
            transform.Rotate(Vector3.right * Time.deltaTime * speed.x);
        }
    }
}

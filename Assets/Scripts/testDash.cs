using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDash : MonoBehaviour
{
    public float power;
    public ForceMode forceMode;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rb.AddForce(Camera.main.transform.forward * power, forceMode);
        }
    }
}

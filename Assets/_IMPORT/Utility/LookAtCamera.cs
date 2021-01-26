﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    private GameObject target;

    // Use this for initialization
    void Start()
    {
        target = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.gameObject;
        transform.LookAt(2 * transform.position - target.transform.position);
    }
}

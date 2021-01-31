using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CamModes
    {
        Ship,
        Attack,
        Hit
    }

    public CamModes mode;

    public GameObject shipCam;
    public GameObject attackCam;
    public GameObject hitCam;


    public Vector3 offset;
    private float rotSpeed = 2.5f;
    private float lerpSpeed = 2.5f;

    // Update is called once per frame
    void LateUpdate()
    {

        switch (mode)
        {
            case CamModes.Ship:
                transform.position = new Vector3(transform.position.x, 31.53f, transform.position.z);
                transform.LookAt(shipCam.transform.position + offset);
                transform.Translate(Vector3.right * rotSpeed * Time.deltaTime);
                break;
            case CamModes.Attack:
                transform.position = Vector3.Lerp(transform.position, attackCam.transform.position, lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, attackCam.transform.rotation, lerpSpeed * Time.deltaTime);
                break;
            case CamModes.Hit:
                transform.position = Vector3.Lerp(transform.position, hitCam.transform.position, lerpSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Lerp(transform.rotation, hitCam.transform.rotation, lerpSpeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    public void SwitchModes(CamModes mode)
    {
        rotSpeed = 2.5f;
        lerpSpeed = 2.5f;
        this.mode = mode;
    }

    public void SwitchModesFast(CamModes mode)
    {
        rotSpeed = 999f;
        lerpSpeed = 999f;
        this.mode = mode;
    }
}


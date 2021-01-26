using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour {

    //public bool alignRotationOnGrab;
    public RotateType initialRotation = RotateType.normal;
    public enum RotateType { normal, zero, custom}
    [Header("Work if set to custom")]
    public Vector3 customRotation;


}

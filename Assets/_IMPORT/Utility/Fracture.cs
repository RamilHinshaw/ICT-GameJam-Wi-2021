using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fracture : MonoBehaviour
{
    [Tooltip("Speed needed to break object")]
    public float fractureVelocity = 1.5f;
    public float explosionForce = 115f;
    public float explosionRadius = 15f;
    public string[] breakFromThese = new string[] { "Interactable" };
    public AudioClip soundEffect;

    [Header("Additional Settings")]
    public bool setupFracture;
    public GameObject swapModel;

    [Range(0,2f)]
    public float appliedForceStrength = 0f;

    public UnityEvent OnFracture;

    private List<Rigidbody> fractureRigidBodies = new List<Rigidbody>();
    private List<Collider> fractureColliders = new List<Collider>();

    private Vector3 appliedForce;

    private Rigidbody rb;
    private Collider col;

    private float velocity;

    private void Start()
    {
        ////tag = "Interactable";

        if (setupFracture)
            SetupFracture();

        var rbs = GetComponentsInChildren<Rigidbody>();
        var cols = GetComponentsInChildren<Collider>();

        //Make sure parent is not in list for col and rb
        for (int i = 0; i < rbs.Length; i++)     
            if (rbs[i].transform.gameObject != transform.gameObject)
                //if (swapModel != null || (swapModel.transform != rbs[i].transform))
                    fractureRigidBodies.Add(rbs[i]);

        for (int i = 0; i < cols.Length; i++)
            if (cols[i].transform.gameObject != transform.gameObject)
               // if (swapModel != null || (swapModel.transform != cols[i].transform))
                    fractureColliders.Add(cols[i]);

        if (swapModel != null)
            for (int i = 0; i < cols.Length; i++)
                if (cols[i].gameObject != swapModel || cols[i].gameObject != gameObject)
                    cols[i].gameObject.SetActive(false);

        //Brute Forced!
        gameObject.SetActive(true);

        if (swapModel != null)
        {
            swapModel.SetActive(true);
            swapModel.GetComponent<Rigidbody>().isKinematic = true;
            swapModel.tag = "Untagged";
        }

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (rb != null)
            velocity = rb.velocity.magnitude;
    }

    private void SetupFracture()
    {
        Transform[] children = gameObject.transform.GetComponentsInChildren<Transform>();

        for (int i = 0; i < children.Length; i++)
            if (children[i].transform.GetInstanceID() != transform.GetInstanceID())
            {
                //Debug.Log(children[i].name);
                //Add rigidbody, colliders, & grabbable
                children[i].tag = "Interactable";        

                var col = children[i].gameObject.AddComponent<MeshCollider>();

                if (col.gameObject != gameObject)
                    col.enabled = false;

                col.convex = true;

                var rb = children[i].gameObject.AddComponent<Rigidbody>();

                if (rb != null)
                    rb.isKinematic = true;

                var grab = children[i].gameObject.AddComponent<Grabbable>();             

            }

    }

    public void Explosion()
    {
        BeginFracture();
    }

    private void BeginFracture()
    {
        //if (soundEffect != null)
        //    GameManager.Instance.PlaySound(soundEffect);

        col.enabled = false;
        tag = "Untagged";
        if (rb != null)
            rb.isKinematic = true;

        for (int i = 0; i < fractureRigidBodies.Count; i++)
        {
            fractureColliders[i].gameObject.SetActive(true);


            if (swapModel != null)
                swapModel.SetActive(false);

            fractureColliders[i].enabled = true;
            fractureRigidBodies[i].tag = "Interactable";
            fractureRigidBodies[i].isKinematic = false;

            fractureRigidBodies[i].velocity = appliedForce * appliedForceStrength;

            fractureRigidBodies[i].AddExplosionForce(explosionForce, transform.position, explosionRadius);

            fractureRigidBodies[i].gameObject.SetActive(true);
        }

        enabled = false;
        OnFracture.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!enabled || collision.transform.tag == "Player") return;

        //if (rb != null)
            //Debug.Log(velocity);

        //This being thrown to fracture
        if (rb != null && (velocity >= fractureVelocity))
        {
            appliedForce = rb.velocity;
            BeginFracture();
        }



       // else if (collision.transform.tag == "Interactable")
               //Waiting to be fractured
        foreach (string tag in breakFromThese)
            if (collision.gameObject.tag == tag)
            {
                var rigidBody = collision.transform.GetComponent<Rigidbody>();

                if (rigidBody.velocity.magnitude >= fractureVelocity)
                {
                    appliedForce = rigidBody.velocity;
                    BeginFracture();
                }
            }


    }
}

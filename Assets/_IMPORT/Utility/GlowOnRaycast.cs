//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GlowOnRaycast : MonoBehaviour
//{

//    private Ray ray;
//    private RaycastHit hit;

//    public bool glowObjects;
//    public GameObject glowGO;
//    public Renderer glowRend;
//    public Material matOriginal;

//    // Update is called once per frame
//    void Update()
//    {
//        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

//        Debug.DrawRay(ray.origin, ray.direction * grabLength, Color.red);
//    }


//    private void Glow(GameObject go)
//    {
//        if (!glowObjects) return;

//        Debug.Log("GLOWING!");

//        if (go != glowGO)
//        {
//            if (glowRend != null)
//            {
//                glowRend.material = matOriginal;
//                glowRend.material.DisableKeyword("_EMISSION");
//                Debug.Log("CHANGED BACK MATERIAL!");
//            }

//            glowRend = go.GetComponent<Renderer>();

//            if (glowRend == null) throw new System.Exception("There is no renderer component!");

//            matOriginal = glowRend.material;
//            glowRend.material.EnableKeyword("_EMISSION");
//            glowRend.material.SetColor("_EmissionColor", Color.yellow * 5.3f);

//            glowGO = go;
//        }
//    }
//}

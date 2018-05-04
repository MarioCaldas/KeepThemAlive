using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHurted : MonoBehaviour {

    public GameObject MetaSpot;
    public Material Transparent;
    public Material GreenTransparent;
    public GameObject Canvas;
    
    public void GoMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = GreenTransparent;
    }

    public void LeaveMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = Transparent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "WeakSpot")
        {
            Debug.Log("Uma pessoa foi salva");
            CanvasScript.PessSalvas++;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "WeakSpot")
    //    {
    //        Debug.Log("Uma pessoa foi salva");
    //        CanvasScript.PessSalvas++;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHurted : MonoBehaviour {

    public GameObject MetaSpot;
    public Material Transparent;
    public Material GreenTransparent;
    public GameObject Canvas;

    void Start()
    {

    }


    void Update()
    {

    }

    public void GoMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = GreenTransparent;
    }

    public void LeaveMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = Transparent;
    }
}

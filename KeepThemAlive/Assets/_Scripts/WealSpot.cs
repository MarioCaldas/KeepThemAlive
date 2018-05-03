using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WealSpot : MonoBehaviour {


	void Start () {
		
	}
	

	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "NPC")
        {
            Debug.Log("Uma pessoa foi salva");
            CanvasScript.PessSalvas++;
        }
    }
}

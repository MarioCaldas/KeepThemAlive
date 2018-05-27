using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticlesColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("oi");
        Destroy(other.gameObject);

        //if(other.tag == "Fire")
        //{
        //    Debug.Log("oi");
        //}


    }
}

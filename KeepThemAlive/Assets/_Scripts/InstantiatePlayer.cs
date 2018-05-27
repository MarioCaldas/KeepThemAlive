using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {

    public GameObject Player;

    public static bool playerInstantied;

    public GameObject camera;

    CameraFollow camScript;

    GameObject ambulanceSpot;

	// Use this for initialization
	void Start () {

        camScript = camera.GetComponent<CameraFollow>();

        ambulanceSpot = transform.GetChild(4).gameObject;

        playerInstantied = false;
    }
	
	// Update is called once per frame
	void Update () {


	}

    void Instantiate()
    {
        if(Player == null)
        {

           Player = Instantiate(Resources.Load("FireMan") as GameObject, new Vector3(296, 0, 352), Quaternion.identity);

        }

        ambulanceSpot.GetComponent<MeshRenderer>().enabled = true;

        playerInstantied = true;


    }
}

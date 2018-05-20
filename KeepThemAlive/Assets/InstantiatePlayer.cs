using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {

    public GameObject Player;

    public static bool playerInstantied;

    public GameObject camera;

    CameraFollow camScript;

	// Use this for initialization
	void Start () {

        camScript = camera.GetComponent<CameraFollow>();

        Player = Resources.Load("FireMan") as GameObject;

        playerInstantied = false;
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(playerInstantied);
	}

    void Instantiate()
    {

        Instantiate(Player, new Vector3(296, 0, 352), Quaternion.identity);


        playerInstantied = true;
    }
}

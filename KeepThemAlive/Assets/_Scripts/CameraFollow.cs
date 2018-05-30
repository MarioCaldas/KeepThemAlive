using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    public Transform ambulance;

    private Transform cam;

    bool followPlayer;

    float camY;
    

	void Start ()
    {
        camY = 100;

        player = null;
        //player = Resources.Load("FireMan") as GameObject;
        cam = GetComponent<Transform>();
        cam.rotation = Quaternion.Euler(90, 0,180);
        //player = ambulance.GetComponent<InstantiatePlayer>().Player;
	}
	
	void Update ()
    {

        if(!InstantiatePlayer.playerInstantied)
        {
            cam.position = ambulance.position + new Vector3(0, 100, 0);
        }
        else
        {

            if (camY > 70)
            {
                camY -= 0.5f;

            }

            if (player != null)
            {
                cam.position = player.transform.position + new Vector3(0, camY, 0);

            }
            else
            {

                player = GameObject.FindGameObjectWithTag("Player");

            }


        }

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Transform cam;
    

	void Start ()
    {
        cam = GetComponent<Transform>();
        cam.rotation = Quaternion.Euler(90, 0,180);
	}
	
	void Update ()
    {
        cam.position = player.position + new Vector3(0, 60, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private static GameObject player;
    private GameObject door;

    bool collided = false;

	void Start ()
    {
        player = GameObject.Find("EthanPlayer");
        door = transform.GetChild(1).gameObject;
    }
	
	void Update ()
    {
        RotateDoor(collided);
	}

    void RotateDoor(bool collided)
    {
        if (collided)
        {
            door.transform.Rotate(new Vector3(0f, -90f, 0f));
        }

        else
        {
            Debug.Log("Is not close");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == player.name)
        {
            collided = true;
        }

        else
        {
            collided = false;
        }
    }
}

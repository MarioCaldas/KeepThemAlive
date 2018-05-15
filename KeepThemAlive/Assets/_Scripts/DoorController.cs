using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private static GameObject player;
    private GameObject door;

    public static bool collided = false;

    public static GameObject doorCol;

	void Start ()
    {
        player = GameObject.Find("FireMan");
        door = transform.GetChild(1).gameObject;
    }
	
	void Update ()
    {
        //RotateDoor(collided);
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

    private void OnTriggerEnter(Collider collision)
    {
        doorCol = collision.gameObject;

        if (collision.gameObject.name == player.name)
        {
            collided = true;
        }

        else
        {
            collided = false;
        }
    }
}

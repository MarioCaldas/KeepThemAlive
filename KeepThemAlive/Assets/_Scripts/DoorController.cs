using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private static GameObject player;
    private GameObject door;

    public static bool collided = false;

    public static GameObject doorCol;

    Rigidbody rb;

    float rotateDoorTime;

    void Start ()
    {
        rotateDoorTime = .5f;

        player = GameObject.Find("FireMan");
        door = transform.GetChild(1).gameObject;
    }
	
	void Update ()
    {
        //RotateDoor(collided);

        if (FiremanController.openDoor)
        {
            Open();

            rotateDoorTime -= Time.deltaTime;
        }



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

    void Open()
    {


        Debug.Log(transform.GetChild(0).gameObject.name);
        transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
        rb = transform.GetChild(0).gameObject.GetComponent<Rigidbody>();

        rb.useGravity = false;

        //rb.AddForce(-transform.forward * 100f);
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

        rb.AddForceAtPosition(player.transform.forward, transform.GetChild(0).transform.localPosition, ForceMode.Impulse);


        if (rotateDoorTime < 0.1f)
        {
            rb.isKinematic = true;

            Debug.Log("ouu");

            transform.GetComponent<BoxCollider>().isTrigger = true;

            rotateDoorTime = .5f;

            transform.GetComponent<BoxCollider>().isTrigger = true;

            Destroy(rb);

            player.GetComponent<Animator>().SetBool("Kick", false);

            FiremanController.freeLook = true;

            FiremanController.openDoor = false;

            Destroy(transform.GetComponent<DoorController>());


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

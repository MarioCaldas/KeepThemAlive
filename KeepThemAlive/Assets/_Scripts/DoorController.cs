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


    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        door = transform.GetChild(1).gameObject;
    }
	
	void Update ()
    {
        //RotateDoor(collided);

        if (FiremanController.openDoor)
        {
            Open();

        }



	}


    void Open()
    {
        Rigidbody rb;

        Debug.Log(transform.GetChild(0).gameObject.name);
        rb = transform.GetChild(0).gameObject.AddComponent<Rigidbody>();
        transform.gameObject.GetComponent<BoxCollider>().isTrigger = true;

        rb.mass = 20;

        rb.AddForce(player.gameObject.transform.forward * 200, ForceMode.Impulse);

        //rb.AddForce(Physics.gravity * rb.mass, ForceMode.Acceleration);

        transform.GetChild(0).gameObject.AddComponent<BoxCollider>();

        //Vector3 ola = Vector3.RotateTowards(transform.right, player.transform.forward, 2f);

        //transform.GetChild(0).Rotate(10, 0, 0);

        player.GetComponent<Animator>().SetBool("Kick", false);



        Physics.IgnoreCollision(transform.GetChild(0).gameObject.GetComponent<BoxCollider>(), player.transform.GetComponent<CapsuleCollider>());

        transform.GetChild(0).parent = null;

        Destroy(transform.GetComponent<DoorController>());

        FiremanController.openDoor = false;

        //rb = transform.GetChild(0).gameObject.GetComponent<Rigidbody>();

        //rb.useGravity = false;

        //rb.AddForce(player.transform.forward, ForceMode.Acceleration);

        //rb.constraints = RigidbodyConstraints.FreezePositionX;

        //rb.AddForceAtPosition(-player.transform.forward, transform.GetChild(0).transform.localPosition, ForceMode.Impulse);


        //if (rotateDoorTime < 0.1f)
        //{
        //    //rb.isKinematic = true;

        //    Debug.Log("ouu");

        //    transform.GetComponent<BoxCollider>().isTrigger = true;

        //    rotateDoorTime = .5f;

        //    transform.GetComponent<BoxCollider>().isTrigger = true;

        //    Destroy(rb);

        //    player.GetComponent<Animator>().SetBool("Kick", false);

        //    FiremanController.freeLook = true;

        //    FiremanController.openDoor = false;

        //    Destroy(transform.GetComponent<DoorController>());


        //}




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

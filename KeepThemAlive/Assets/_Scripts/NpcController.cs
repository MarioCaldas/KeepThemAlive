using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public static GameObject[] NPCs;

    public Transform target;
    float moveSpeed = 2f;
    float rotationSpeed = 2f;
    public bool canFollow = false;
    string npcTag = "NPC";
    public static float hp = 100f;
	
	void Update ()
    {
        if(canFollow)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == npcTag)
        {

            canFollow = false;
        }
    }
}

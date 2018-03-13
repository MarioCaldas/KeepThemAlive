using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjManager : MonoBehaviour
{
    public static GameObject[] Boxes;
    public static GameObject[] NPCs;

    bool isActive = false, hasInteracted = false;
    Transform player;
    public float radius = 3f;

    void Start ()
    {
        Boxes = GameObject.FindGameObjectsWithTag("Boxes");
        NPCs = GameObject.FindGameObjectsWithTag("NPC");
    }
	
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

	void Update ()
    {
		if(isActive && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if(distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
	}

    public void SetActive(Transform playerTransform)
    {
        isActive = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void DeactivateObject()
    {
        isActive = false;
        hasInteracted = false;
        player = null;
    }
}

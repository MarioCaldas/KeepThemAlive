using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjManager : MonoBehaviour {

    public static GameObject[] Boxes;
    public static GameObject[] NPCs;

    void Start ()
    {
        Boxes = GameObject.FindGameObjectsWithTag("Boxes");
        NPCs = GameObject.FindGameObjectsWithTag("NPC");

    }
	
	void Update ()
    {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceTravelTime : MonoBehaviour {

    Animator anim;

    public static bool travel, isTraveling;

    public static float timeTravel;

    private GameObject AmbulanceSpot;

    private Material greenMat;

    

	void Start ()
    {
        AmbulanceSpot = transform.GetChild(4).gameObject;

        greenMat = Resources.Load("GreenTransparent") as Material;
        timeTravel = 5f;
        travel = false;
        anim = GetComponent<Animator>();

	}
	
	void Update ()
    {

        Debug.Log("travel: " + travel);
        
        if(travel)
        {

            AmbulanceSpot.GetComponent<MeshRenderer>().enabled = false;

            anim.SetBool("bed", false);

            anim.SetBool("bedBack", true);
            anim.SetBool("travel", true);
        }
        else
        {

            anim.SetBool("travel", false);
            anim.SetBool("bedBack", false);
        }


    }



    void travelFalse()
    {
        if(transform.GetChild(3).GetChild(0).GetChild(0) != null)
        {
            Destroy(transform.GetChild(3).GetChild(0).GetChild(0).gameObject);
        }

        print("ai ai");
        travel = false;
    }

    void Bed()
    {    

        anim.SetBool("bed", true);
    }



}

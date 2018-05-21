using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceTravelTime : MonoBehaviour {

    public static Animator anim;

    public static bool travel, isTraveling;

    public static float timeTravel;

    private GameObject AmbulanceSpot;

    private Material greenMat;

	void Start ()
    {

        greenMat = Resources.Load("GreenTransparent") as Material;
        timeTravel = 5f;
        travel = false;
        anim = GetComponent<Animator>();

	}
	
	void Update ()
    {
       if(isTraveling && timeTravel > 0)
        {
            timeTravel -= Time.deltaTime;
        }
       else
        {
            Return();
        }
    }

    void Bed()
    {
        anim.SetBool("bed", true);
    }

    void Return()
    {
        anim.SetBool("travel", false);

        AmbulanceSpot = GameObject.Find("WeakSpot");

    }

    public static void Travel()
    {
        isTraveling = true;


        print("boi");

        anim.SetBool("travel", true);
    }
}

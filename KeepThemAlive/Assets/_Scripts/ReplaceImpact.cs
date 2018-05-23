using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceImpact : MonoBehaviour
{
    int metalWindowsNumber, metalDesksNumber;
    public static int windowshealthImpact = 0, deskshealthImpact = 0;
    public static int totalheathImpact;

	void Start ()
    {
        metalWindowsNumber = LoadData.metalWindows;
        metalDesksNumber = LoadData.metalDesks;
        //Debug.Log("Windows: " + metalWindowsNumber);
        //Debug.Log("Metal Desks: " + metalDesksNumber);
	}
	
	void Update ()
    {
        MetalWindowsImpact();
        MetalDesksImpact();

        totalheathImpact = MetalDesksImpact() + MetalWindowsImpact();
        //Debug.Log("Total health impact: " + totalheathImpact);
	}

    int MetalWindowsImpact()
    {
        if (metalWindowsNumber <= 3)
        {
            windowshealthImpact = 10;
        }

        else if (metalWindowsNumber >= 4 && metalWindowsNumber <= 8)
        {
            windowshealthImpact = 5;
        }

        else if (metalWindowsNumber >= 9)
        {
            windowshealthImpact = 2;
        }

        return windowshealthImpact;
    }

    int MetalDesksImpact()
    {
        if (metalDesksNumber <= 7)
        {
            deskshealthImpact = 20;
        }

        else if (metalWindowsNumber >= 8 && metalWindowsNumber <= 15)
        {
            deskshealthImpact = 10;
        }

        else if (metalWindowsNumber >= 15)
        {
            deskshealthImpact = 5;
        }

        return deskshealthImpact;
    }
}

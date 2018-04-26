using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

    private GameObject school;

    public GameObject wreckedWall;

	void Start ()
    {
        school = GameObject.Find("SchoolBuilding");

        for (int i = 0; i < school.transform.childCount; i++)
        {

            if (school.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Objects"))
            {

                if (school.transform.GetChild(i).tag == "Desk")
                {
                    if (GetRandom() < 10)
                    {
                        
                        school.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                else if (school.transform.GetChild(i).tag == "wall")
                {
                    if (GetRandom() < 8)
                    {
                       //GameObject wWall = Instantiate(wreckedWall, school.transform.GetChild(i).position, school.transform.GetChild(i).rotation);
                       
                        //school.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                else if (school.transform.GetChild(i).tag == "Window")
                {
                    school.transform.GetChild(i).gameObject.SetActive(false);

                }
            }

        }
    }

    int GetRandom()
    {
        int r = Random.Range(1, 11);
        return r;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private GameObject school;

    public GameObject WreckedWall;
    public GameObject WreckeDesk;
    private GameObject wreckedWindow;

	void Start ()
    {
        wreckedWindow = Resources.Load("GlassPieces") as GameObject;
        school = GameObject.Find("SchoolBuilding");

        if (school != null)
        {
            for (int i = 0; i < school.transform.childCount; i++)
            {
                Debug.Log("sdasdasdasd");

                if (school.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Objects"))
                {

                    if (school.transform.GetChild(i).tag == "Desk")
                    {
                        if (GetRandom() < 10)
                        {
                            school.transform.GetChild(i).gameObject.SetActive(false);
                            GameObject obj = Instantiate(WreckeDesk, school.transform.GetChild(i).position, Quaternion.Euler(0, Random.Range(0, 180), 0));
                            obj.transform.position += new Vector3(0, -3.46f, 0);
                        }
                    }

                    else if (school.transform.GetChild(i).tag == "wall")
                    {
                        if (GetRandom() < 1)
                        {
                            //GameObject wWall = Instantiate(wreckedWall, school.transform.GetChild(i).position, school.transform.GetChild(i).rotation);

                            school.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }

                    else if (school.transform.GetChild(i).tag == "Window")
                    {
                        school.transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;

                        GameObject glass = school.transform.GetChild(i).Find("Glass").gameObject;

                        GameObject brokeWindow = Instantiate(wreckedWindow, glass.transform.position, glass.transform.rotation);

                        glass.SetActive(false);


                    }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private GameObject school;

    public GameObject WreckedWall;
    public GameObject WreckeDesk;
    private GameObject wreckedWindow;
    private GameObject npc;

	void Start ()
    {
        //Timer.TotalPess = 0;
        npc = Resources.Load("npc") as GameObject;
        wreckedWindow = Resources.Load("GlassPieces") as GameObject;
        school = GameObject.Find("SchoolBuilding");

        List<GameObject> metalObj = new List<GameObject>();
        


        if (school != null)
        {
            for (int i = 0; i < school.transform.childCount; i++)
            {
                if (school.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Objects"))
                {
                    if (school.transform.GetChild(i).tag == "Desk")
                    {
                        if(GetRandom() < 5)
                        {
                           GameObject objNpc = Instantiate(npc, school.transform.GetChild(i).position + new Vector3(0, 1.5f, 0), school.transform.GetChild(i).rotation);
                           Rigidbody rb = objNpc.GetComponent<Rigidbody>();
                           rb.AddForce(new Vector3(0, Random.Range(0, 180), 0), ForceMode.Impulse);
                           Timer.TotalPess++;
                        }

                        if (GetRandom() < 10)
                        {
                            school.transform.GetChild(i).gameObject.SetActive(false);
                            GameObject obj = Instantiate(WreckeDesk, school.transform.GetChild(i).position + new Vector3(0,15,0), Quaternion.Euler(0, Random.Range(0, 180), 0));
                            obj.transform.position += new Vector3(0, -3.46f, 0);
                        }
                        else
                        {
                            metalObj.Add(school.transform.GetChild(i).gameObject);
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
                    else if(school.transform.GetChild(i).tag == "chair")
                    {

                        school.transform.GetChild(i).transform.rotation = Quaternion.Euler(0, Random.Range(0, 180), 0);
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

        GameObject[] MetalDesk = GameObject.FindGameObjectsWithTag("Desk");

        foreach (GameObject item in MetalDesk)
        {

            if (item.transform.parent == null)
            {
                metalObj.Add(item);
            }
        }

        for (int i = 0; i < metalObj.Count; i++)
        {
            //SURVIVALS
            if (GetRandom() < 5)
            {
                GameObject objNpc = Instantiate(npc, metalObj[i].transform.position - new Vector3(0, 4.5f, 0), metalObj[i].transform.rotation);
                Timer.TotalPess++;
            }
        }
    }

    int GetRandom()
    {
        int r = Random.Range(1, 11);
        return r;
    }
}

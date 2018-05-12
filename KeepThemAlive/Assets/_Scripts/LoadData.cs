﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private GameObject school;

    private GameObject WreckedWall;
    public GameObject WreckeDesk;
    private GameObject wreckedWindow;
    private GameObject healthyNpc;
    public GameObject hurtedNpc;

    // NPC stuffs
    public GameObject TimeCanvas;
    public GameObject WeakSpot;

    public static bool isDamaged = false;

	void Start ()
    {
        WreckedWall = Resources.Load("wWallParent") as GameObject;
        healthyNpc = Resources.Load("npc") as GameObject;
        //hurtedNpc = Resources.Load("HurtedNpc") as GameObject;
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
                        if(GetRandom() < 4)
                        {                          
                            isDamaged = true;
                            GameObject objNpc = Instantiate(hurtedNpc, school.transform.GetChild(i).position + new Vector3(0, -2.1f, 0), school.transform.GetChild(i).rotation);
                            objNpc.GetComponent<NpcHurted>().MetaSpot = WeakSpot;
                            objNpc.GetComponent<NpcHurted>().Canvas = TimeCanvas;
                            CanvasScript.TotalPess++;
                        }

                        if (GetRandom() < 10)
                        {
                            school.transform.GetChild(i).gameObject.SetActive(false);
                            GameObject obj = Instantiate(WreckeDesk, school.transform.GetChild(i).position + new Vector3(0, 3, 0), Quaternion.Euler(0, Random.Range(0, 180), 0));
                            //obj.transform.position += new Vector3(0, -3.46f, 0);
                        }
                        else
                        {
                            metalObj.Add(school.transform.GetChild(i).gameObject);
                        }
                    }
                    else if (school.transform.GetChild(i).tag == "wall")
                    {
                        if (GetRandom() > 10)
                        {
                            //GameObject wWall = Instantiate(WreckedWall, school.transform.GetChild(i).GetChild(0).position, school.transform.GetChild(i).GetChild(0).rotation);
                            //wWall.transform.localScale = school.transform.GetChild(i).localScale;

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
                GameObject objNpc = Instantiate(healthyNpc, metalObj[i].transform.position - new Vector3(0, 4.5f, 0), metalObj[i].transform.rotation);
                CanvasScript.TotalPess++;

                isDamaged = false;
            }
        }
    }

    int GetRandom()
    {
        int r = Random.Range(1, 11);
        return r;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    private GameObject school;

    private GameObject WreckedWall;
    public GameObject WreckeDesk;
    private GameObject wreckedWindow;
    private GameObject proneNpc;
    private GameObject hurtedNpc;


    // NPC stuffs
    public GameObject TimeCanvas;
    public GameObject WeakSpot;

    public static bool isDamaged = false;

    //rego
    public static int metalWindows = 0;
    public static int metalDesks = 0;

    DoorController doorController;

    public GameObject Player;


	void Start ()
    {
        WreckedWall = Resources.Load("wWallParent") as GameObject;
        proneNpc = Resources.Load("ProneNpc") as GameObject;
        hurtedNpc = Resources.Load("") as GameObject;
        //hurtedNpc = Resources.Load("HurtedNpc") as GameObject;
        wreckedWindow = Resources.Load("GlassPieces") as GameObject;
        school = GameObject.Find("SchoolBuilding");


        //rego
        //doorController = school.GetComponentInChildren<DoorController>();
        //doorController.enabled = true;

        List<GameObject> metalObj = new List<GameObject>();

        Debug.Log(proneNpc);
            
        if (school != null)
        {
            for (int i = 0; i < school.transform.childCount; i++)
            {
                if (school.transform.GetChild(i).gameObject.layer == LayerMask.NameToLayer("Objects"))
                {
                    if (school.transform.GetChild(i).tag == "Desk")
                    {

                        if (GetRandom() < 4)
                        {                          
                            isDamaged = true;
                            Instantiate(proneNpc, school.transform.GetChild(i).position - new Vector3(0,2,0), school.transform.GetChild(i).rotation);
                            proneNpc.GetComponent<NpcController>().isCrouch = false;

                            //npc.GetComponent<NpcHurted>().MetaSpot = WeakSpot;
                            //npc.GetComponent<NpcHurted>().Canvas = TimeCanvas;
                            CanvasScript.TotalPess++;
                        }

                        if (GetRandom() < 10)
                        {
                            school.transform.GetChild(i).gameObject.SetActive(false);
                            GameObject obj = Instantiate(WreckeDesk, school.transform.GetChild(i).position + new Vector3(0, 10, 0), Quaternion.Euler(0, Random.Range(0, 180), 0));

                            //obj.transform.GetChild(0).DetachChildren();
                            //obj.transform.GetChild(1).DetachChildren();



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

                        //rego
                        //metalWindows++;
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
            //rego
            metalDesks++;
            //SURVIVALS
            if (GetRandom() > 0)
            {
                Debug.Log("vasddd");

                Instantiate(proneNpc, metalObj[i].transform.position - new Vector3(0,2,0), metalObj[i].transform.rotation);

                proneNpc.GetComponent<NpcController>().isCrouch = true;

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

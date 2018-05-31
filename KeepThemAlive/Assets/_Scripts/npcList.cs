using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class npcList : MonoBehaviour {

    public List<GameObject> hurtedNpcs = new List<GameObject>();
    public List<GameObject> proneNpc = new List<GameObject>();

    GameObject[] MetalDesk;

    GameObject[] WreckedDesk;

    GameObject[] MetalDoor; 


    public List<GameObject> metalDeskList = new List<GameObject>();
    public List<GameObject> wreckedDeskList = new List<GameObject>();
    public List<GameObject> metalDoorList = new List<GameObject>();


    public Canvas Healthcanvas;

    Image healthBar;

    void Awake ()
    {
        


       MetalDesk = GameObject.FindGameObjectsWithTag("MetalDesk");

       WreckedDesk = GameObject.FindGameObjectsWithTag("wreckedDesk");

       MetalDoor = GameObject.FindGameObjectsWithTag("metalDoor");

        foreach (GameObject item in MetalDesk)
        {
            Debug.Log("kkkkkk");

            //if (item.transform.parent == null)
            //{

                metalDeskList.Add(item);
            //}
        }

        foreach (GameObject item in WreckedDesk)
        {
            if (item.transform.parent == null)
            {
                wreckedDeskList.Add(item);
            }
        }

        foreach (GameObject item in MetalDoor)
        {
            item.transform.GetChild(0).transform.Rotate(item.transform.GetChild(0).rotation.x, item.transform.GetChild(0).rotation.y, 130);

            metalDoorList.Add(item);
            
        }

        InstantiateProneNpc();

        InstantiateNpcFodido();

        InstantiateDoorNpc();
    }

    void Update ()
    {
       


    }

    void InstantiateDoorNpc()
    {
        for (int i = 0; i < metalDoorList.Count; i++)
        {
            GameObject npc = Instantiate(proneNpc[0], metalDoorList[i].transform.position - new Vector3(0, 2, 0), metalDoorList[i].transform.rotation);

            npc.AddComponent<NpcController>();

            npc.GetComponent<NpcController>().isOnDoor = true;
        }
    }

    void InstantiateProneNpc()
    {
        for (int i = 0; i < metalDeskList.Count; i++)
        {
            /*int random = Random.Range(0, proneNpc.Count)*/

            GameObject npc = Instantiate(proneNpc[0], metalDeskList[i].transform.position - new Vector3(0, 2, 0), metalDeskList[i].transform.rotation);

            npc.GetComponent<Animator>().SetBool("crawl", true);

            npc.AddComponent<NpcController>();
            
        }
    }

    void InstantiateNpcFodido()
    {
        for (int i = 0; i < wreckedDeskList.Count; i++)
        {
            /*int random = Random.Range(0, proneNpc.Count)*/
            

            GameObject npc = Instantiate(proneNpc[0], wreckedDeskList[i].transform.position - new Vector3(0,15.5f,0) , wreckedDeskList[i].transform.rotation);

            Canvas canvas = Instantiate(Healthcanvas, null, true);

            canvas.transform.position = wreckedDeskList[i].transform.position - Vector3.up * 2;


            npc.GetComponent<Animator>().SetBool("hurted", true);

            Destroy(npc.GetComponent<NavMeshAgent>());

            npc.GetComponent<BoxCollider>().center = new Vector3(0, 2.22f, 0);

            npc.gameObject.tag = "HurtedNPC";

            npc.AddComponent<NpcHurted>();

            canvas.transform.SetParent(npc.transform);


            //npc.GetComponent<NpcHurted>().SetHeath(Random.Range(0,100));
        }

    }

}

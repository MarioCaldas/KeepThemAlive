using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcList : MonoBehaviour {

    public List<GameObject> hurtedNpcs = new List<GameObject>();
    public List<GameObject> proneNpc = new List<GameObject>();

    GameObject[] MetalDesk;

    GameObject[] WreckedDesk;


    public List<GameObject> metalDeskList = new List<GameObject>();
    public List<GameObject> wreckedDeskList = new List<GameObject>();


    void Start ()
    {
       MetalDesk = GameObject.FindGameObjectsWithTag("Desk");

       WreckedDesk = GameObject.FindGameObjectsWithTag("wreckedDesk");


        foreach (GameObject item in MetalDesk)
        {
            if (item.transform.parent == null)
            {
                metalDeskList.Add(item);
            }
        }

        foreach (GameObject item in WreckedDesk)
        {
            if (item.transform.parent == null)
            {
                wreckedDeskList.Add(item);
            }
        }

        InstantiateProneNpc();

        InstantiateNpcFodido();
    }

    void Update ()
    {
       


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
            

            GameObject npc = Instantiate(proneNpc[0], wreckedDeskList[i].transform.position - new Vector3(0,7,0) , wreckedDeskList[i].transform.rotation);

            npc.GetComponent<Animator>().SetBool("hurted", true);

            npc.AddComponent<NpcHurted>();

        }

    }

}

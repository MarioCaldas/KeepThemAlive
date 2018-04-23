﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
    public GameObject School;
    List<Objects> ListObj;
    MoneyManager Money;
    int CostAux = 0;

    public Button substituteButton;
    GameObject subButtonGameObject;
    GameObject objToSwitch, newObjectPrefab;

    string objectTag = "Object";
    string deskTag = "Desk";
    string windowTag = "Window";
    string metalDeskTag = "MetalDesk";

    public Camera mainCamera;
    public RaycastHit hit;

    bool canSwitch = false;

    public Material greenMaterial;

    public List<Material> ListMaterials = new List<Material>();

    public List<GameObject> selectedObjs = new List<GameObject>();

    public GameObject saveObj;

    public int childCount;

    void Start ()
    {
        Money = transform.GetComponent<MoneyManager>();
        ListObj = new List<Objects>();
        AddList();

        Button subButton = substituteButton.GetComponent<Button>();
        subButton.onClick.AddListener(InstantiateNewObject);

        subButtonGameObject = subButton.gameObject;
        //subButtonGameObject.SetActive(false);

        mainCamera = Camera.main;

        saveObj = null;

    }

    void InstantiateNewObject()
    {
        if (objToSwitch != null)
        {
            if (Money.CanBuy(CostAux))
            {
                for (int i = 0; i < selectedObjs.Count; i++)
                {
                    Money.BuySomething(CostAux);
                    GameObject newObj = Instantiate(newObjectPrefab, selectedObjs[i].transform.position, selectedObjs[i].transform.rotation);
                    Destroy(selectedObjs[i]);
                }
                //Money.BuySomething(CostAux);
                //GameObject newObject = Instantiate(newObjectPrefab, objToSwitch.transform.position, objToSwitch.transform.rotation);
                //Destroy(objToSwitch);
                selectedObjs.Clear();
            }
        }
    }

    public void SwitchObject(RaycastHit hit)
    {
        //subButtonGameObject.SetActive(true);

        //switch (hit.transform.tag)
        //{
        //    case "Desk":
        //        objToSwitch = hit.transform.gameObject;
        //        newObjectPrefab = Resources.Load("DeskMetal") as GameObject;
        //        break;

        //    case "Window": Debug.Log("windooooh");
        //        objToSwitch = hit.transform.gameObject;

        //        break;
        //    default:
        //        break;
        //}


        // Numa lista com todos os elementos pre-colocados procura-se o que tem a tag x
        // este foreach funciona para todos....
        foreach (Objects ScriptObj in ListObj)
        {
            if (ScriptObj.GetTag() == hit.transform.tag)
            {
                // ESTE É O NOSSO OBJECTO, AGORA ACEDO A TUDO DELE;
                objToSwitch = hit.transform.gameObject;
                newObjectPrefab = ScriptObj.GetGO();
                CostAux = ScriptObj.GetCost();


               
            }
        }
    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
            {
                SwitchObject(hit);


                //highlight objects
                childCount = hit.collider.transform.childCount;

                saveObj = hit.transform.gameObject;


                selectedObjs.Add(saveObj);


                for (int i = 0; i < selectedObjs.Count; i++)
                {
                    for (int u = 0; u < childCount; u++)
                    {
                        ListMaterials.Add(selectedObjs[i].transform.GetChild(u).GetComponent<MeshRenderer>().material);

                        MeshRenderer mesh = selectedObjs[i].transform.GetChild(u).gameObject.GetComponent<MeshRenderer>();
                        mesh.material = greenMaterial;
                    }
                }

            }
          

        }

        //unhighligh objects
        if (Input.GetMouseButtonDown(1))
        {
            
            for (int i = 0; i < selectedObjs.Count; i++)
            {
                for (int u = 0; u < childCount; u++)
                {
                    MeshRenderer originalMesh = selectedObjs[i].transform.GetChild(u).gameObject.GetComponent<MeshRenderer>();
                    originalMesh.material = ListMaterials[u];
                }
            }

            selectedObjs.Clear();
            ListMaterials.Clear();
        }

        Debug.Log("list: " + selectedObjs.Count);
       
    }

    void AddList()
    {
        Objects objScript = new Objects();
        objScript.SetCost(100);
        objScript.SetGO(Resources.Load("DeskMetal") as GameObject);
        objScript.SetTag("Desk");
        ListObj.Add(objScript);

        objScript = new Objects();
        objScript.SetCost(35);
        objScript.SetGO(Resources.Load("WindowMetal") as GameObject);
        objScript.SetTag("Window");
        ListObj.Add(objScript);
        
    }
}

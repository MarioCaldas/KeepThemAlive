using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
    public GameObject School;
    List<Objects> ListObj;
    MoneyManager Money;
    int CostAux = 0;
    
    GameObject objToSwitch, newObjectPrefab;
    
    public Camera mainCamera;
    public RaycastHit hit;
    
    public Material greenMaterial;

    public List<Material> ListMaterials = new List<Material>();

    public List<GameObject> selectedObjs = new List<GameObject>();
    
    // PQ PUBLICO?? é preciso?
    public GameObject newObj;

    public int childCount;

    void Start()
    {
        Money = transform.GetComponent<MoneyManager>();
        ListObj = new List<Objects>();
        AddList();

        mainCamera = Camera.main;

        newObjectPrefab = null;
    }

    public void InstantiateNewObject()
    {
        if (objToSwitch != null)
        {
            if (Money.CanBuy(CostAux))
            {
                for (int i = 0; i < selectedObjs.Count; i++)
                {
                    Money.BuySomething(CostAux);
                    newObjectPrefab = Instantiate(newObjectPrefab, selectedObjs[i].transform.position, selectedObjs[i].transform.rotation);
                    newObjectPrefab.transform.localScale = selectedObjs[i].transform.localScale;

                    DontDestroyOnLoad(newObjectPrefab);

                    Destroy(selectedObjs[i]);
                }

                selectedObjs.Clear();
                ListMaterials.Clear();
                objToSwitch = null;
            }
        }
    }

    void SwitchObject(GameObject obj)
    {
        foreach (Objects ScriptObj in ListObj)
        {
            if (ScriptObj.GetTag() == obj.transform.tag)
            {
                // ESTE É O NOSSO OBJECTO, AGORA ACEDO A TUDO DELE;
                objToSwitch = obj.transform.gameObject;
                newObjectPrefab = ScriptObj.GetGO();
                CostAux = ScriptObj.GetCost();
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
            {
                //highlight objects

                newObjectPrefab = hit.transform.gameObject;

                childCount = newObjectPrefab.transform.childCount;

                AddSelectedList(newObjectPrefab);
                
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
            UnhighlighObj();
        }
    }

    void UnhighlighObj()
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


    void AddSelectedList(GameObject SelObj)
    {
        if (selectedObjs.Count == 0)
        {
            selectedObjs.Add(SelObj);
            SwitchObject(SelObj);

        }
        else if (selectedObjs[0].tag == SelObj.tag)
        {
            selectedObjs.Add(SelObj);
            SwitchObject(SelObj);
        }

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


        objScript = new Objects();
        objScript.SetCost(200);
        objScript.SetGO(Resources.Load("Wall") as GameObject);
        objScript.SetTag("wall");
        ListObj.Add(objScript);

    }
}

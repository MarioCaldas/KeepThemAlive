using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeObjects : MonoBehaviour
{
    public List<GameObject> ListAllObj;
    MoneyManager Money;
    int CostAux = 0;

    public static int Cost;

    bool CanPickObj = true;

    //rego
    string sourceToTranfer = "";
    
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

                    if (selectedObjs[i].gameObject.tag == "Window")
                    {
                        LoadData.metalWindows++;
                    }

                    if (selectedObjs[i].gameObject.tag == "wall")
                    {
                        ChangeWallMaterial(selectedObjs[i].gameObject);
                    }

                    else
                    {
                        Debug.Log("Entrei!");
                        newObjectPrefab = Instantiate(newObjectPrefab, selectedObjs[i].transform.position, selectedObjs[i].transform.rotation);
                        newObjectPrefab.transform.localScale = selectedObjs[i].transform.localScale;
                        Destroy(selectedObjs[i]);

                    }
                    Money.BuySomething(CostAux);
                    
                    DontDestroyOnLoad(newObjectPrefab);
                }

                selectedObjs.Clear();
                ListMaterials.Clear();
                objToSwitch = null;
            }

            SourceDescription.Clear();
            CanPickObj = true;
        }

        //rego
        Cost = 0;
    }

    void SwitchObject(GameObject obj)
    {
        for (int i = 0; i < ListAllObj.Count; i++)
        {
            if (ListAllObj[i].GetComponent<Objects>().tag == obj.transform.tag)
            {              
                objToSwitch = obj;
                newObjectPrefab = ListAllObj[i].GetComponent<Objects>().Object;
                CostAux = ListAllObj[i].GetComponent<Objects>().cost;
                Cost += CostAux;
            }
        }
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (Input.GetMouseButtonDown(0) /*&& CanPickObj*/)
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
                {
                    //rego
                    sourceToTranfer = hit.collider.tag;

                    //highlight objects

                    newObjectPrefab = hit.transform.gameObject;
                    
                    AddSelectedList(newObjectPrefab);

                    HighlightObj();

                    CanPickObj = false;

                    Debug.Log("childs: " + childCount);
                }
            }
        }

        //unhighligh objects
        if (Input.GetMouseButtonDown(1))
        {
            UnhighlighObj();
            SourceDescription.Clear();
            CanPickObj = true;
        }
    }

    public void ActivateQuestion()
    {
        Debug.Log("Source: " + sourceToTranfer);
        SourceDescription.SetQuestion(sourceToTranfer);
    }

    void ChangeWallMaterial(GameObject wall)
    {
        Debug.Log("ola");

        Material concreteMat = Resources.Load<Material>("Concrete");

        wall.transform.GetChild(0).GetComponent<MeshRenderer>().material = concreteMat;
    }

    void HighlightObj()
    {
        for (int i = 0; i < selectedObjs.Count; i++)
        {
            for (int u = 0; u < childCount; u++)
            {
                //Debug.Log("bom dia");
                
                ListMaterials.Add(selectedObjs[i].transform.GetChild(u).GetComponent<MeshRenderer>().material);
                
                MeshRenderer mesh = selectedObjs[i].transform.GetChild(u).GetComponent<MeshRenderer>();
                mesh.material = greenMaterial;
            }
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
            childCount = newObjectPrefab.transform.childCount;
            SourceDescription.ChangeDescription(newObjectPrefab.tag);

            selectedObjs.Add(SelObj);
            SwitchObject(SelObj);
        }

        else if (selectedObjs[0].tag == SelObj.tag)
        {
            childCount = newObjectPrefab.transform.childCount;
            SourceDescription.ChangeDescription(newObjectPrefab.tag);

            selectedObjs.Add(SelObj);
            SwitchObject(SelObj);
        }
    }
}

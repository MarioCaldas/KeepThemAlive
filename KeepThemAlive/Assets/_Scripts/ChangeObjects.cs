using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
    public GameObject School;
    public List<GameObject> ListAllObj;
    MoneyManager Money;
    int CostAux = 0;

    bool CanPickObj = true;
    
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
            SourceDescription.Clear();
            CanPickObj = true;
        }
    }

    void SwitchObject(GameObject obj)
    {
        for (int i = 0; i < ListAllObj.Count; i++)
        {
            if (ListAllObj[i].GetComponent<Objects>().tag == obj.transform.tag)
            {
                objToSwitch = obj.transform.gameObject;
                newObjectPrefab = ListAllObj[i].GetComponent<Objects>().Object;
                CostAux = ListAllObj[i].GetComponent<Objects>().cost;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) /*&& CanPickObj*/)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, 1 << 9))
            {

                if (CanPickObj || hit.transform.gameObject.tag == newObjectPrefab.tag)
                {
                    //highlight objects

                    newObjectPrefab = hit.transform.gameObject;

                    SourceDescription.ChangeDescription(hit.collider.tag);

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
                    CanPickObj = false;
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
}

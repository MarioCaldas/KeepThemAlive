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

    public GameObject saveObj, newObj;

    public int childCount;

    void Start()
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
                    newObj = Instantiate(newObjectPrefab, selectedObjs[i].transform.position, selectedObjs[i].transform.rotation);
                    newObj.transform.localScale = selectedObjs[i].transform.localScale;

                    DontDestroyOnLoad(newObj);

                    SceneData.ChangedObjList.Add(selectedObjs[i].name);

                    Debug.Log("obj: " + SceneData.ChangedObjList[i]);

                    Destroy(selectedObjs[i]);
                }

                selectedObjs.Clear();
                ListMaterials.Clear();
                objToSwitch = null;
            }
        }
    }

    public void SwitchObject(GameObject obj)
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

                saveObj = hit.transform.gameObject;

                childCount = saveObj.transform.childCount;

                AddSelectedList(saveObj);


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

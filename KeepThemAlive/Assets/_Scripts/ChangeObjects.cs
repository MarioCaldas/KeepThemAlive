using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
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
	}

    void InstantiateNewObject()
    {
        if (objToSwitch != null)
        {
            if (Money.CanBuy(CostAux))
            {
                Money.BuySomething(CostAux);
                GameObject newObject = Instantiate(newObjectPrefab, objToSwitch.transform.position, objToSwitch.transform.rotation);
                Destroy(objToSwitch);
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

            if (Physics.Raycast(ray, out hit, 1000))
            {
                SwitchObject(hit);
            }
        }
    }

    void AddList()
    {
        Objects objScript = new Objects();
        objScript.SetCost(100);
        objScript.SetGO(Resources.Load("DeskMetal") as GameObject);
        objScript.SetTag("Desk");
        ListObj.Add(objScript);

        /*
        objScript.SetCost(35);
        objScript.SetGO(Resources.Load("DeskMetal") as GameObject);
        objScript.SetTag("Window");
        ListObj.Add(objScript);
        */
    }
}

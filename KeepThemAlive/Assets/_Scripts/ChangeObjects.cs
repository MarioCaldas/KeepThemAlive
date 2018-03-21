using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
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
        Button subButton = substituteButton.GetComponent<Button>();
        subButton.onClick.AddListener(InstantiateNewObject);

        subButtonGameObject = subButton.gameObject;
        subButtonGameObject.SetActive(false);

        mainCamera = Camera.main;
	}

    void InstantiateNewObject ()
    {
        Debug.Log("You can switch ma friend");
        
        if(objToSwitch != null)
        {
            GameObject newObject = Instantiate(newObjectPrefab, objToSwitch.transform.position, objToSwitch.transform.rotation);
            Destroy(objToSwitch);
        }    
    }

    public void SwitchObject(RaycastHit hit)
    {
        subButtonGameObject.SetActive(true);

        switch (hit.transform.tag)
        {
            case "Desk":
                objToSwitch = hit.transform.gameObject;
                newObjectPrefab = Resources.Load("DeskMetal") as GameObject;
                break;

            case "Window": Debug.Log("windooooh");
                objToSwitch = hit.transform.gameObject;

                break;
            default:
                break;
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
}

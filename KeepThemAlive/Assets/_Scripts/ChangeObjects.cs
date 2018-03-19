using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeObjects : MonoBehaviour
{
    public Button substituteButton;
    string objectTag = "Object";

    string deskTag = "Desk";
    string windowTag = "Window";

    public Camera mainCamera;

	void Start ()
    {
        Button subButton = substituteButton.GetComponent<Button>();
        subButton.onClick.AddListener(SubstituteObject);

        mainCamera = Camera.main;
	}

    void SubstituteObject()
    {
     

    }

    void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                ObjectClicked(hit);
            }
        }
    }

    void ObjectClicked(RaycastHit hit)
    {
        if(hit.transform.tag == deskTag)
        {
            Debug.Log("Desk seu fffffffilho da puta");
        }

        else if(hit.transform.tag == windowTag)
        {
            Debug.Log("Windoooooooooh");
        }
    }
}

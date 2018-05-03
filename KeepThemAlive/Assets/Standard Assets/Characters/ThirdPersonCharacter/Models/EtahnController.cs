using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtahnController : MonoBehaviour {

    Animator anim;
    public Camera mainCamera;
    public LayerMask ObjLayer;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (anim.GetBool("PickShoulder"))
            {
                anim.SetBool("PickShoulder", false);
                transform.GetChild(0).GetChild(0).position = transform.position + transform.forward * 3;
                transform.GetChild(0).GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
                transform.GetChild(0).DetachChildren();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Walk", true);
        }
        else if (Input.GetKeyUp(KeyCode.W) && anim.GetBool("Walk"))
        {
            anim.SetBool("Walk", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.Log("Hit: " + hit.collider.name);
                if (hit.collider.tag == "NPC")
                {
                    //hit.transform.position = transform.GetChild(0).localPosition;
                    //hit.transform.rotation = transform.GetChild(0).localRotation;
                    anim.SetBool("PickShoulder", true);
                    hit.transform.SetParent(transform.GetChild(0));
                    transform.GetChild(0).GetChild(0).position = transform.GetChild(0).position;
                    transform.GetChild(0).GetChild(0).rotation = transform.GetChild(0).rotation;
                }
            }
        }
    }
}

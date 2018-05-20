﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class FiremanController : MonoBehaviour {

    Animator anim;

    public Camera mainCamera;
    public LayerMask GroundLayer;
    public LayerMask ObjLayer;
    public LayerMask DoorLayer;

    NavMeshAgent navMeshAgent;


    RaycastHit hit;

    float walkAnimSpeed = 0f;

    float runAnimSpeed = 0f;


    bool doubleClick = false;

    GameObject raycastedObj;

    public GameObject objPos;

    public GameObject npcPos;

    GameObject inHandsObj;

    bool canRun = true;

    public static bool openDoor;

    public static bool freeLook;

    GameObject door;

    bool isMoving = false;


    void Start ()
    {
        openDoor = false;

        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        inHandsObj = null;
        raycastedObj = null;
    }

    void Update ()
    {
        CheckRaycast();

        Animations();
    }






    void Detach(GameObject obj)
    {
        canRun = true;


        if (!obj.GetComponent<Rigidbody>())
        {
            
        }

        if (obj.tag == "NPC" || obj.tag == "HurtedNPC")
        {
            NpcHurted.carried = false;

            anim.SetBool("PickNpc", false);

            npcPos.transform.DetachChildren();
        
        }
        else
        {
            anim.SetBool("PickObj", false);

            objPos.transform.DetachChildren();

            //obj.transform.GetChild(1).DetachChildren();
            
        }

    }


    void CheckRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //??
        //FireManAnimator.isMoving = true;

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(ray, out hit, 1000, GroundLayer))
            {

                MoveToPoint(hit);
            }         

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1000, ObjLayer))
            {
                raycastedObj = hit.transform.gameObject;
                //raycastedObj.transform.position = new Vector3(transform.position.x + 2, transform.position.y + 3, transform.position.z);
                Pick(raycastedObj);
            }
            else
            {
                Debug.Log("pooasdas");
            }           
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Detach(inHandsObj);
        }

    }

    void PlayerLookTo()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(freeLook)
        {
            if (Physics.Raycast(ray, out hit, 1000, GroundLayer))
            {
                transform.LookAt(hit.point, Vector3.up);
            }

        }
        //else
        //{
        //    transform.LookAt(door.transform, Vector3.up);
        //}
     

      
        
    }

    

    void Pick(GameObject hitObj)
    {
        inHandsObj = hit.transform.gameObject;

        canRun = false;

        if(hitObj.tag == "NPC")
        {
            NpcController.evacuate = true;
        }

        else if (hitObj.tag == "HurtedNPC")
        {


            hit.collider.transform.SetParent(npcPos.transform);
            hit.collider.transform.position = npcPos.transform.position;
            hit.collider.transform.rotation = npcPos.transform.rotation;

            NpcHurted.carried = true;

            anim.SetBool("PickNpc", true);
            
        }
        
        // mesas partidas
        else if (Vector3.Distance(transform.position, hitObj.transform.position) < 10)
        {

            hit.transform.SetParent(objPos.transform);

            Destroy(hit.collider.transform.GetChild(0));

            hit.collider.transform.position = objPos.transform.position;
            hit.collider.transform.rotation = objPos.transform.rotation;


            anim.SetBool("PickObj", true);
        }
   

    }


    //Double Click Run
    void OnGUI()
    {

        //if double click
        if (Event.current.isMouse && Event.current.button == 1 && Event.current.clickCount > 1)
        {
            doubleClick = true;
        }
    

    }



    void Animations()
    {
        if (isMoving)
        {
            if(doubleClick && canRun)
            {
                navMeshAgent.speed = 22;

                anim.SetBool("run", true);
            }
            else
            {
                navMeshAgent.speed = 10;
                anim.SetBool("walk", true);

            }
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);

        }

        if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 1f)
        {
            doubleClick = false;

            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

    }



    void MoveToPoint(RaycastHit hit)
    {

        navMeshAgent.SetDestination(hit.point);
        navMeshAgent.speed = 10f;


      
    }

    void OpenDoor()
    {
        //AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo(2);
        door.AddComponent<DoorController>();


        openDoor = true;

        print("pe na porta");
    }


    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Door")
        {
            door = collision.transform.gameObject;

            freeLook = false;


            transform.LookAt(collision.transform.position);


            anim.SetBool("Kick", true);


            print("ola");
            //OpenDoor(collision.transform.gameObject);
        }
        else
        {

        }

        navMeshAgent.velocity = Vector3.zero;

        navMeshAgent.ResetPath();

    }
}

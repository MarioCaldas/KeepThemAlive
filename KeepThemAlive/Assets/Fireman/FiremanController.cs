using System.Collections;
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


    bool isRun = false;

    GameObject raycastedObj;


    void Start ()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        raycastedObj = null;
    }

    void Update ()
    {
        CheckRaycast();


        if (isRun)
        {
            runAnimSpeed += Time.deltaTime * 2;

            navMeshAgent.speed = 22;
            runAnimSpeed = Mathf.Clamp(runAnimSpeed, 0.5f, 1f);
            Animations(runAnimSpeed);


        }
        else
        {
            runAnimSpeed -= Time.deltaTime * 2;

            navMeshAgent.speed = 10;

        }

    }


    void CheckRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        

        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(ray, out hit, 1000, GroundLayer))
            {
                MoveToPoint(hit);
            }

            if (Physics.Raycast(ray, out hit, 1000, ObjLayer))
            {
                raycastedObj = hit.transform.gameObject;

            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            Pick(raycastedObj);
        }


    }


    void Pick(GameObject hitObj)
    {

        if (Vector3.Distance(transform.position, hitObj.transform.position) < 10)
        {
            hit.collider.transform.SetParent(transform.GetChild(1));

            anim.SetTrigger("pick");
        }

    }


    //Double Click Run
    void OnGUI()
    {

        //if double click
        if (Event.current.isMouse && Event.current.button == 1 && Event.current.clickCount > 1)
        {
            isRun = true;
        }

        if (navMeshAgent.velocity != Vector3.zero)
        {
            walkAnimSpeed += Time.deltaTime;
        }
        else
        {
            walkAnimSpeed -= Time.deltaTime;
            isRun = false;
        }

        walkAnimSpeed = Mathf.Clamp(walkAnimSpeed, 0f, .5f);

        Animations(walkAnimSpeed);

    }



    void Animations(float animSpeed)
    {
        anim.SetFloat("Speed", animSpeed);

    }



    void MoveToPoint(RaycastHit hit)
    {
        navMeshAgent.SetDestination(hit.point);
        navMeshAgent.speed = 10f;
    }

    void OpenDoor(GameObject door)
    {
        AnimatorStateInfo currInfo = anim.GetCurrentAnimatorStateInfo(2);


        transform.LookAt(door.transform.position);

        transform.position = door.transform.position;

        anim.SetTrigger("kick");

        print("pe na porta");
    }


    private void OnCollisionEnter(Collision collision)
    {

        if(collision.transform.tag == "Door")
        {
            OpenDoor(collision.transform.gameObject);
        }

        navMeshAgent.velocity = Vector3.zero;

        navMeshAgent.ResetPath();

    }
}

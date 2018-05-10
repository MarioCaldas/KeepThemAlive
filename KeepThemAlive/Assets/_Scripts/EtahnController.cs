using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EtahnController : MonoBehaviour {

    Animator anim;
    public Camera mainCamera;
    public LayerMask GroundLayer;
    public LayerMask ObjLayer;
    NavMeshAgent navMeshAgent;
    bool move;
    string WalkOrRun;
    bool GoToObj;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        move = false;
        WalkOrRun = "Run";
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
                transform.GetChild(0).GetChild(0).GetComponent<NpcHurted>().LeaveMeta();
                transform.GetChild(0).DetachChildren();
            }
            else
            {
                Debug.Log("Entrei");
                LeaveBox();
            }

            anim.SetBool("Walk", false);
            navMeshAgent.speed = 15f;
            WalkOrRun = "Run";
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveToPoint();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, ObjLayer))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < 10)
                {   
                    Debug.Log("Hit: " + hit.collider.name);
                    if (hit.collider.tag == "NPC")
                    {
                        anim.SetBool(WalkOrRun, false);
                        anim.SetBool("PickShoulder", true);
                        hit.transform.SetParent(transform.GetChild(0));
                        transform.GetChild(0).GetChild(0).position = transform.GetChild(0).position;
                        transform.GetChild(0).GetChild(0).rotation = transform.GetChild(0).rotation;
                        transform.GetChild(0).GetChild(0).GetComponent<NpcHurted>().GoMeta();
                        navMeshAgent.speed = 7f;
                        WalkOrRun = "Walk";
                    }
                    else if (hit.collider.tag == "Desk")
                    {
                        anim.SetBool(WalkOrRun, false);
                        anim.SetBool("PickObj", true);
                        PickUpBoxes();
                    }
                }
                else
                {
                    MoveToPoint();
                    navMeshAgent.stoppingDistance = 5;
                }   
            }
        }
        
        if (navMeshAgent.remainingDistance <= 1)
        {
            anim.SetBool(WalkOrRun, false);
        }
        else
        {
            anim.SetBool(WalkOrRun, true);
        }
    }

    void MoveToPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, GroundLayer))
        {
            navMeshAgent.stoppingDistance = 1;
            // se o raycast tocar no chao anda...
            navMeshAgent.SetDestination(hit.point);
            //navMeshAgent.speed = 15f;
        }

        if (Physics.Raycast(ray, out hit, 100, ObjLayer))
        {
            // se o raycast passar um obj > StopDistance
            navMeshAgent.stoppingDistance = 10;
        }
        else
        {
            // se o raycast nao atravessar obj stopDistance = default
            navMeshAgent.stoppingDistance = 1;
        }

        if (Physics.Raycast(ray, out hit, 100, GroundLayer))
        {
            // se o raycast tocar no chao anda...
            navMeshAgent.SetDestination(hit.point);
        }
    }

    void PickUpBoxes()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, ObjLayer))
        {
            if (Vector3.Distance(transform.position, hit.transform.position) < 10)
            {
                //isPickedObj = true;
                hit.collider.transform.SetParent(transform.GetChild(1));

                transform.GetChild(1).GetChild(0).position = transform.GetChild(1).position;

                transform.GetChild(1).GetChild(0).rotation = transform.GetChild(1).rotation;

                navMeshAgent.speed = 10f;
            }
            else
            {
                Debug.Log("Entrei!");
                MoveToPoint();
            }
        }
    }

    void LeaveBox()
    {
        Debug.Log("Largar!");

        Transform aux = transform.GetChild(1).GetChild(0);

        anim.SetBool("PickObj", false);
        transform.GetChild(1).Rotate(0, 0, 10);
        transform.GetChild(1).GetChild(0).SetParent(null);
        transform.GetChild(1).Rotate(0, 0, -10);

        aux.gameObject.AddComponent<Rigidbody>();
        aux.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        //isPickedObj = false;
    }
}

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
    bool GoToObj;

    private void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        move = false;
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
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveToPoint();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (Vector3.Distance(transform.position, hit.transform.position) < 10)
                {
                    Debug.Log("Hit: " + hit.collider.name);
                    if (hit.collider.tag == "NPC")
                    {
                        anim.SetBool("PickShoulder", true);
                        hit.transform.SetParent(transform.GetChild(0));
                        transform.GetChild(0).GetChild(0).position = transform.GetChild(0).position;
                        transform.GetChild(0).GetChild(0).rotation = transform.GetChild(0).rotation;
                        transform.GetChild(0).GetChild(0).GetComponent<NpcHurted>().GoMeta();
                    }
                }
                else
                {
                    MoveToPoint();
                    navMeshAgent.stoppingDistance = 5;
                }   
            }
        }
        anim.SetBool("Run", move);
        if (navMeshAgent.remainingDistance <= 1)
        {
            move = false;
        }
        else
        {
            move = true;
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
    }
}

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


    void Start ()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    void Update ()
    {
        CheckRaycast();
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
                Pick(hit);
            }

        }

    }


    void Pick(RaycastHit hit)
    {

        
    }

    void MoveToPoint(RaycastHit hit)
    {

        navMeshAgent.SetDestination(hit.point);
        navMeshAgent.speed = 15f;
    }

    private void OnCollisionEnter(Collision collision)
    {

        navMeshAgent.velocity = Vector3.zero;

        navMeshAgent.ResetPath();

    }
}

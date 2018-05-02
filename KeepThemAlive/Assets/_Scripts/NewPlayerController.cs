using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewPlayerController : MonoBehaviour {
    
    public Camera mainCamera;
    public LayerMask GroundLayer;
    public LayerMask ObjLayer;
    NavMeshAgent navMeshAgent;
    Vector3 LookPos;
    Vector3 OldPos;
    bool isPickedObj = false;
    Transform ObjectClicked;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Player a olhar para o rato!
        //PlayerLookTo();

        if (Input.GetMouseButtonDown(1))
        {
            MoveToPoint();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (!isPickedObj && OldPos == transform.position)
                PickUpBoxes();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPickedObj)
                LeaveBox();
        }

        OldPos = transform.position;
    }

    void PickUpBoxes()
    {
        isPickedObj = true;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, ObjLayer))
        {
            hit.collider.transform.SetParent(transform.GetChild(1));
            
            transform.GetChild(1).GetChild(0).position = transform.GetChild(1).position;
        }
    }
    void LeaveBox()
    {
        Debug.Log("Largar!");

        Transform aux = transform.GetChild(1).GetChild(0);

        transform.GetChild(1).GetChild(0).SetParent(null);

       
        aux.gameObject.AddComponent<Rigidbody>();
        aux.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        

        isPickedObj = false;
    }

    void MoveToPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, ObjLayer))
        {
            // se o raycast passar um obj > StopDistance
            navMeshAgent.stoppingDistance = 10;
            ObjectClicked = hit.transform;
        }
        else
        {
            // se o raycast nao atravessar obj stopDistance = default
            navMeshAgent.stoppingDistance = 0.1f;
        }

        if (Physics.Raycast(ray, out hit, 100, GroundLayer))
        {
            // se o raycast tocar no chao anda...
            navMeshAgent.SetDestination(hit.point);
            navMeshAgent.speed = 15f;
        }  
    }

    void PlayerLookTo()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            LookPos = hit.point;
        }
        LookPos.y = 7.4f;
        transform.LookAt(LookPos, Vector3.up);
    }

}

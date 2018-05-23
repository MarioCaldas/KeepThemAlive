using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;
    NavMeshAgent navMeshAgent;
    public InteractableObjManager activeObject;
    string boxTag = "Boxes";

    bool canMove;
    bool IsGrabed;
    Rigidbody rb;
    public Camera mainCamera;
    public GameObject parentTarget;

    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();

        canMove = true;
        IsGrabed = false;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //PushBoxes();
                //SurvivalFollow();
                //RemoveParent();
            }

            PointClickMovement();
        }
    }

    void PointClickMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, layerMask))
            {
                MoveToPoint(hit.point);
                RemoveActiveObject();
            }                
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                InteractableObjManager interactableObject = hit.collider.GetComponent<InteractableObjManager>();
                MoveToPoint(hit.point);
                RemoveActiveObject();

                if (interactableObject != null)
                {
                    SetActiveObject(interactableObject);
                    //PushBoxes(interactableObject);
                    //GiveOrder(interactableObject);
                }
            }
        }
    }

    void MoveToPoint(Vector3 destinationPoint)
    {
        navMeshAgent.SetDestination(destinationPoint);
        //navMeshAgent.stoppingDistance = 3f;
        navMeshAgent.speed = 15f;
    }

    void SetActiveObject(InteractableObjManager newActiveObject)
    {
        if(newActiveObject != activeObject)
        {
            if (activeObject != null)
                activeObject.DeactivateObject();

            activeObject = newActiveObject;

            //MoveToPoint(newActiveObject.transform.position);
            //navMeshAgent.stoppingDistance = newActiveObject.radius * 0.5f;
        }

        newActiveObject.SetActive(transform);   
    }

    void RemoveActiveObject()
    {
        if (activeObject != null)
            activeObject.DeactivateObject();

        activeObject = null;
        //navMeshAgent.stoppingDistance = 0f;
    }

    void GiveOrder(InteractableObjManager interactableObj)
    {
        Debug.Log("Move there,  " + interactableObj.name);
    }

    void SurvivalFollow()
    {
        GameObject[] npcs = InteractableObjManager.NPCs;
        GameObject activeNpc = null;

        float distance;

        foreach (GameObject NPC in npcs)
        {
            distance = Vector3.Distance(transform.position, NPC.transform.position);

            if (distance < 1.5f)
            {
                activeNpc = NPC;               
                //activeNpc.GetComponent<NpcController>().canFollow = true;
                Debug.Log("Npc to follow: " + activeNpc.name);
            }
            else
                distance = 0.0f;
        }
    }

    void PushBoxes(InteractableObjManager interactableObj)
    {
        float speed = 2f;
        float smoothTurn = speed * Time.deltaTime;

        SetActiveObject(interactableObj);
        GameObject activeBox = interactableObj.gameObject;

        if (!IsGrabed)
        {
            Vector3 targetDirection = activeBox.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, smoothTurn, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);

            //activeBox.GetComponent<Rigidbody>().isKinematic = true;
            activeBox.transform.parent = this.transform;
            IsGrabed = true;
            Debug.Log("tou aqui");
        }

        else
        {
            //activeBox.GetComponent<Rigidbody>().isKinematic = false;

            //IsGrabed = false;
            Debug.Log("Is grabed = true, seu ffffffilho da puta");
        }

        RemoveActiveObject();
    }

    void RemoveParent()
    {
        transform.GetChild(1).SetParent(parentTarget.transform);
        IsGrabed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        InteractableObjManager interactableObject = collision.collider.GetComponent<InteractableObjManager>();

        if (collision.gameObject.tag == "Boxes")
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            PushBoxes(interactableObject);
        }
    }
}

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
            PointClickMovement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PushBoxes();
            //SurvivalFollow();
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

        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                InteractableObjManager interactableObject = hit.collider.GetComponent<InteractableObjManager>();
                
                if (interactableObject != null)
                {
                    SetActiveObject(interactableObject);
                    PushBoxes();
                }
            }
        }
    }

    void MoveToPoint(Vector3 destinationPoint)
    {
        navMeshAgent.SetDestination(destinationPoint);
    }

    void SetActiveObject(InteractableObjManager newActiveObject)
    {
        if(newActiveObject != activeObject)
        {
            if (activeObject != null)
                activeObject.DeactivateObject();

            activeObject = newActiveObject;

            MoveToPoint(newActiveObject.transform.position);
            navMeshAgent.stoppingDistance = newActiveObject.radius * 0.8f;
        }
        Debug.Log("toyy");

        newActiveObject.SetActive(transform);   
    }

    void RemoveActiveObject()
    {
        if (activeObject != null)
            activeObject.DeactivateObject();

        activeObject = null;
        navMeshAgent.stoppingDistance = 0f;
    }

    void SurvivalFollow()
    {
        GameObject[] npcs = InteractableObjManager.NPCs;
        GameObject NewNPC = null;
        float distance;

        foreach (GameObject NPC in npcs)
        {
            distance = Vector3.Distance(transform.position, NPC.transform.position);

            if (distance < 1.5f)
            {
                NewNPC = NPC;
                Debug.Log("Este é o idiota: " + NewNPC.name);
                break;
            }
            else
                distance = 0.0f;
        }
    }

    void PushBoxes()
    {
        float distance;
        GameObject BoxGO = null;
        GameObject[] box = InteractableObjManager.Boxes;
        
        if (!IsGrabed)
        {
            // vê se tem alguma caixa perto
            foreach (GameObject caixa in box)
            {
                distance = Vector3.Distance(transform.position, caixa.transform.position);

                if (distance < 2.5f)
                {
                    BoxGO = caixa;
                    IsGrabed = true;
                    break;
                }
                else
                    distance = 0.0f;
                
            }

            //if (BoxGO != null)
                //BoxGO.transform.parent = this.transform;
        }
        else
        {
            //transform.GetChild(0).parent = null;
            IsGrabed = false;
        }

    }
}

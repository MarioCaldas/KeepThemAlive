using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public LayerMask layerMask;
    private NavMeshAgent navMeshAgent;
    string boxTag = "Boxes";
    InteractableObjManager activeObject;

    private Vector3 moveVelocity;
    private Vector3 moveInput;
    private float forwardAmount;
    public float turnAmount;
    public float moveSpeed;
    bool canMove;
    bool IsGrabed;
    Rigidbody rb;
    public Camera mainCamera;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();

        canMove = true;
        IsGrabed = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            //rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
            //MovementTopDown();
            //ConvertMoveInput();
            PointClickMovement();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PushBoxes();
            SurvivalFollow();
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
                Debug.Log("Hit: " + hit.transform.name);
                MoveToPoint(hit.point);
            }                
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                Debug.Log("Hit: " + hit.transform.name);

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
        activeObject = newActiveObject;
        MoveToPoint(newActiveObject.transform.position);
        navMeshAgent.stoppingDistance = 2f;
    }

    void RemoveActiveObject()
    {
        activeObject = null;
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

            if (BoxGO != null)
                BoxGO.transform.parent = this.transform;
        }
        else
        {
            transform.GetChild(0).parent = null;
            IsGrabed = false;
        }

    }

    void ConvertMoveInput()
    {
        Vector3 localMove = transform.InverseTransformDirection(moveInput);
        turnAmount = localMove.x;
        forwardAmount = localMove.z;
    }

    void MovementTopDown()
    {
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            moveInput = transform.TransformDirection(moveInput);

            moveVelocity = moveInput * moveSpeed;

            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);

            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLenght;

            if (groundPlane.Raycast(cameraRay, out rayLenght))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
                Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }
}

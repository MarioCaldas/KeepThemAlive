using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
    void Start ()
    {
        canMove = true;
        IsGrabed = false;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canMove)
        {
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
            MovementTopDown();
            ConvertMoveInput();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PushBoxes();
            SurvivalFollow();
        }
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

                if (distance < 1.5f)
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

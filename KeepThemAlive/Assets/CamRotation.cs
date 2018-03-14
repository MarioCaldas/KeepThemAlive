using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour {

    private GameObject cam;

    public Transform target;

    public GameObject targetObject;
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;


    void Start () {

        cam = this.gameObject;

        cam.transform.position = new Vector3(255, 75, 417);
        cam.transform.rotation = Quaternion.Euler(0, -180, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle -= 90.0f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle += 90.0f;
        }

        if (targetAngle != 0)
        {
            Rotate();
        }
    }

    protected void Rotate()
    {

        if (targetAngle > 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
}

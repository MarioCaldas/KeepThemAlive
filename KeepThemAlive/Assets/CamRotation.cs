using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour {

    private GameObject cam;

    public Transform target;

    public GameObject targetObject;
    private float targetAngleX = 0;
    private float targetAngleY = 0;

    const float rotationAmount = 1.5f;
    const float rotationAmountY = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    bool isRotatingX = false;

    bool isRotatingY = false;




    void Start () {

        cam = this.gameObject;

        cam.transform.position = new Vector3(255, 75, 417);
        cam.transform.rotation = Quaternion.Euler(0, -180, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotatingX && !isRotatingY)
        {
            Debug.Log("ESQUERDA");
          
            if (transform.forward != -Vector3.up)
            {
                targetAngleX += 91.5f;
                isRotatingX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotatingX && !isRotatingY)
        {
            Debug.Log("DIREITA");

            if (transform.forward != -Vector3.up)
            {
                targetAngleX -= 91.5f;
                isRotatingX = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isRotatingY && !isRotatingX)
        {
            Debug.Log("UP");

            if (transform.forward != -Vector3.up)
            {
                targetAngleY -= 91.5f;
                isRotatingY = true;
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotatingY && !isRotatingX)
        {
            Debug.Log("DOWN");

            if (transform.forward == -Vector3.up)
            {
                targetAngleY += 91.5f;
                isRotatingY = true;
            }
        }

        if(targetAngleX == -1.5f)
        {
            isRotatingX = false;
            targetAngleX = 0;
        }
        if (targetAngleX == 1.5f)
        {
            isRotatingX = false;
            targetAngleX = 0;

        }

        if (targetAngleY == 1.5f)
        {
            isRotatingY = false;
            targetAngleY = 0;
        }
        if (targetAngleY == -1.5f)
        {

            isRotatingY = false;
            targetAngleY = 0;
        }


        if (targetAngleX != 0)
        {
            Debug.Log(isRotatingX);

            if (isRotatingX)
            {
                Rotate();
            }
        }


        if (targetAngleY != 0)
        {
            if (isRotatingY)
            {
                Rotate();
            }
        }
    }

    protected void Rotate()
    {
        if (targetAngleX > 0)
        {
            transform.RotateAround(targetObject.transform.position, transform.up, rotationAmount);
            targetAngleX -= rotationAmount;
            

        }
        else if (targetAngleX < 0)
        {
            transform.RotateAround(targetObject.transform.position, transform.up, -rotationAmount);
            targetAngleX += rotationAmount;
        }

        else if (targetAngleY < 0)
        {

            transform.RotateAround(targetObject.transform.position, transform.right, rotationAmountY);
            targetAngleY += rotationAmountY;
        }
        else if(targetAngleY >= 0)
        {
            Debug.Log("boi");

            transform.RotateAround(targetObject.transform.position, transform.right, -rotationAmountY);
            targetAngleY -= rotationAmountY;

        }
    }
}

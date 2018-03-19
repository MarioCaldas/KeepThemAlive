using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour {

    private GameObject cam;

    public Transform target;

    public GameObject targetObject;
    private float targetAngleX = 0;
    public float targetAngleY = 0;
    private float Zooming = 0;

    const float rotationAmount = 1.5f;
    const float rotationAmountY = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    bool isRotatingX = false;
    public bool isRotatingY = false;

    int ZoomMode = 0;
    public int UpDownMode = 0;

   


    void Start () {

        cam = this.gameObject;

        cam.transform.position = new Vector3(255, 33, 417);
        cam.transform.rotation = Quaternion.Euler(0, -180, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region INPUTS
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

                if (ZoomMode == 0)
                {
                    if (UpDownMode == 0)
                    {
                        targetAngleY -= 91.5f;
                        isRotatingY = true;
                        UpDownMode++;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotatingY && !isRotatingX)
            {
                Debug.Log("DOWN");

                if (ZoomMode == 0)
                {
                    if (UpDownMode == 1)
                    {
                        targetAngleY += 91.5f;
                        isRotatingY = true;
                        UpDownMode--;
                    }
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (ZoomMode == 0 && UpDownMode == 0)
                {
                    Zooming = 100;
                    ZoomMode++;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (ZoomMode == 1)
                {
                    Zooming = -100;
                    ZoomMode--;
                }
            }
        #endregion

        if (targetAngleX == -1.5f || targetAngleX == 1.5f)
        {
            isRotatingX = false;
            targetAngleX = 0;
        }

        if (targetAngleY == 1.5f || targetAngleY == -1.5f)
        {
            isRotatingY = false;
            targetAngleY = 0;
        }

        if (Zooming != 0)
        {
            Zoom();
        }

        if (targetAngleX != 0 || targetAngleY != 0)
        {
            if (isRotatingX)
            {
                Rotate();
            }
            else if (isRotatingY)
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
            transform.RotateAround(targetObject.transform.position, transform.right, -rotationAmountY);
            targetAngleY -= rotationAmountY;
        }
    }

    protected void Zoom()
    {
        if (Zooming > 0)
        {
            // ZOOM IN
            transform.position += transform.forward * 2;
            Zooming -= 2f;
        }
        else if (Zooming < 0)
        {
            // ZOOM OUT
            transform.position -= transform.forward * 2;
            Zooming += 2f;
        }
    }
}

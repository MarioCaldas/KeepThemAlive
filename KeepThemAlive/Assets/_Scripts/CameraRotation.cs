using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private float targetAngleX = 0;
    private float targetAngleY = 0;
    bool isRotatingX = false;
    bool isRotatingY = false;
    const float rotationAmountX = 1.5f;
    const float rotationAmountY = 1.5f;
    private int UpDownMode = 0;
    Transform cam;
    public Transform School;

    private float Zooming = 0;
    int ZoomMode = 0;


    void Start ()
    {

    }
	
	void Update ()
    {
        #region Inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotatingX && !isRotatingY)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("ESQUERDA");

                setTargetAngleX(91.5f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotatingX && !isRotatingY)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("DIREITA");

                setTargetAngleX(-91.5f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isRotatingY && !isRotatingX)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("UP");

                setTargetAngleY(-91.5f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotatingY && !isRotatingX)
        {
            if (UpDownMode == 1 && ZoomMode != 2)
            {
                Debug.Log("DOWN");

                setTargetAngleY(91.5f);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (ZoomMode == 0)
            {
                Zooming = 80;
                ZoomMode++;
            }
            else if (ZoomMode == 1 && UpDownMode == 1 && Zooming == 0)
            {
                Zooming = 80;
                ZoomMode++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            
            if (ZoomMode == 2 && UpDownMode == 1)
            {
                Zooming = -80;
                ZoomMode--;
            }
            else if (ZoomMode == 1 && Zooming == 0)
            {
                Zooming = -80;
                ZoomMode--;
            }
        }

        #endregion
        Debug.Log("Zoooom: " + ZoomMode);

        if (targetAngleX == -rotationAmountX || targetAngleX == rotationAmountX)
        {
            isRotatingX = false;
            targetAngleX = 0;
        }

        if (targetAngleY == -rotationAmountY || targetAngleY == rotationAmountY)
        {
            isRotatingY = false;
            targetAngleY = 0;
        }

        if (targetAngleX != 0 || targetAngleY != 0)
        {
            if (isRotatingX)
            {
                Rotate(School.transform);
            }
            else if (isRotatingY)
            {
                Rotate(School.transform);
            }
        }

        if (Zooming != 0)
        {
            Zoom();
        }
    }

    protected void Rotate(Transform target)
    {
        // CHAMADA PARA FAZER UMA ROTAÇÃO QUE FOI PREVIAMENTE DADO O VALOR
        if (targetAngleX > 0)
        {
            transform.RotateAround(target.transform.position, transform.up, rotationAmountX);
            targetAngleX -= rotationAmountX;
        }
        else if (targetAngleX < 0)
        {
            transform.RotateAround(target.transform.position, transform.up, -rotationAmountX);
            targetAngleX += rotationAmountX;
        }
        else if (targetAngleY < 0)
        {
            transform.RotateAround(target.transform.position, transform.right, rotationAmountY);
            targetAngleY += rotationAmountY;
        }
        else if (targetAngleY >= 0)
        {
            transform.RotateAround(target.transform.position, transform.right, -rotationAmountY);
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

    #region SetValeusAngles
    public void setTargetAngleY(float valeu)
    {
        if (valeu < 0 && UpDownMode == 0)
        {
            UpDownMode++;
            targetAngleY = valeu;
            isRotatingY = true;
        }
        else if(valeu > 0 && UpDownMode == 1 && Zooming != 2)
        {
            UpDownMode--;
            targetAngleY = valeu;
            isRotatingY = true;
        }
    }
       
    public void setTargetAngleX(float valeu)
    {
        targetAngleX = valeu;
        isRotatingX = true;
    }

    public void SetUpDownMode(int valeu)
    {
        UpDownMode = valeu;
    }
    #endregion
}

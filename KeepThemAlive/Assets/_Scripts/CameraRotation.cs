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


    void Start ()
    {

    }
	
	void Update ()
    {
        #region INPUTS
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
            if (UpDownMode == 1)
            {
                Debug.Log("DOWN");

                setTargetAngleY(91.5f);
            }
        }

        #endregion

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

    #region SetValeusAngles
    public void setTargetAngleY(float valeu)
    {
        if (valeu < 0)
            UpDownMode++;
        else
            UpDownMode--;

        targetAngleY = valeu;
        isRotatingY = true;
    }
    public void setTargetAngleX(float valeu)
    {
        targetAngleX = valeu;
        isRotatingX = true;
    }
    #endregion
}

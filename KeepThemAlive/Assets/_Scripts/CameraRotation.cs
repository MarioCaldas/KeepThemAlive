using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private float targetAngleX = 0;
    private float targetAngleY = 0;
    private float Zooming = 0;
    bool isRotatingX = false;
    bool isRotatingY = false;
    bool CenterCam = false;
    const float rotationAmountX = 1.5f;
    const float rotationAmountY = 1.5f;
    const float panBorderSpace = 20f;
    const float panSpeed = 35f;
    private int UpDownMode = 0;
    private int ZoomMode = 0;
    private Vector3 CamCentered;
    
    public Transform School;
    
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
                CamCentered = transform.position;
                CamCentered.y = 62.53441f;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Aqui têm de checar se a camera esta centrada se nao recentra
            
            if (ZoomMode == 2 && UpDownMode == 1 && transform.position == CamCentered)
            {
                Zooming = -80;
                ZoomMode--;
            }
            else if (ZoomMode == 2 && UpDownMode == 1 && transform.position != CamCentered)
            {
                // se nao tiver centrada!!!
                CenterCam = true;
            }
            else if (ZoomMode == 1 && Zooming == 0)
            {
                Zooming = -80;
                ZoomMode--;
            }
            
        }
        if (ZoomMode == 2)
        {
            if (Input.mousePosition.y >= Screen.height - panBorderSpace)
            {
                transform.position += this.transform.up * Time.deltaTime * panSpeed;
            }
            else if (Input.mousePosition.y <= panBorderSpace)
            {
                transform.position -= this.transform.up * Time.deltaTime * panSpeed;
            }
            else if (Input.mousePosition.x <= panBorderSpace)
            {
                transform.position -= this.transform.right * Time.deltaTime * panSpeed;
            }
            else if (Input.mousePosition.x >= Screen.width - panBorderSpace)
            {
                transform.position += this.transform.right * Time.deltaTime * panSpeed;
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

        if (Zooming != 0)
        {
            Zoom();
        }

        if (CenterCam)
        {
            Center();
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

    protected void Center()
    {
        if (transform.position != CamCentered)
        {
            transform.position = Vector3.MoveTowards(transform.position, CamCentered, 0.5f);
        }
        else
        {
            CenterCam = false;
            Zooming = -80;
            ZoomMode--;
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

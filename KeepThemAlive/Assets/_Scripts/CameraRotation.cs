using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {
    
    bool isRotatingX = false;
    bool isRotatingY = false;
    bool CoroutineOn = false;
    public bool IntView = false;
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
        CamCentered = new Vector3(252.4f, 58.53452f, 240.1655f);
    }
	
	void Update ()
    {
        #region Inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotatingX && !isRotatingY && !CoroutineOn)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("ESQUERDA");
                StartCoroutine(Left());
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotatingX && !isRotatingY && !CoroutineOn)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("DIREITA");
                StartCoroutine(Right());
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !isRotatingY && !isRotatingX && !CoroutineOn)
        {
            if (UpDownMode == 0)
            {
                Debug.Log("UP");
                StartCoroutine(Up());
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotatingY && !isRotatingX && !CoroutineOn)
        {
            if (UpDownMode == 1 && ZoomMode != 2)
            {
                Debug.Log("DOWN");
                StartCoroutine(Down());
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (ZoomMode == 0 && !CoroutineOn)
            {
                StartCoroutine(ZoomIn());
            }
            else if (ZoomMode == 1 && UpDownMode == 1 && !CoroutineOn && IntView)
            {
                StartCoroutine(ZoomIn());
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            // Aqui têm de checar se a camera esta centrada se nao recentra
            if (ZoomMode == 2 && UpDownMode == 1 && transform.position == CamCentered && !CoroutineOn)
            {
                StartCoroutine(ZoomOut());
                //ZoomMode--;
            }
            else if (ZoomMode == 2 && UpDownMode == 1 && transform.position != CamCentered)
            {
                // se nao tiver centrada!!!
                StartCoroutine(Center());
            }
            else if (ZoomMode == 1)
            {
                StartCoroutine(ZoomOut());
                //ZoomMode--;
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
    }

    #region Coroutines
    private IEnumerator Left()
    {
        CoroutineOn = true;
        for (float f = 0; f < 90; f += 1.5f)
        {
            transform.RotateAround(School.transform.position, transform.up, rotationAmountX);
            yield return null;
            CoroutineOn = false;
        }
    }

    private IEnumerator Right()
    {
        CoroutineOn = true;
        for (float f = 0; f < 90; f += 1.5f)
        {
            transform.RotateAround(School.transform.position, transform.up, -rotationAmountX);
            yield return null;
            CoroutineOn = false;
        }
    }

    public IEnumerator Up()
    {
        if (UpDownMode == 0)
        {
            CoroutineOn = true;
            for (float f = 0; f < 90; f += 1.5f)
            {
                transform.RotateAround(School.transform.position, transform.right, rotationAmountX);
                UpDownMode = 1;
                yield return null;
                CoroutineOn = false;
            }
        }
    }

    private IEnumerator Down()
    {
        CoroutineOn = true;
        for (float f = 0; f < 90; f += 1.5f)
        {
            transform.RotateAround(School.transform.position, transform.right, -rotationAmountX);
            yield return null;
            UpDownMode = 0;
            CoroutineOn = false;
        }
    }

    private IEnumerator ZoomIn()
    {
        if (UpDownMode == 0 && ZoomMode == 0)
        {
            CoroutineOn = true;
            ZoomMode++;
            for (float f = 0; f < 82f; f += 2)
            {
                transform.position += transform.forward * 2;
                yield return null;
                CoroutineOn = false;
            }
        }
        else if (UpDownMode == 1 && ZoomMode < 2)
        {
            CoroutineOn = true;
            ZoomMode++;
            for (float f = 0; f < 82f; f += 2)
            {
                transform.position += transform.forward * 2;
                yield return null;

                CoroutineOn = false;
            }
        }
    }

    public IEnumerator ZoomOut()
    {
        if (ZoomMode > 0)
        {
            CoroutineOn = true;
            ZoomMode--;
            for (float f = 80; f >= 0; f -= 2)
            {
                transform.position -= transform.forward * 2;
                yield return null;
                CoroutineOn = false;
            }
        }
    }

    private IEnumerator Center()
    {
        while(transform.position != CamCentered)
        {
            transform.position = Vector3.MoveTowards(transform.position, CamCentered, 0.02f);
        }
        yield return null;
        StartCoroutine(ZoomOut());
    }
    #endregion

    #region SetValeusAngles
    public void SetUpDownMode(int valeu)
    {
        UpDownMode = valeu;
    }

    public void SetZoomMode(int valeu)
    {
        ZoomMode = valeu;
    }
    #endregion
}

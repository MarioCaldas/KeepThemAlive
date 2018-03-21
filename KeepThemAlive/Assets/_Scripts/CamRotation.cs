using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour {

    private GameObject cam;

    public Transform target;

    public GameObject targetObject, roomTarget1, roomTarget2, roomTarget3, roomTarget4;

    private float targetAngleX = 0;
    public float targetAngleY = 0;
 

    const float rotationAmount = 1.5f;
    const float rotationAmountY = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    bool isRotatingX = false;
    public bool isRotatingY = false;

    
    public int UpDownMode = 0;


    private float schoolPosInitX = 255;
    private float schoolposInitY = 27;
    private float schoolposInitZ = 417;

    private float schoolrotInitX = 0;
    private float schoolrotInitY = -180;
    private float schoolrotInitZ = 0;

    ChangeObjects changeObjScript;

    public GameObject gameManagerObj;


    private float freeRotationX = 0.0f;
    private float freeRotationY = 0.0f;
    public float cameraSensitivity = 90;


    public float panSpeed = 35f;
    public float panBorderSpace = 20f;
    public Vector2 panLimit;

    public float scrollSpeed = 20f;

    public bool isOnTop = false;

    public bool front, left, back, right;

    void Start () {

        front = false;
        left = false;
        back = false;
        right = false;

        cam = this.gameObject;

        cam.transform.position = new Vector3(schoolPosInitX, schoolposInitY, schoolposInitZ);
        cam.transform.rotation = Quaternion.Euler(schoolrotInitX, schoolrotInitY, schoolrotInitZ);

        changeObjScript = gameManagerObj.GetComponent<ChangeObjects>();
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

             
                if (UpDownMode == 0)
                {
                    targetAngleY -= 91.5f;
                    isRotatingY = true;
                    UpDownMode++;


                }
                
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !isRotatingY && !isRotatingX)
            {
                Debug.Log("DOWN");

                
                if (UpDownMode == 1)
                {
                    targetAngleY += 91.5f;
                    isRotatingY = true;
                    UpDownMode--;
                }
                
            }

        #endregion


        Debug.Log(isOnTop);

        #region ZOOM

        Vector3 pos = transform.position;
        if (isOnTop)
        {
            if (pos.y < 136)
            {
                if (Input.mousePosition.y >= Screen.height - panBorderSpace)
                {
                    pos.z -= panSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.y <= panBorderSpace)
                {
                    pos.z += panSpeed * Time.deltaTime;
                }

                if (Input.mousePosition.x <= panBorderSpace)
                {
                    pos.x += panSpeed * Time.deltaTime;
                }


                if (Input.mousePosition.x >= Screen.width - panBorderSpace)
                {
                    pos.x -= panSpeed * Time.deltaTime;
                }
            }
            else
            {
                pos = Vector3.Lerp(transform.position, new Vector3(255, transform.position.y, transform.position.z), 0.02f);
                pos = Vector3.Lerp(transform.position, new Vector3(255, transform.position.y, 231), 0.02f);

            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            pos.y -= scroll * scrollSpeed * 100 * Time.deltaTime;

            //limites

            //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            //pos.y = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

            transform.position = pos;

        }
        #endregion

        camYpos(pos);


        if (targetAngleX == -1.5f || targetAngleX == 1.5f)
        {
            isRotatingX = false;
            targetAngleX = 0;
        }

        if (targetAngleY == 1.5f || targetAngleY == -1.5f)
        {
            isOnTop = true;
            isRotatingY = false;
            targetAngleY = 0;
        }

        
        if (targetAngleX != 0 || targetAngleY != 0)
        {
            if (isRotatingX)
            {
                Rotate(targetObject.transform);
            }
            else if (isRotatingY)
            {
                Rotate(targetObject.transform);
            }
        }

        //Debug.Log("cam rot" + cam.transform.localEulerAngles.y);

    }

    private void RTScam(Vector3 pos)
    {
        //if(front)
        //{
        //    pos.z -= panSpeed * Time.deltaTime;
        //}
 


    }


    void camYpos(Vector3 pos)
    {
        if (cam.transform.eulerAngles.y >= 180 && cam.transform.eulerAngles.y <= 181)
        {
            front = true;
            left = false;
            back = false;
            right = false;


            Debug.Log("front: " + front);
        }

        else if (cam.transform.eulerAngles.y >= 90 && cam.transform.eulerAngles.y <= 91)
        {
            front = false;
            left = false;
            back = false;
            right = true;

            Debug.Log("right: " + right);


        }

        else if (cam.transform.eulerAngles.y >= 0 && cam.transform.eulerAngles.y <= 1)
        {
            front = false;
            left = false;
            back = true;
            right = false;


            Debug.Log("back: " + back);

        }

        else if (cam.transform.eulerAngles.y >= 270 && cam.transform.eulerAngles.y <= 271)
        {
            front = false;
            left = true;
            back = false;
            right = false;

            Debug.Log("left: " + left);


        }


    }


    protected void Rotate(Transform target)
    {
        if (targetAngleX > 0)
        {
            transform.RotateAround(target.transform.position, transform.up, rotationAmount);
            targetAngleX -= rotationAmount;
        }
        else if (targetAngleX < 0)
        {
            transform.RotateAround(target.transform.position, transform.up, -rotationAmount);
            targetAngleX += rotationAmount;
        }
        else if (targetAngleY < 0)
        {
            transform.RotateAround(target.transform.position, transform.right, rotationAmountY);
            targetAngleY += rotationAmountY;
        }
        else if(targetAngleY >= 0)
        {
            transform.RotateAround(target.transform.position, transform.right, -rotationAmountY);
            targetAngleY -= rotationAmountY;
        }
    }

    

}

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

    public static bool isOnTop = false;

    private float camYinitPos;

    private bool diffY = false;

    private Vector3 firstCamPos;

    public Vector3 pos;

    public Quaternion originalRotationValue;

    private bool mouseWhellUp = false;

    void Start () {

        cam = this.gameObject;

        cam.transform.position = new Vector3(schoolPosInitX, schoolposInitY, schoolposInitZ);
        cam.transform.rotation = Quaternion.Euler(schoolrotInitX, schoolrotInitY, schoolrotInitZ);

        changeObjScript = gameManagerObj.GetComponent<ChangeObjects>();

        camYinitPos = 210f;

        firstCamPos = transform.position;

        originalRotationValue = transform.rotation;

    }
	
	void Update ()
    {
        #region INPUTS
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotatingX && !isRotatingY)
        {
                //if (transform.forward != -Vector3.up)
                //{
                    Debug.Log("ESQUERDA");

                    targetAngleX += 91.5f;
                    isRotatingX = true;
                //}
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotatingX && !isRotatingY)
        {
            //if (transform.forward != -Vector3.up)
            //{
                Debug.Log("DIREITA");

                targetAngleX -= 91.5f;
                isRotatingX = true;
            //}
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

        #region ZOOM

        pos = transform.position;
        if (isOnTop)
        {
            if (pos.y < 136)
            {
                //diffY = true;

                if (Input.mousePosition.y >= Screen.height - panBorderSpace)
                {

                    pos += this.transform.up * Time.deltaTime * panSpeed;
                }

                if (Input.mousePosition.y <= panBorderSpace)
                {
                    pos -= this.transform.up * Time.deltaTime * panSpeed;

                }

                if (Input.mousePosition.x <= panBorderSpace)
                {
                    pos -= this.transform.right * Time.deltaTime * panSpeed;

                }

                if (Input.mousePosition.x >= Screen.width - panBorderSpace)
                {

                    pos += this.transform.right * Time.deltaTime * panSpeed;

                }
            }
            else if(isOnTop)
            {
                
                
                pos = Vector3.Lerp(transform.position, new Vector3(255, transform.position.y, transform.position.z), 0.02f);
                pos = Vector3.Lerp(transform.position, new Vector3(255, transform.position.y, 231), 0.02f);

                //cam.y = 210
            }

            Debug.Log(isOnTop);
            

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            pos.y -= scroll * scrollSpeed * 100 * Time.deltaTime;



            if(scroll < 0)
            {
                mouseWhellUp = true;
            }
            else
            {
                mouseWhellUp = false;
            }
            //limites

            //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
            //pos.y = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

            //transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);
            transform.position = pos;
        }

        #endregion

        //Debug.Log("isRotatingY " + isRotatingY);
        //Debug.Log("isRotatingX " + isRotatingX);
        
        if (targetAngleX == -1.5f || targetAngleX == 1.5f)
        {
            isRotatingX = false;
            targetAngleX = 0;
        }

        if (targetAngleY == 1.5f || targetAngleY == -1.5f)
        {
            diffY = true;
            isOnTop = true;

            isRotatingY = false;
            targetAngleY = 0;
        }

        if(firstCamPos != new Vector3(transform.position.x, transform.position.y, transform.position.z))
        {
            diffY = false;
        }


        Debug.Log("isRotatingX: " + isRotatingX);


        if (targetAngleX != 0 || targetAngleY != 0)
        {

            //isOnTop = false;
            if (isRotatingX)
            {
                Debug.Log("ola");
                Rotate(targetObject.transform);

            }
            else if (isRotatingY)
            {

                Rotate(targetObject.transform);

            }
        }


        //if(isOnTop && ChangeView.extButtonPressed)
        //{
        //    isRotatingY = false;
        //    transform.position = firstCamPos;
        //    transform.rotation = originalRotationValue;
        //}


    }

    protected void Rotate(Transform target)
    {
        if (targetAngleX > 0)
        {
            Debug.Log("1");
            transform.RotateAround(target.transform.position, transform.up, rotationAmount);
            targetAngleX -= rotationAmount;
        }
        else if (targetAngleX < 0)
        {
            Debug.Log("2");

            transform.RotateAround(target.transform.position, transform.up, -rotationAmount);
            targetAngleX += rotationAmount;
        }
        else if (targetAngleY < 0)
        {
            Debug.Log("3");

            transform.RotateAround(target.transform.position, transform.right, rotationAmountY);
            targetAngleY += rotationAmountY;
        }
        //button ext pressed
        else if (targetAngleY >= 0)
        {

            Debug.Log("4");

            //colocar camera no ponto Y original
            //transform.position = Vector3.Lerp(transform.position, new Vector3(firstCamPos.x, firstCamPos.y, firstCamPos.z), 0.02f);

            if (diffY)
            {
                transform.RotateAround(target.transform.position, transform.right, -rotationAmountY);
            }
            
            targetAngleY -= rotationAmountY;
        }
    }
}

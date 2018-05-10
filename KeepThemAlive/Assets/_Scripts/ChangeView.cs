using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChangeView : MonoBehaviour {
   
    public GameObject roof;
    public GameObject SchoolGO;
    public GameObject Cam;
    Vector3 ResetedCamPos;
    Quaternion ResetedCamRot;
    
    private CameraRotation CameraRotation;

    Color cor;

    void Start ()
    {

        ResetedCamPos = Cam.transform.position;
        ResetedCamRot = Cam.transform.rotation;
        
        cor = roof.GetComponent<Renderer>().material.color;
        
        CameraRotation = Cam.GetComponent<CameraRotation>();
    }

    public void ChangeScene()
    {
        //isto nao se faz assim
        //GameObject[] Doors = GameObject.FindGameObjectsWithTag("Door");


        //foreach (var door in Doors)
        //{
        //    //Debug.Log("Door: " + door.name + ", rotation: " + door.transform.rotation.y);
        //    door.transform.GetChild(0).rotation = Quaternion.Euler(0, 80, 0);
        //}
        //GameObject.Find("Door").transform.rotation = Quaternion.Euler(0, 120, 0);
        roof.SetActive(false);
        DontDestroyOnLoad(SchoolGO);
        Application.LoadLevel(1);
    }
	
    public void ChangeIntView()
    {
        //CameraRotation.setTargetAngleY(-91.5f);
        StartCoroutine(SetMaterialTransparent());
        StartCoroutine(CameraRotation.Up());
        CameraRotation.IntView = true;
    }

    public void ChangeExtView()
    {
        //CameraRotation.setTargetAngleY(91.5f);
        roof.SetActive(true);
        StartCoroutine(SetMaterialOpaque());
        StartCoroutine(CameraRotation.ZoomOut());
        CameraRotation.IntView = false;
    }


    public void ResetIntCamera()
    {
        Cam.transform.position = ResetedCamPos;
        Cam.transform.rotation = ResetedCamRot;
        CameraRotation.SetUpDownMode(0);
        CameraRotation.SetZoomMode(0);
        CameraRotation.IntView = false;
        roof.SetActive(true);
        StartCoroutine(SetMaterialOpaque());
    }

    void Update ()
    {
        
    }

    #region Transparecy
    IEnumerator SetMaterialTransparent()
    {
        for (float f = 1f; f >= -0.1f; f -= 0.1f)
        {
            if (f <= 0)
            {
                roof.SetActive(false);
                StopCoroutine(SetMaterialTransparent());
            }
            cor.a = f;
            roof.transform.GetComponent<Renderer>().material.color = cor;
            yield return null;
        }
    }
    
    IEnumerator SetMaterialOpaque()
    {
        // Desvanecer para aparecer
        for (float f = 0; f < 1.1f; f += 0.1f)
        {
            if (f >= 1)
            {
                StopCoroutine(SetMaterialOpaque());
            }
            cor.a = f;
            roof.transform.GetComponent<Renderer>().material.color = cor;
            yield return null;
        }
    }
    #endregion
}

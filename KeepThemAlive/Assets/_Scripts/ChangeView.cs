using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeView : MonoBehaviour {

    public Button buttonIntView;
    public Button buttonExtView;
    public Button resetViewButton;
    
    public GameObject roof;
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

        Button intButton = buttonIntView.GetComponent<Button>();
        Button extButton = buttonExtView.GetComponent<Button>();
        Button resetButton = resetViewButton.GetComponent<Button>();

        intButton.onClick.AddListener(ChangeIntView);
        extButton.onClick.AddListener(ChangeExtView);
        resetButton.onClick.AddListener(ResetIntCamera);

        CameraRotation = Cam.GetComponent<CameraRotation>();

    }
	
    void ChangeIntView()
    {
        //CameraRotation.setTargetAngleY(-91.5f);
        StartCoroutine(SetMaterialTransparent());
        StartCoroutine(CameraRotation.Up());
        CameraRotation.IntView = true;
    }

    void ChangeExtView()
    {
        //CameraRotation.setTargetAngleY(91.5f);
        roof.SetActive(true);
        StartCoroutine(SetMaterialOpaque());
        StartCoroutine(CameraRotation.ZoomOut());
        CameraRotation.IntView = false;
    }


    void ResetIntCamera()
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

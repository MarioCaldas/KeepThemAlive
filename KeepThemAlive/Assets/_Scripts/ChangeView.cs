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

    //private CamRotation camRotScript;
    private CameraRotation CameraRotation;

    bool TurnOffRoof;
    bool TurnOnRoof;

    Color cor;

    //public static bool intButtonPressed = false;

    //public static bool extButtonPressed = false;

    void Start ()
    {
        ResetedCamPos = Cam.transform.position;
        ResetedCamRot = Cam.transform.rotation;

        TurnOffRoof = false;
        TurnOnRoof = false;

        cor = roof.GetComponent<Renderer>().material.color;

        Button intButton = buttonIntView.GetComponent<Button>();
        Button extButton = buttonExtView.GetComponent<Button>();
        Button resetButton = resetViewButton.GetComponent<Button>();

        intButton.onClick.AddListener(ChangeIntView);
        extButton.onClick.AddListener(ChangeExtView);
        resetButton.onClick.AddListener(ResetIntCamera);

        //camRotScript = Cam.GetComponent<CamRotation>();
        CameraRotation = Cam.GetComponent<CameraRotation>();

    }
	
    void ChangeIntView()
    {
        // LIMPEZA, DASS -.-'
        CameraRotation.setTargetAngleY(-91.5f);
        TurnOffRoof = true;
        
        //roof.SetActive(false);
        
        //camRotScript.targetAngleY -= 91.5f;
        
        //SetMaterialTransparent();

        //camRotScript.Zooming = 70;

        //camRotScript.isRotatingY = true;

        //camRotScript.UpDownMode++;


        // PARA QUE ISTO???
        //intButtonPressed = true;

        //extButtonPressed = false;
    }

    void ChangeExtView()
    {
        CameraRotation.setTargetAngleY(91.5f);
        roof.SetActive(true);
        TurnOnRoof = true;

        //roof.SetActive(true);

        //camRotScript.targetAngleY += 91.5f;
        
        //camRotScript.Zooming = -70;

        //camRotScript.isRotatingY = true;

        //camRotScript.UpDownMode--;

        // nao devia ter um "intButtonPressed = false;" ?????????????
        //extButtonPressed = true;
    }


    void ResetIntCamera()
    {
        // Funciona!!!
        Cam.transform.position = ResetedCamPos;
        Cam.transform.rotation = ResetedCamRot;
        CameraRotation.SetUpDownMode(0);
        roof.SetActive(true);
        // Algo errado não está certo
        TurnOffRoof = false;
        TurnOnRoof = true;
    }

    void Update ()
    {
        if (TurnOffRoof)
        {
            SetMaterialTransparent();
        }
        else if (TurnOnRoof)
        {
            SetMaterialOpaque();
        }
    }

    #region Transparecy
    private void SetMaterialTransparent()
    {
        // Desvanecer para desaparecer

        if (cor.a <= 0)
        {
            TurnOffRoof = false;
            roof.SetActive(false);
        }
        else
        {
            cor.a -= 0.02f;
            roof.transform.GetComponent<Renderer>().material.color = cor;
        }
    }

    private void SetMaterialOpaque()
    {
        // Desvanecer para aparecer

        if (cor.a >= 1)
        {
            TurnOnRoof = false;
        }
        else
        {
            cor.a += 0.02f;
            roof.transform.GetComponent<Renderer>().material.color = cor;
        }
    }
    #endregion
}

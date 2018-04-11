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

    private CamRotation camRotScript;
    private CameraRotation CameraRotation;

    public static bool intButtonPressed = false;

    public static bool extButtonPressed = false;

	void Start ()
    {
        Button intButton = buttonIntView.GetComponent<Button>();
        Button extButton = buttonExtView.GetComponent<Button>();
        Button resetButton = resetViewButton.GetComponent<Button>();

        intButton.onClick.AddListener(ChangeIntView);
        extButton.onClick.AddListener(ChangeExtView);
        resetButton.onClick.AddListener(ResetIntCamera);

        camRotScript = Cam.GetComponent<CamRotation>();
        CameraRotation = Cam.GetComponent<CameraRotation>();

    }
	
    void ChangeIntView()
    {
        //roof.SetActive(false);

        // LIMPEZA, DASS -.-'

        //camRotScript.targetAngleY -= 91.5f;
        CameraRotation.setTargetAngleY(-91.5f);
        SetMaterialTransparent();

        //camRotScript.Zooming = 70;

        //camRotScript.isRotatingY = true;

        //camRotScript.UpDownMode++;


        // PARA QUE ISTO???
        intButtonPressed = true;

        extButtonPressed = false;
    }

    void ChangeExtView()
    {
        //roof.SetActive(true);

        //camRotScript.targetAngleY += 91.5f;
        CameraRotation.setTargetAngleY(91.5f);
        SetMaterialOpaque();
        //camRotScript.Zooming = -70;

        //camRotScript.isRotatingY = true;

        //camRotScript.UpDownMode--;

        // nao devia ter um "intButtonPressed = false;" ?????????????
        extButtonPressed = true;
    }


    void ResetIntCamera()
    {

    }

    void Update ()
    {

    }

    

    private void SetMaterialTransparent()
    {
        foreach (Material m in roof.GetComponent<Renderer>().materials)
        {
            // AQUI É SO FAZER UMA VARIAVEL DE SOMA DE X EM X TEMPO
            Color cor = roof.GetComponent<Renderer>().material.color;
            cor.a = 0;
            roof.transform.GetComponent<Renderer>().material.color = cor;
        }
    }

    private void SetMaterialOpaque()
    {

        foreach (Material m in roof.GetComponent<Renderer>().materials)
        {
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);

            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

            m.SetInt("_ZWrite", 1);

            m.DisableKeyword("_ALPHATEST_ON");

            m.DisableKeyword("_ALPHABLEND_ON");

            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

            m.renderQueue = -1;
        }
    }
 
}

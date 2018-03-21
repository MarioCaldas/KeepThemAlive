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


	void Start ()
    {
        Button intButton = buttonIntView.GetComponent<Button>();
        Button extButton = buttonExtView.GetComponent<Button>();
        Button resetButton = resetViewButton.GetComponent<Button>();

        intButton.onClick.AddListener(ChangeIntView);
        extButton.onClick.AddListener(ChangeExtView);
        resetButton.onClick.AddListener(ResetIntCamera);

        camRotScript = Cam.GetComponent<CamRotation>();

    }
	
    void ChangeIntView()
    {
        roof.SetActive(false);

        camRotScript.targetAngleY -= 91.5f;
        //camRotScript.Zooming = 70;
        camRotScript.isRotatingY = true;
        camRotScript.UpDownMode++;



    }

    void ChangeExtView()
    {
        roof.SetActive(true);

        camRotScript.targetAngleY += 91.5f;
        //camRotScript.Zooming = -70;
        camRotScript.isRotatingY = true;
        camRotScript.UpDownMode--;

    }

 
    void ResetIntCamera()
    {

    }

    void Update ()
    {

    }
}

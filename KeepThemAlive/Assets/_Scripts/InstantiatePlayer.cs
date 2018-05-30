using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePlayer : MonoBehaviour {

    public GameObject Player;

    public static bool playerInstantied;

    public GameObject camera;

    CameraFollow camScript;

    GameObject ambulanceSpot;

    GameObject roof;

    Color cor;

    public AudioListener camAudioListener;

	// Use this for initialization
	void Start () {

        roof = GameObject.Find("Roof");
        cor = roof.GetComponent<Renderer>().material.color;


        camScript = camera.GetComponent<CameraFollow>();

        ambulanceSpot = transform.GetChild(4).gameObject;

        playerInstantied = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(Player != null)
        {
            if (Vector3.Distance(Player.transform.position, roof.transform.position) < 85)
            {
                StartCoroutine(SetMaterialTransparent());

            }
        }
      
    }

    void Instantiate()
    {
        if(Player == null)
        {

           Player = Instantiate(Resources.Load("FireMan") as GameObject, new Vector3(296, 0, 352), Quaternion.identity);

            Destroy(camAudioListener);

        }

        ambulanceSpot.GetComponent<MeshRenderer>().enabled = true;

        playerInstantied = true;


    }

    IEnumerator SetMaterialTransparent()
    {
        for (float f = 1f; f >= -0.1f; f -= 0.05f)
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
}

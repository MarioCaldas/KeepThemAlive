using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirensRotation : MonoBehaviour {

    public GameObject redLight, blueLight;

    private Vector3 redTemp, blueTemp;

    int speed =150;



    void Update ()
    {
        redTemp.y += speed * Time.deltaTime;
        blueTemp.y -= speed * Time.deltaTime;
        redLight.transform.eulerAngles = redTemp;
        blueLight.transform.eulerAngles = blueTemp;

    }
}

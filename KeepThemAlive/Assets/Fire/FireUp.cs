using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUp : MonoBehaviour {

    float y;

    void Start () {

        y = -3f;
    }

    // Update is called once per frame
    void Update ()
    {
        y += 0.05f;

        if(y < 3)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);

        }
    }

 
}

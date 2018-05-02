using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlaneScript : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colidiu: " + collision.gameObject.name);
        Destroy(collision.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHurted : MonoBehaviour
{
    public GameObject MetaSpot;
    public Material Transparent;
    public Material GreenTransparent;
    public GameObject Canvas;

    //public float health = 70f;
    //float damageAmount = 5f;

    private void Update()
    {
        //Debug.Log("Hurted Health: " + health);
        //TakeDamageControl();
    }

    public void GoMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = GreenTransparent;
    }

    public void LeaveMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = Transparent;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "WeakSpot")
        {
            Debug.Log("1 pessoa salva");
            CanvasScript.PessSalvas++;
        }
    }

    /*void TakeDamageControl()
    {
        health -= damageAmount * Time.deltaTime;

        if (health <= 0)
        {
            Die();
        }
    }*/

    void Die()
    {
        Debug.Log("I died");
    }
}

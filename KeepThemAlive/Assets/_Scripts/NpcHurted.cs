using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHurted : MonoBehaviour
{
    public GameObject MetaSpot;
    public Material Transparent;
    public Material GreenTransparent;
    public GameObject Canvas;
    public Animator animator;

    public float health = 100f;

    public static bool carried;

    GameObject maca;

    //float damageAmount = 5f;

    private void Awake()
    {
        carried = false;

        animator = GetComponent<Animator>();

        health = 100f - ReplaceImpact.totalheathImpact;
        //Debug.Log("Total Health: " + health);

        maca = GameObject.Find("Stretcher");
    }

    private void Update()
    {
        //Debug.Log("Hurted Health: " + health);
        //TakeDamageControl();

        Carried();
        

    }

    public void GoMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = GreenTransparent;
    }

    public void LeaveMeta()
    {
        MetaSpot.GetComponent<Renderer>().material = Transparent;
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("1 pessoa salva");

        AmbulanceTravelTime.Travel();

        if (collision.name == "WeakSpot")
        {
            Debug.Log("1 pessoa salva");
            CanvasScript.PessSalvas++;
        }
    }

    void Carried()
    {
        if(carried)
        {
            transform.GetComponent<BoxCollider>().isTrigger = true;
            animator.SetBool("isGrabed", true);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -3.76f, transform.position.z);
            transform.GetComponent<BoxCollider>().isTrigger = false;
            animator.SetBool("isGrabed", false);

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

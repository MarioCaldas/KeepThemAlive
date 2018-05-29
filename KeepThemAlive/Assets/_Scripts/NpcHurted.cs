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

    private bool isOnSpot;

    private GameObject AmbuSpot;

    private GameObject bedPlayerPos;

    //float damageAmount = 5f;

    private void Awake()
    {
        AmbuSpot = GameObject.Find("WeakSpot");

        carried = false;
        isOnSpot = false;

        animator = GetComponent<Animator>();

        health = 100f - ReplaceImpact.totalheathImpact;
        //Debug.Log("Total Health: " + health);

        Debug.Log(AmbuSpot);

        maca = GameObject.Find("Stretcher");

        bedPlayerPos = maca.transform.GetChild(0).gameObject;
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

        if (collision.gameObject == AmbuSpot)
        { 
            isOnSpot = true;

        }
        else
        {
            //isOnSpot = false;
        }
    }

    void Carried()
    {
        if(carried)
        {
            //transform.GetComponent<BoxCollider>().isTrigger = true;
            //animator.SetBool("isGrabed", true);

            //if (Input.GetKeyDown(KeyCode.E))
            //{
                
            //}

        }
        else
        {


            if (isOnSpot)
            {
                //AmbulanceTravelTime.Travel();

                AmbulanceTravelTime.travel = true;

                print("aqui");
                transform.SetParent(bedPlayerPos.transform);
                transform.position = new Vector3(bedPlayerPos.transform.position.x, bedPlayerPos.transform.position.y - 4.5f , bedPlayerPos.transform.position.z);
                transform.localRotation = Quaternion.Euler(bedPlayerPos.transform.rotation.x, bedPlayerPos.transform.rotation.y, bedPlayerPos.transform.rotation.z);
                animator.SetBool("isGrabed", false);

                CanvasScript.PessSalvas++;

                isOnSpot = false;

            }
            else
            {
                //transform.position = new Vector3(transform.position.x, -3.5f, transform.position.z);
                //transform.GetComponent<BoxCollider>().isTrigger = false;
                //animator.SetBool("isGrabed", false);
            }

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

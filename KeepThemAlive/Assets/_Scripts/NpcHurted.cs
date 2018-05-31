using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcHurted : MonoBehaviour
{
    public GameObject MetaSpot;
    public Material Transparent;
    public Material GreenTransparent;
    public GameObject Canvas;
    public Animator animator;

    private float health;

    public static bool carried;

    GameObject maca;

    private bool isOnSpot;

    private GameObject AmbuSpot;

    private GameObject bedPlayerPos;

    public static Canvas Healthcanvas;

    Image image;

    private bool thisGirlIsOnFire;

    public bool faliceu;

    private void Awake()
    {
        health = 100;

        faliceu = false;

        thisGirlIsOnFire = false;
        //image = transform.GetChild(8).GetChild(0).GetComponent<Image>();

        //image.fillAmount = 50;

        AmbuSpot = GameObject.Find("WeakSpot");

        carried = false;
        isOnSpot = false;

        animator = GetComponent<Animator>();

        //health = 100f - ReplaceImpact.totalheathImpact;
        //Debug.Log("Total Health: " + health);

        Debug.Log(AmbuSpot);

        maca = GameObject.Find("Stretcher");

        bedPlayerPos = maca.transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        
        Carried();


        
        if(faliceu)
        {
            Destroy(transform.GetComponent<BoxCollider>());
            //transform.gameObject.SetActive(false);
        }

    }


    public float Health()
    {
        return Random.RandomRange(50, 100) - (GenerateWreckage.objsDensity * 100) / 2;
    }

    public bool IsOnFire()
    {
        return thisGirlIsOnFire;
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

        if (collision.tag == "Fire")
        {
            thisGirlIsOnFire = true;
        }

        if (collision.gameObject == AmbuSpot)
        { 
            isOnSpot = true;

        }
        else
        {
            //isOnSpot = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    void Carried()
    {
        if(carried)
        {
           if(Healthcanvas != null)
            {
                Healthcanvas.gameObject.SetActive(false);
            }

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

                Healthcanvas.gameObject.SetActive(false);


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
                if(Healthcanvas!= null)
                Healthcanvas.gameObject.SetActive(true);

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

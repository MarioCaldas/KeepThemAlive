using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NpcController : MonoBehaviour
{
    public static GameObject[] NPCs;
    public Image healthBar;

    public Transform target;
    float moveSpeed = 2f;
    float rotationSpeed = 2f;
    float damageAmount = 0.5f;
    public bool canFollow = false;
    string playerTag = "Player";
    float startHealth = 100f;
    float health;

    public bool isCrouch;

    private GameObject player;

    Vector3 playerDir;

    float crawlTime;

    bool crawlaLittle = false;

    public static bool evacuate = false;

    private NavMeshAgent agent;

    public Vector3 outsideEvacPos;

    private void Start()
    {
        outsideEvacPos = new Vector3(230, 0 , 280);

        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");

        health = startHealth - ReplaceImpact.totalheathImpact;

        target = null;

        crawlTime = 4f;
    }

    void Update ()
    {

        //resolver bug
        if (Vector3.Distance(transform.position, player.transform.position) < 8)
        {
            crawlaLittle = true;

        }

        CrawlALittle();



        if (canFollow)
        {
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //            Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);

            transform.LookAt(player.transform.position);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }


        //rastejar um bocado para sair debaixo da mesa, se nao o player nao consegue ver o npc
        if (crawlTime <= 1.1f)
        {
            crawlaLittle = false;
        }

        Debug.Log(evacuate);

        if(evacuate)
        {
            Evacuate();
        }
        //Debug.Log("Healthy health: " + health);
        //TakeDamageControl();
    }

    void Evacuate()
    {
        transform.GetComponent<Animator>().SetBool("run", true);
        agent.SetDestination(outsideEvacPos);
    }


    void CrawlALittle()
    {
        if (crawlaLittle)
        {
            crawlTime -= Time.deltaTime;

            transform.GetComponent<Animator>().SetBool("crawlMove", true);
            canFollow = true;
        }
        else
        {
            canFollow = false;
            transform.GetComponent<Animator>().SetBool("crawlMove", false);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == playerTag)
        {
            canFollow = false;
        }
    }

    void TakeDamageControl()
    {
        health -= damageAmount * Time.deltaTime;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("I died");
    }
}

﻿using System.Collections;
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
    public static bool canFollow = false;
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

    public bool isOnDoor = false;

    private void Start()
    {
        outsideEvacPos = new Vector3(233, 0 , 321);

        agent = GetComponent<NavMeshAgent>();


        health = startHealth - ReplaceImpact.totalheathImpact;

        target = null;

        crawlTime = 4f;
    }

    void Update ()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //resolver bug
        if(player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 8)
            {
                crawlaLittle = true;
            }

            if (isOnDoor)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 17)
                {
                    transform.GetComponent<Animator>().SetBool("run", true);
                    this.agent.SetDestination(outsideEvacPos);
                }
            }

            //rastejar um bocado para sair debaixo da mesa, se nao o player nao consegue ver o npc

            if (crawlTime <= 1.1f)
            {
                crawlaLittle = false;
            }

            if (crawlaLittle)
            {

                transform.GetComponent<Animator>().SetBool("crawlMove", true);
            }
        }
    


        //Debug.Log("Healthy health: " + health);
        //TakeDamageControl();
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

    //void TakeDamageControl()
    //{
    //    health -= damageAmount * Time.deltaTime;

    //    if(health <= 0)
    //    {
    //        Die();
    //    }
    //}

    void Die()
    {
        Debug.Log("I died");
    }
}

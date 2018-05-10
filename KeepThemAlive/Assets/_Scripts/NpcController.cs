using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Start()
    {
        health = startHealth;
    }

    void Update ()
    {
        if(canFollow)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        //Debug.Log("Healthy health: " + health);
        //TakeDamageControl();
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

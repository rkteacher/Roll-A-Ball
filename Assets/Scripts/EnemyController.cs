using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();

    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

     void FixedUpdate()
    {
        enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
    }
}

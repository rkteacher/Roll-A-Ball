using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    public bool enemyAggro = false;
    [SerializeField] SphereCollider aggroZone;

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
        if(enemyAggro == true)
        {
            enemyRb.AddForce((player.transform.position - transform.position).normalized * speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerAggro"))
        {
            enemyAggro = true;
        }
    }
}

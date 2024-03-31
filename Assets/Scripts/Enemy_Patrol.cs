using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public float speed = 5.0f;
    public List<Transform> waypoints;

    private int waypointIndex;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        waypointIndex = 0;
        range = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
       
    }

    public void Move()
    {
        transform.LookAt(waypoints[waypointIndex]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, waypoints[waypointIndex].position) < range)
        {
            waypointIndex++;
            if(waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
        }
    }
}

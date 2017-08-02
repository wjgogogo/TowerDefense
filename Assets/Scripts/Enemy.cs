using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    private Transform[] wayPoints;
    private int index = 0;

    private void Start()
    {
        wayPoints = WayPoints.wayPoints;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (index >= wayPoints.Length)
        {
            ReachDestination();
            return;
        }

        transform.Translate((wayPoints[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(wayPoints[index].position, transform.position) < 0.2f)
            index++;
    }

    private void ReachDestination()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.countAlive--;
    }
}
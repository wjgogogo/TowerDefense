using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public float hp = 100;
    public Slider hpSlider;
    public GameObject explosion;
    private Transform[] wayPoints;
    private int index = 0;

    private void Start()
    {
        wayPoints = WayPoints.wayPoints;
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
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

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
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
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
    public int getMoney = 50;
    public Transform body;
    public Slider hpSlider;
    public GameObject explosion;
    private Transform[] wayPoints;
    private int index = 0;

    private void Start()
    {
        wayPoints = WayPoints.wayPoints;
        hpSlider.maxValue = hp;
        hpSlider.value = hp;
        body.LookAt(wayPoints[index]);
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

        if (Vector3.Distance(wayPoints[index].position, transform.position) < 0.5f)
        {
            index++;
            if (index < wayPoints.Length - 1)
                body.LookAt(wayPoints[index]);
        }
    }

    public void TakeDamage(float damage)
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
        GameObject.Find("GameManager").SendMessage("ChangeMoney", getMoney);
        GameObject effect = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    private void ReachDestination()
    {
        GameObject.Find("EndCube").SendMessage("ChangeHp");

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.countAlive--;
    }
}
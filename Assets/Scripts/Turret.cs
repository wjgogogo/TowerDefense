using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretAttackType
{
    UseLaser,
    UseBullet
}

public class Turret : MonoBehaviour
{
    public TurretAttackType attackType = TurretAttackType.UseBullet;
    public float attackRateTime = 0.2f;
    public GameObject bulletPrefab;
    public LineRenderer laser;
    public GameObject laserEffect;
    public float laserDamage = 40;
    private float timer = 0.2f;
    public Transform fireTran;
    public Transform head;
    private List<GameObject> enemyList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    private void Start()
    {
        timer = attackRateTime;
    }

    private void Update()
    {
        if (attackType == TurretAttackType.UseBullet)
            timer += Time.deltaTime;
        if (enemyList.Count > 0 && enemyList[0] == null)
            UpdateEnemy();
        if (enemyList.Count != 0)
        {
            Vector3 targetPosition = enemyList[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
            if (timer > attackRateTime && attackType == TurretAttackType.UseBullet)
            {
                timer = 0;
                TurretAttack();
            }
            else if (attackType == TurretAttackType.UseLaser)
            {
                if (laser.enabled == false)
                {
                    laser.enabled = true;
                    laserEffect.SetActive(true);
                }
                laser.SetPositions(new Vector3[] { fireTran.position, enemyList[0].transform.position });
                laserEffect.transform.position = enemyList[0].transform.position;
                laserEffect.transform.LookAt(fireTran);
                enemyList[0].GetComponent<Enemy>().TakeDamage(laserDamage * Time.deltaTime);
            }
        }
        else
        {
            if (laser != null && laser.enabled == true)
            {
                laser.enabled = false;
                laserEffect.SetActive(false);
            }
        }
    }

    private void TurretAttack()
    {
        GameObject bullet = Instantiate(bulletPrefab, fireTran.position, fireTran.rotation);
        bullet.GetComponent<Bullet>().SetTarget(enemyList[0].transform);
    }

    private void UpdateEnemy()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] == null)
            {
                enemyList.RemoveAt(i);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    public float speed = 20;
    public GameObject explosionEffectPrefab;
    private Transform target;
    private float distanceArriveTarget = 0.6f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if ((transform.position - target.position).magnitude <= distanceArriveTarget)
        {
            target.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }
}
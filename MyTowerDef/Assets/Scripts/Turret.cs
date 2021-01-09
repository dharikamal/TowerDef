using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform rotater;

    public float turnSpeed = 10f;

    public GameObject bulletprefab;
    public Transform firePoint;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotaion = Quaternion.LookRotation(dir);
        Vector3 rotaion = Quaternion.Lerp(rotater.rotation, lookRotaion, Time.deltaTime * turnSpeed).eulerAngles;
        rotater.rotation = Quaternion.Euler(0f, rotaion.y, 0f);

        if (fireCountdown <= 0)
        {
            shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;

    }
    void shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)

            bullet.seek(target);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

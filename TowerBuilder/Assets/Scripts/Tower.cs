using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private Enemy enemyTarget;
    private Vector3 projectileSpawnPoint;
    private float findTargetTimer;
    [SerializeField] private float findTargetTimerMax = 0.2f; // search every 200 Miliseconds
    [SerializeField] float targetRadius = 20f; //
    private float fireRate;
    [SerializeField] private float fireRateMax;

    private void Awake()
    {
        projectileSpawnPoint = transform.Find("arrowSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleTargeting()
    {
        // Target Timer
        findTargetTimer -= Time.deltaTime;

        if (findTargetTimer < 0f)
        {
            findTargetTimer += findTargetTimerMax;
            FindTarget();
        }
    }

    private void HandleShooting()
    {
        fireRate -= Time.deltaTime;
        if (fireRate <= 0f)
        {
            fireRate += fireRateMax;
            // only shoots if we have a target
            if (enemyTarget != null)
            {
                Projectile.Create(projectileSpawnPoint, enemyTarget);
            }
        }        
    }

    /// <summary>
    ///  Enemy Finds a Target
    /// </summary>
    private void FindTarget()
    {        
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetRadius);

        foreach (Collider2D collider2D in collider2Ds)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                // It is a enemy
                if (enemyTarget == null)
                {
                    enemyTarget = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, enemyTarget.transform.position))
                    {
                        // Closer Target
                        enemyTarget = enemy;
                    }
                }
            }
        }
    }
}

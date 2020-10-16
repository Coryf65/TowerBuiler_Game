using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // get a target enemy
    // then move towards
    // oncollision deal destroy ?

    /// <summary>
    ///  Spawns a ArrowProjectile
    /// </summary>
    /// <param name="position">at this position</param>
    /// <param name="enemy">the Target Enemy</param>
    /// <returns>Projectile arrow</returns>
    public static Projectile Create(Vector3 position, Enemy enemy)
    {
        // Getting our Prefab
        Transform pfArrow = Resources.Load<Transform>("pf_ArrowProjectile");
        // Create an instantiate
        Transform arrowTransform = Instantiate(pfArrow, position, Quaternion.identity);
        // returning this referece to this object
        Projectile arrow = arrowTransform.GetComponent<Projectile>();
        arrow.FindTarget(enemy);
        return arrow;
    }

    private Enemy enemyTarget;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private int damageAmount = 10;
    private Vector3 lastMovementDirection;
    private float projectileLifeSpan = 2f; // 2 Seconds

    // Update is called once per frame
    void Update()
    {
        // using a Kniematic rgigid body to practice
        Vector3 moveDirection;

        if (enemyTarget != null)
        {
            moveDirection = (enemyTarget.transform.position - transform.position).normalized;
            // storing last move dir to have arrow keep moving
            lastMovementDirection = moveDirection;
        } else
        {
            moveDirection = lastMovementDirection;
        }
        
        transform.position += moveDirection * projectileSpeed * Time.deltaTime;
        // setting the rotation of our arrow
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));

        projectileLifeSpan -= Time.deltaTime;

        if (projectileLifeSpan <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FindTarget(Enemy enemy)
    {
        enemyTarget = enemy;
    }

    // testing for a collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // see if we collided with an enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            // hit an enemy
            enemy.GetComponent<HealthSystem>().TakeDamage(damageAmount);
            // destroy this arrow
            Destroy(gameObject);
        }
    }
}

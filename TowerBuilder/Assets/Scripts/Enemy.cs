using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy Create(Vector3 position)
    {
        // Getting our Prefab
        Transform enemyPrefab = Resources.Load<Transform>("Enemy");
        // Create an instantiate
        Transform enemyTransform = Instantiate(enemyPrefab, position, Quaternion.identity);
        // returning this referece to this object
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    private Transform targetTransform;
    private Rigidbody2D rigidbody2D;
    private HealthSystem healthSystem;

    [SerializeField] private float findTargetTimer;
    [SerializeField] private float findTargetTimerMax = 0.2f; // search every 200 Miliseconds
    [SerializeField] private float movementSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        // move towards a building at first the HQ and then touch and do damage
        targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDestroyed += HealthSystem_OnDestroyed;
        // adding a slight Randomness so Enemies won't loook
        // For a traget at the very same frame every time
        // if 10 get spawned at the same time they will all have diff timers
        findTargetTimer = Random.Range(0f, findTargetTimerMax);
        
    }

    private void HealthSystem_OnDestroyed(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleTargeting();
    }

    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            // handling the movements be using the RigidBody
            // need a direction so use a Vector
            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;

            rigidbody2D.velocity = moveDirection * movementSpeed;            
        }
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

    // Handle what happens when an enemy touches a building
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // happens when there is a collision between 2 physics objects
        Building building = collision.gameObject.GetComponent<Building>();
        if (building != null)
        {
            // collided with a building
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            // deal damage to the touched building
            healthSystem.TakeDamage(10);
            // now our enemy is destroyed
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///  Enemy Finds a Target
    /// </summary>
    private void FindTarget()
    {
        float targetRadius = 10f;
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, targetRadius);

        foreach (Collider2D collider2D in collider2Ds)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                // It is a building
                if (targetTransform == null)
                {
                    targetTransform = building.transform;
                } else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) < Vector3.Distance(transform.position, targetTransform.position))
                    {
                        // Closer Target
                        targetTransform = building.transform;
                    }
                }
            }
        }

        // setting default of HQ as the target is all other buildings are destroyed
        if (targetTransform == null)
        {
            // Noting in Range
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}

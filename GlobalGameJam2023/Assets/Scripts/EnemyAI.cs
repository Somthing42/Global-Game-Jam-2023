using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //patroling
   public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float walkSpeed;

    float walkReset = 2;
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Transform bulletSpawn;

    //attack type
    public string attackType;
    public float meleeDamage;
    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public float knockback;

    private void Awake()
    {
        //later call the game manager that will have the player found already
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        
        if (!walkPointSet || walkReset <= 0) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            walkReset -= Time.deltaTime;

        }
        //calculate distance to walk point
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walk point reached
        if (distanceToWalkPoint.magnitude <= 2f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
       
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        
        if (Physics.Raycast(walkPoint, -transform.up, 2f ) || walkReset <= 0)
        {
            //Debug.Log("patrolling");
            walkPointSet = true;
            walkReset = 2;
        }
    }


    private void ChasePlayer()
    {
        agent.speed = walkSpeed;
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {

        if (attackType == "Shoot")
        {
            //stops enemy from moving
            agent.SetDestination(transform.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                //attacked code goes here
                Instantiate(projectile, bulletSpawn.position, Quaternion.identity);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        if(attackType == "Charge")
        {

        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("i see the player");

            collision.gameObject.GetComponent<Damageable>().TakeDamage(meleeDamage, collision.gameObject.transform.position, collision.gameObject.transform.position);
            gameObject.transform.position += transform.forward * Time.deltaTime * knockback;
        }


       

    }
}



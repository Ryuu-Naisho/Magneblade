using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private int ChaseRange;
    [SerializeField] private int AttackRange;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    private wAnimatorUtils animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool chase = false;
    private bool canMove = true;
    private bool attack = false;
    private bool idle = true;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<wAnimatorUtils>();
        playerController = playerTransform.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            PlayerFinder();

            if(chase)
            {
                animator.Run();
                goToPoint(playerTransform.position);
            }
            if (attack)
            {
                animator.Attack();
                playerController.Hit();
            }
            
            if (!chase && !attack && idle)
            {
                animator.Idle();
            }
        }
    }


    public void Hit()
    {
        agent.isStopped = true;
        canMove = false;
        chase = false;
        attack = false;
        idle = false;
        animator.Hit();
        health --;
        if (health <= 0)
        {
            Die();
        }
        Action startMove = ()=>
        {
            canMove = true;
            agent.isStopped = false;
        };
        StartCoroutine(Wait(stunnedTime, startMove));
    }


    public void Die()
    {
        agent.isStopped = true;
        agent.enabled = false;
        canMove = false;
        chase = false;
        attack = false;
        idle = false;
        animator.Death();
        Destroy(gameObject, 5f);
    }


    private void PlayerFinder()
    {
        bool isNear = false;
        float distance = (playerTransform.position-this.transform.position).sqrMagnitude;
        if (distance<ChaseRange*ChaseRange && distance > AttackRange*AttackRange) {
            if (!chase)
            {
                idle = false;
                chase = true;
            }
        }
        else if (distance<= (agent.stoppingDistance * agent.stoppingDistance)+1) {
            if (!attack)
            {
                idle = false;
                attack = true;
            }
            if (chase)
            {
                chase = false;
            }
        }
        else if (distance >= ChaseRange*ChaseRange)
        {
            idle = true;
            chase = false;
            attack = false;
        }
    }


    //<summary>Go to vector3 point.</summary>
    private void goToPoint(Vector3 point_destination)
    {
        agent.SetDestination(point_destination);
    }


        
    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private int ChaseRange;
    [SerializeField] private int AttackRange;
    [SerializeField] private int health;
    [SerializeField] private float stunnedTime;
    [SerializeField] private AudioClip[] NPCAudioClips;
    private Transform playerTransform;
    private wAnimatorUtils animator;
    private UnityEngine.AI.NavMeshAgent agent;
    private AudioUtil audioUtil;
    private bool chase = false;
    private bool canMove = true;
    private bool attack = false;
    private bool idle = true;
    private wNames names;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<wAnimatorUtils>();
        audioUtil = GetComponent<AudioUtil>();
        names = new wNames();
        GameObject playerGameObject = GameObject.Find(names.Player);
        playerTransform = playerGameObject.transform;
        playerController = playerGameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DoSound();
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
            }
            
            if (!chase && !attack && idle)
            {
                animator.Idle();
            }
        }
    }


    private AudioClip GetRandomClip(AudioClip[] clips)
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }


    private void DoSound()
    {
        if (!audioUtil.isPlaying)
        {
            AudioClip clip  = GetRandomClip(NPCAudioClips);
            audioUtil.Play(clip);
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

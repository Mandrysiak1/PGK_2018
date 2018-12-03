using System.Collections;
using System.Collections.Generic;
using QTE;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class WaypointWandering : MonoBehaviour, IWandering {

    public NavMeshAgent agent;
    public float wandererRadius = 3f;

    private Vector3 wanderPoint;
    public Transform[] wayPoints;
    private int currentWayPoint = 0;

    private bool isRunning = true;
    private bool isWalking = false;
    private bool isStopped = false;
    private float luck = 0f;
    private float originalSpeed;
    private readonly float speedMultiplier = 2.3f;
    private readonly float startTime = 3.0f;
    private readonly float repeatRate = 3.0f;
    private PlayerCollisionHandler CollisionHandler;

    private bool collidedWithPlayer = false;
    private Animator animator;
    private float originalAnimationSpeed;

    //animation
    //public ThirdPersonCharacter character;

    void Start ()
    {
        if (CollisionHandler == null)
            CollisionHandler = FindObjectOfType<PlayerCollisionHandler>();
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        originalSpeed = agent.speed;
        InvokeRepeating("AddDynamic", startTime, repeatRate);
        transform.GetComponentInChildren<WanderingText>().text.enabled = false;
        // I'm weary of it
        animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        originalAnimationSpeed = animator.speed;
    }

	void Update () {
        Wander();
        /*
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }
        */
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wayPoints[currentWayPoint].position) < 2f)
        {
            if (currentWayPoint == wayPoints.Length - 1)
            {
                currentWayPoint = 0;
            }
            else
            {
                currentWayPoint++;
            }
        }
        else
        {
            agent.SetDestination(wayPoints[currentWayPoint].position);
        }
    }

    public void AddDynamic()
    {
        if (agent.speed != originalSpeed)
        {
            agent.speed = originalSpeed;
            animator.speed = originalAnimationSpeed;
            isWalking = true;
            isRunning = false;
            luck = 0;
        }
        else
        {
            luck = Random.Range(1, 11);
            if (luck < 6 && luck != 0)
            {
                isWalking = false;
                isRunning = true;
                originalSpeed = agent.speed;
                agent.speed = originalSpeed * speedMultiplier;
                animator.speed = originalAnimationSpeed * speedMultiplier;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            CollisionHandler.CustomerCollision(this);
            transform.GetComponentInChildren<WanderingText>().text.enabled = true;
            StartCoroutine(StopBecauseOfPlayerCoroutine());
        }
    }

    // ppff
    private IEnumerator StopBecauseOfPlayerCoroutine()
    {
        StopBecauseOfPlayer();
        yield return new WaitForSeconds(1.0f);
        transform.GetComponentInChildren<WanderingText>().text.enabled = false;
        ResumeBecauseOfPlayer();
    }

    private void StopBecauseOfPlayer()
    {
        animator.speed = originalAnimationSpeed;
        animator.SetFloat("Angery", 0.4f);
        collidedWithPlayer = true;
        agent.speed = 0;
    }
    private void ResumeBecauseOfPlayer()
    {
        animator.SetFloat("Angery", 0.0f);
        collidedWithPlayer = false;
        agent.speed = originalSpeed;
    }

}

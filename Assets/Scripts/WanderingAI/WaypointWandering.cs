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
    private readonly float speedMultiplier = 1.8f;
    private QTEController QTE;
    private PlayerCollisionHandler CollisionHandler;

    //animation
    //public ThirdPersonCharacter character;

    void Start ()
    {
        if (QTE == null)
            QTE = FindObjectOfType<QTEController>();
        if (CollisionHandler == null)
            CollisionHandler = FindObjectOfType<PlayerCollisionHandler>();
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        originalSpeed = agent.speed;
        InvokeRepeating("AddDynamic", 5, 5);
        transform.GetComponentInChildren<WanderingText>().text.enabled = false;
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
            isWalking = true;
            isRunning = false;
            luck = 0;
        }
        else
        {
            luck = Random.Range(1, 11);
            if (luck < 5 && luck != 0)
            {
                isWalking = false;
                isRunning = true;
                originalSpeed = agent.speed;
                agent.speed = originalSpeed * speedMultiplier;
            }
            /*
            else if (luck == 5)
            {
                agent.speed = 0f;
                ren.material.color = Color.red;
            }
            */
            else
            {
                ;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            CollisionHandler.CustomerCollision(this);
            transform.GetComponentInChildren<WanderingText>().text.enabled = true;
            StartCoroutine(wait());
        }
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1.5f);
        transform.GetComponentInChildren<WanderingText>().text.enabled = false;
    }

}

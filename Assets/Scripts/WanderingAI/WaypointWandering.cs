using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointWandering : MonoBehaviour {

    public NavMeshAgent agent;
    private Renderer ren;
    public float wandererRadius = 7f;

    private Vector3 wanderPoint;
    public Transform[] wayPoints;
    private int currentWayPoint = 0;

    private bool isRunning = true;
    private bool isWalking = false;
    private bool isStopped = false;
    private float luck = 0f;
    private float originalSpeed;
    private readonly float speedMultiplier = 1.8f;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        ren = GetComponent<Renderer>();
        ren.material.color = Color.yellow;
        originalSpeed = agent.speed;
        InvokeRepeating("AddDynamic", 5, 5);
    }
	
	// Update is called once per frame
	void Update () {
        Wander();
	}

    void Wander()
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
            ren.material.color = Color.yellow;
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
                ren.material.color = Color.black;
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
}

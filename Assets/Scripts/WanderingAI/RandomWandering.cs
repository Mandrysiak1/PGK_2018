using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomWandering : MonoBehaviour, IWandering {
    public NavMeshAgent agent;
    private Renderer ren;
    public float wandererRadius = 7f;

    private Vector3 wanderPoint;
    private Vector3 currentPosition;
    private Vector3 lastPosition;

    private bool isRunning = true;
    private bool isWalking = false;
    private bool isStopped = false;
    private float luck = 0f;
    private float originalSpeed;
    private readonly float speedMultiplier = 1.8f;

    public float sightDistance = 10f;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        ren = GetComponent<Renderer>();
        ren.material.color = Color.magenta;
        originalSpeed = agent.speed;
        currentPosition = transform.position;
        wanderPoint = RandomWanderPoint();
        InvokeRepeating("ChangeDestination", 0, 0.25f);
        InvokeRepeating("AddDynamic", 5, 5);
    }
	
	void Update () {
        Wander();
        WanderingCustomerDetection();
    }

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wandererRadius) + transform.position;
        NavMeshHit navMeshHit;
        NavMesh.SamplePosition(randomPoint, out navMeshHit, wandererRadius, -1);
        Vector3 destinationPoint = new Vector3(navMeshHit.position.x, transform.position.y, navMeshHit.position.z);
        return destinationPoint;
    }

    public void Wander()
    {
        if (Vector3.Distance(transform.position, wanderPoint) < 1f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint);
        }
    }

    public void ChangeDestination()
    {
        lastPosition = transform.position;
        if (lastPosition == currentPosition)
        {
            Debug.Log("I'm stuck, help me!");
            SetRandomWaypoint();
        }
        currentPosition = transform.position;
    }


    private void SetRandomWaypoint()
    {
        wanderPoint = RandomWanderPoint();
        agent.SetDestination(wanderPoint);
    }

    public void AddDynamic()
    {
        if (agent.speed != originalSpeed)
        {
            agent.speed = originalSpeed;
            ren.material.color = Color.magenta;
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
            else
            {
                ;
            }
        }
    }

    private void WanderingCustomerDetection()
    {
        RaycastHit hit;
        /*
        Debug.DrawRay(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), transform.forward * sightDistance, Color.green);
        Debug.DrawRay(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), (transform.forward + transform.right).normalized * sightDistance, Color.green); //right 45 degrees
        Debug.DrawRay(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), (transform.forward - transform.right).normalized * sightDistance, Color.green); //left 45 degrees
        */
        if (Physics.Raycast(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), transform.forward, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "WanderingCustomer")
            {
                SetRandomWaypoint();
            }
        }
        if (Physics.Raycast(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), (transform.forward + transform.right).normalized, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "WanderingCustomer")
            {
                SetRandomWaypoint();
            }
        }
        if (Physics.Raycast(transform.position + Vector3.down + new Vector3(0f, 1.5f, 0f), (transform.forward - transform.right).normalized, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag == "WanderingCustomer")
            {
                SetRandomWaypoint();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            Debug.Log("@^%&!*");
        }
    }
}

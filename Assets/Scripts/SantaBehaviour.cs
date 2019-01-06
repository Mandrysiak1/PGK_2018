using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SantaBehaviour : MonoBehaviour
{
    [SerializeField]
    private OrderSource OrderSource;
    [SerializeField]
    private float RunningTime = 2.0f;

    private Animator animator;
    private bool orderPlaced = false;

    void Start ()
    {
        animator = GetComponent<Animator>();
    }
	
    void Update ()
    {
        if (OrderSource.CurrentOrder != null)
        {
            orderPlaced = true;
        }

        if (orderPlaced && OrderSource.CurrentOrder == null)
        {
            animator.SetBool("Running", true);
            StartCoroutine("StopRunningDelay");
        }
    }
    IEnumerator StopRunningDelay()
    {
        yield return new WaitForSeconds(RunningTime);
        animator.SetBool("Running", false);
        yield return new WaitForSeconds(0.1f);
    }
}

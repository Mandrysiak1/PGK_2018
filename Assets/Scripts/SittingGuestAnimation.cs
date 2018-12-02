using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SittingGuestAnimation : MonoBehaviour
{
    [SerializeField]
    private OrderSource OrderSource;
    [SerializeField]
    private float AnimationSpeed = 1.0f;
    [SerializeField]
    private float AnimationSpeedVariation = 0.1f;

    private Animator animator;

    void Start ()
    {
        if (OrderSource == null)
            OrderSource = gameObject.GetComponentInParent<OrderSource>();
        animator = GetComponent<Animator>();
        animator.speed = AnimationSpeed + Random.Range(-AnimationSpeedVariation, AnimationSpeedVariation);
    }

    void Update ()
    {
        animator.SetFloat("Mood", OrderSource.Mood);
    }
}

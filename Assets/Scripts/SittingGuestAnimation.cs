using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SittingGuestAnimation : MonoBehaviour
{
    [SerializeField]
    private TableScript TableScript;
    [SerializeField]
    private float AnimationSpeed = 1.0f;

    private Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
        animator.speed = AnimationSpeed;
    }

    void Update ()
    {
        animator.SetFloat("Mood", TableScript.MyTable.Mood);
    }
}

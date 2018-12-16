using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FightingGuestAnimator : MonoBehaviour
{
    enum StartStateEnum
    {
        Punching = 0,
        AwaitingBeingHit,
        BeingHit,
        AwaitingPunching
    }

    [SerializeField]
    private StartStateEnum StartState = StartStateEnum.Punching;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float AnimationOffset = 0.0f;

    private Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
        string stateName = "Kek";
        if (StartState == StartStateEnum.Punching)
            stateName = "Punching";
        else if (StartState == StartStateEnum.AwaitingBeingHit)
            stateName = "AwaitingBeingHit";
        else if (StartState == StartStateEnum.BeingHit)
            stateName = "BeingHit";
        else if (StartState == StartStateEnum.AwaitingPunching)
            stateName = "AwaitingPunching";

        float length = 0.0f;
        for(int i = 0; i < animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            if (animator.runtimeAnimatorController.animationClips[i].name == stateName)
            {
                length = animator.runtimeAnimatorController.animationClips[i].length;
                break;
            }
        }
        animator.Play(stateName, 0, AnimationOffset / length);
    }
	
    void Update ()
    {
        
    }
}

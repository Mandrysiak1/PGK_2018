using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorOffsetSetter : MonoBehaviour
{
    [SerializeField]
    private float AnimationOffset = 0.0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        string name = animator.runtimeAnimatorController.animationClips[0].name;
        float length = animator.runtimeAnimatorController.animationClips[0].length;
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, AnimationOffset / length);
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
    }
    public virtual void ChangeState(string animationName, float crossFadeTime)
    {
        animator.CrossFade(animationName, crossFadeTime);
    }
}

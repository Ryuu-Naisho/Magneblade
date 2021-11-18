using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Animator))]
public class wAnimatorUtils : MonoBehaviour
{
    private bool dead = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    { 
    } 


    private string GetAnimationPlaying()
    {


        string animation = "";



        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            animation = "attack";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            animation = "idle";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            animation = "walk";
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            animation = "run";



        return animation;
    }

    private bool isAttracting => animator.GetCurrentAnimatorStateInfo(0).IsName("attract");


    public void Hit()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("hit"))
        animator.ResetTrigger("idle");
        animator.ResetTrigger("run");
        animator.ResetTrigger("walk");
        animator.ResetTrigger("attack");
        animator.SetTrigger("hit");
    }

    public void Death()
    {
        animator.ResetTrigger("idle");
        animator.ResetTrigger("run");
        animator.ResetTrigger("walk");
        animator.ResetTrigger("hit");
        animator.ResetTrigger("attack");
        animator.SetTrigger("death");
        dead = true;
    }


    public void Attack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            animator.ResetTrigger("run");
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
            animator.SetTrigger("attack");
    }


    public void Attract()
    {
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("attract"))
            animator.SetTrigger("attract");
    }


    public void StopAttract()
    {
        if (!dead )
            animator.ResetTrigger("attract");
    }


    public void Idle()
    {
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            animator.SetTrigger("idle");
    }

    public void Run()
    {
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("run"))
            animator.SetTrigger("run");
    }



    public void Walk()
    {
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            animator.SetTrigger("walk");
    }


    public void Fire()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("attract"))
            animator.ResetTrigger("attract");
        if (!dead && !animator.GetCurrentAnimatorStateInfo(0).IsName("fire"))
            animator.SetTrigger("fire");
    }
}

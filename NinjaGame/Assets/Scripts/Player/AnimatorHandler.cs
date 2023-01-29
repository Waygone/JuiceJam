using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    Animator animator;
    private string currentState;

    const string IDLE = "Idle";
    const string RUNNING = "Running";
    const string JUMP = "Jump";
    const string FALL = "Fall";
    const string DASH = "Dash";
    const string ATTACK = "Attack";


    void Start()
    {
        animator = GetComponent<Animator>();
    }

   void ChangeAnimationState(string newState)
    {
        if(currentState == newState) return;

        animator.Play(newState);
        print(newState);

        currentState = newState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    //Collecting scripts
    [SerializeField]
    internal PlayerInputScript inputScript;

    [SerializeField]
    internal PlayerMovementScript movementScript;

    [SerializeField]
    internal PlayerCollisionScript collisionScript;

    //Main Properties
    [SerializeField]
    internal int health;
    [SerializeField]
    internal float speed = 0;

    //Components

    internal Animator anim;
    internal Rigidbody2D rb;

    //other references
    internal string currentState;

    private void Start()
    {
        print("Main PlayerScript Starting");
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //State Manager
    internal void ChangeState(string newState)
    {
        if (newState != currentState)
        {
            anim.Play(newState);
            currentState = newState;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve Acceleration;
    [SerializeField] private AnimationCurve Deceleration;
    Rigidbody2D rb;
    [SerializeField] Vector3 movement;
    [SerializeField] bool left = false;
    [SerializeField] bool right = false;
    [SerializeField] bool accel = false;
    [SerializeField] bool decel = false;
    float time = 0;
    bool dash;
    [SerializeField] float speed = 0;
    [SerializeField] float dashAmount;
    [SerializeField] float speedtimer;
    [SerializeField] float speedcountdown;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speedtimer = speedcountdown;
        // dash is set to max
    }

    private void Update()
    {
        speed = Acceleration.Evaluate(time);
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && speedtimer == speedcountdown)
            {
                //if player is moving and dash button is pressed
                dash= true;
            } else
            {
                dash = false;
            }
            time += Time.deltaTime;
            if (horizontalMovement < 0)
            {
                left = true;
                right = false;
            }
            else if (horizontalMovement > 0) {
                left = false;
                right = true;
            }
        }
        else
        {
            left = false;
            right = false;
        }

        MoveUpdate(speed);
        if (!dash)
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, 0);
        } else if (dash)
        {
            speedtimer = speedcountdown;
            speedtimer -= Time.deltaTime;
            if (left)
            {
                rb.velocity = new Vector2(-dashAmount, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(dashAmount, rb.velocity.y);
            }
        }
        
    }

    public Vector3 MoveUpdate(float speed)
    {
        if (left)
        {
            movement.x = -1 * speed;
            left = true;
            right = false;
            accel = true;
            decel = false;
            //moving left
            //accel left
        } else if (!right)
        {
            time = 0;
            time += Time.deltaTime;
            speed = Deceleration.Evaluate(time);
            movement.x = movement.x * speed;
            left = true;
            right = false;
            accel = false;
            decel = true;
            //was moving left but now not
            //decel from left
        }

        if (right)
        {
            movement.x = 1 * speed;
            left = false;
            right = true;
            accel = true;
            decel = false;
            //moving right
            //accel right
        } else if (!left)
        {
            time = 0;
            time += Time.deltaTime;
            speed = Deceleration.Evaluate(time);
            movement.x = movement.x * speed;
            left = false;
            right = true;
            accel = false;
            decel = true;
            //was moving right but now not
            //decel from right
        }


        return movement;
    }
}

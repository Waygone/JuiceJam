using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve acceleration;
    [SerializeField] float speed;
    Rigidbody2D rb;
    float time = 0f;

    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (horizontalMovement != 0 )
        {
            time += Time.deltaTime;
            speed = acceleration.Evaluate(time);
        } else
        {
            time = 0;
        }

        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
    }
}

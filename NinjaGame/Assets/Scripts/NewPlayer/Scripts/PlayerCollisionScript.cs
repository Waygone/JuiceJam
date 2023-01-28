using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    private void Start()
    {
        print("PlayerCollisionScript Starting" + playerScript.rb);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerScript.ChangeState("Hurt");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerScript.ChangeState("Idle");
        }
    }
}

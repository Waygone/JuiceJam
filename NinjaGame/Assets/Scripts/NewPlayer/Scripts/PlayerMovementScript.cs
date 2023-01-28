using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    private void Start()
    {
        print("PlayerMovementScript Starting");
    }

    private void Update()
    {
        //Check input manager for button press
        if (playerScript.inputScript.isLeftPressed)
        {
            MovePlayerLeft();
        } else if (playerScript.inputScript.isRightPressed)
        {
            MovePlayerRight();
        } else
        {
            StopMovement();
        }
    }


    private void MovePlayerLeft()
    {
        playerScript.rb.velocity = new Vector2(-playerScript.speed, 0);
    }

    private void MovePlayerRight()
    {
        playerScript.rb.velocity = new Vector2(playerScript.speed, 0);
    }

    private void StopMovement()
    {
        playerScript.rb.velocity = new Vector2(0, 0);
    }
}

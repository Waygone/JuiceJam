using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField]
    PlayerScript playerScript;

    //variables
    internal bool isLeftPressed;
    internal bool isRightPressed;
    internal bool isJumpPressed;
    internal bool isDashPressed;

    private void Start()
    {
        print("PlayerInputScript starting");
    }

    private void Update()
    {
        //handles inputs only

        if (Input.GetKey(KeyCode.A))
        {
            isLeftPressed = true;
        }
        else
        {
            isLeftPressed = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isRightPressed = true;
        } else
        {
            isRightPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed= true;
        } else
        {
            isJumpPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDashPressed= true;
        }
        else
        {
            isDashPressed = false;
        }

        if (isLeftPressed || isRightPressed)
        {
            playerScript.ChangeState("Running");
        } else
        {
            playerScript.ChangeState("Idle");
        }
    }
}

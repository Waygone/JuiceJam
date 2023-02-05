using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    
  

    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
        
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpForce);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
        if (amountOfJumpsLeft > 1)
        {
            player.jump.PlayOneShot(player.jumpClip);
        }
        else
        {
            player.doubleJump.PlayOneShot(player.doubleJumpClip);
        }
    }

    public bool CanJump()
    {
        return amountOfJumpsLeft > 0 ? true : false;
    }
    public void ResetJumpsAmount() => amountOfJumpsLeft = playerData.amountOfJumps;

    public void DecreaseJumpsAmount() => amountOfJumpsLeft--;
}

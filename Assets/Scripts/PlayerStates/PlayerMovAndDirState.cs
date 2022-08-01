using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovAndDirState : PlayerBaseState
{
    private bool toIdle = false;
    public override void EnterState(Player_FSM player)
    {
        toIdle = false;
        player.pointerBar.SetActive(true);
    }
    public override void ExitState(Player_FSM player)
    {
        if(toIdle)
            player.TransitionToState(player.idleState);
        else player.TransitionToState(player.powerState);
    }
    public override void Update(Player_FSM player)
    {
        player.Aiming();

        switch (player.playerColor)
        {
            case 1 when Input.GetAxis("Horizontal_01") != 0.0f:
            case 2 when Input.GetAxis("Horizontal_02") != 0.0f:
                Player_FSM.HorizontalMovement(player);
                break;
            default:
                toIdle = true;
                ExitState(player);
                break;
        }

        if (Player_FSM.Shoot(player))
            ExitState(player);
    }
}

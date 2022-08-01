using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private bool toPower = false;
    public override void EnterState(Player_FSM player)
    {
        toPower = false;
        player.pointerBar.SetActive(true);
    }
    public override void ExitState(Player_FSM player)
    {
        if(toPower)
            player.TransitionToState(player.powerState);
        else
            player.TransitionToState(player.movAndDirState);
    }
    public override void Update(Player_FSM player)
    {
        player.Aiming();

        switch (player.playerColor)
        {
            case 1 when Input.GetAxis("Horizontal_01") != 0.0f:
            case 2 when Input.GetAxis("Horizontal_02") != 0.0f:
                ExitState(player);
                break;
        }

        if (!Player_FSM.Shoot(player)) return;
        toPower = true;
        ExitState(player);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootState : PlayerBaseState
{
    public override void EnterState(Player_FSM player)
    {
        player.StartCoroutine(LetThemExit(player));
    }

    public override void ExitState(Player_FSM player)
    {
        player.TransitionToState(player.idleState);
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
        }
    }

    private IEnumerator LetThemExit(Player_FSM player)
    {
        yield return new WaitForSeconds(.75f);
        ExitState(player);
    }
}

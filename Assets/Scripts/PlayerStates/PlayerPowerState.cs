using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerState : PlayerBaseState
{
    public override void EnterState(Player_FSM player)
    {
            player.powerBar.SetActive(true);
    }
    public override void ExitState(Player_FSM player)
    {
        player.powerBar.SetActive(false);
        player.TransitionToState(player.shootState);
    }
    public override void Update(Player_FSM player)
    {
        PowerLevel(player);
        switch (player.playerColor)
        {
            case 1 when Input.GetAxis("Horizontal_01") != 0.0f:
            case 2 when Input.GetAxis("Horizontal_02") != 0.0f:
                Player_FSM.HorizontalMovement(player);
                break;
        }

        if (!Firing(player)) return;
        player.ReleaseTheBullet();
        ExitState(player);
    }
    private static void PowerLevel(Player_FSM player)
    {
        if (Firing(player)) return;
        player.sliderPower.value = Mathf.PingPong(player.t, 1);
        player.t += 1 * Time.deltaTime;
    }

    private static bool Firing(Player_FSM player)
    {
        return player.playerColor switch
        {
            1 => Input.GetButtonDown("Fire_01"),
            2 => Input.GetButtonDown("Fire_02"),
            _ => false
        };
    }
}

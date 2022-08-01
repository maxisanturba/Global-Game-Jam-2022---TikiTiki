using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(Player_FSM player);
    public abstract void ExitState(Player_FSM player);
    public abstract void Update(Player_FSM player);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerSquating : State
    {
        protected override void OnEnter(State prevState)
        {
        }
    }
}
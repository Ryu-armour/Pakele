using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerDead : State
    {
        protected override void OnEnter(State prevState)
        {
            //�����~
            Owner.enabled = false;
            Owner.GetComponent<Collider>().enabled = false;


        }
    }

}
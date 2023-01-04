using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerStanding : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log("Standing");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //A・Dキー押下で歩く状態に遷移
                StateMachine.Dispatch((int)Event.Walk);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //スペースキー押下でジャンプ状態に遷移
                StateMachine.Dispatch((int)Event.Jump);
            }
            else if (!Owner.isGround)
            {
                //足場がなかったら落下状態に遷移
                StateMachine.Dispatch((int)Event.Stand);
            }

            //重力
            Owner.speed = new Vector2(Owner.rigidBody.velocity.x, -Owner.gravity);
        }
    }
}
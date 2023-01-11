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
            //反転可能にする
            Owner.canInverse = true;

            //移動ベクトルの初期化
            Owner.rigidBody.velocity = Vector2.zero;

            Debug.Log("Standing");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //A・Dキー押下で歩く状態に遷移
                StateMachine.Dispatch((int)Event.Walk);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                if (Owner.canInverse)
                {
                    //Fキー押下で反転可能な状態なら反転状態に遷移
                    stateMachine.Dispatch((int)Event.Inverse);
                }
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //スペースキー押下でジャンプ状態に遷移
                StateMachine.Dispatch((int)Event.Jump);
            }
        }

        protected override void OnFixedUpdate()
        {
            if (!Owner.isGround)
            {
                //足場がなかったら落下状態に遷移
                StateMachine.Dispatch((int)Event.Dive);
            }

            //落下
            float gravity = -Owner.gravity;
            //gravity = Mathf.Max(gravity, -Owner.maxFallSpeed);
            //重力
            //Owner.speed = new Vector2(Owner.rigidBody.velocity.x, gravity);
            Owner.speed = new Vector2(Owner.rigidBody.velocity.x, gravity * Owner.rigidBody.velocity.y);
        }
    }
}
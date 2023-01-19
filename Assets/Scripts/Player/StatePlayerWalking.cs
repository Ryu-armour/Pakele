using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerWalking : State
    {
        //移動速度
        private float xSpeed = 0.0f;
        protected override void OnEnter(State prevState)
        {
            Debug.Log("Walking");
        }
        protected override void OnUpdate()
        {
            //移動キーが離されたら立ち状態に遷移
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                StateMachine.Dispatch((int)Event.Stand);
            }
            else if (Input.GetKeyDown(KeyCode.F))
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

            //移動処理
            SetMoveSpeed();
        }

        protected override void OnFixedUpdate()
        {

            if (!Owner.isGround)
            {
                //足場がなかったら落下状態に遷移
                StateMachine.Dispatch((int)Event.Dive);
            }

            //移動
            Owner.speed = new Vector2(xSpeed, Owner.rigidBody.velocity.y);
        }

        protected override void OnExit(State nextState)
        {
        }

        //移動速度の設定
        private void SetMoveSpeed()
        {
            if (Input.GetKey(KeyCode.A))
            {
                xSpeed = -Owner.walkPower;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                xSpeed = Owner.walkPower;
            }
            else
            {
                xSpeed = 0.0f;
            }
        }
    }

}


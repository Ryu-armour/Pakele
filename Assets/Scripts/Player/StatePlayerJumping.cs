using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{

    //ジャンプ
    public class StatePlayerJumping : State
    {
        //移動速度
        private float xSpeed,ySpeed = 0.0f;
        //ジャンプの高さ
        private float jumpPos = 0.0f;

        protected override void OnEnter(State prevState)
        {
            Debug.Log("Jumping");

            //ジャンプ開始位置の記録
            jumpPos = Owner.transform.position.y;

            //ジャンプ時間の初期化
            Owner.jumpTime = 0.0f;
        }
        protected override void OnUpdate()
        {
            //ジャンプの制限時間が過ぎたか
            bool isLimit = Owner.jumpTime >= Owner.jumpLimitTime;
            //ジャンプが最高到達点に達したか
            bool isReach = Owner.transform.position.y >= jumpPos + Owner.maxJumpHeight;
            //スペースキーを離したか
            bool isDetach = Input.GetKeyUp(KeyCode.Space);

            if (Input.GetKey(KeyCode.F))
            {
                if (Owner.canInverse)
                {
                    //Fキー押下で反転可能な状態なら反転状態に遷移
                    stateMachine.Dispatch((int)Event.Inverse);
                }
            }
            else if (Owner.isHead || isLimit || isReach || isDetach)
            {
                //落下状態に遷移
                stateMachine.Dispatch((int)Event.Dive);
            }

            //移動速度の決定
            SetMoveSpeed();
        }

        protected override void OnFixedUpdate()
        {
            //ジャンプ
            ySpeed = Owner.jumpPower - Owner.gravity * Time.deltaTime;

            //移動
            Owner.speed = new Vector2(xSpeed, ySpeed);
        }

        //地面に着地した時の調整
        private void AdjustLanding()
        {
            var pos = Owner.transform.position;
            pos.y = Owner.transform.localScale.y * 0.5f;
            Owner.transform.position = pos;
        }

        //移動速度の決定
        private void SetMoveSpeed()
        {
            //横移動
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

using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerDiving : State
    {
        //移動速度
        private float xSpeed, ySpeed = 0.0f;

        protected override void OnEnter(State prevState)
        {
            Debug.Log("Diving");
        }
        protected override void OnUpdate()
        {
            if (Owner.isGround)
            {
                //接地したら立ち状態に遷移
                StateMachine.Dispatch((int)Event.Stand);
            }

            //移動
            SetMoveSpeed();

            //重力
            Owner.speed = new Vector2(xSpeed, ySpeed);
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

            //ジャンプ
            ySpeed = -Owner.gravity;
        }

    }
}

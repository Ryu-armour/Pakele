using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerDiving : State
    {
        //�ړ����x
        private float xSpeed, ySpeed = 0.0f;

        protected override void OnEnter(State prevState)
        {
            Debug.Log("Diving");
        }
        protected override void OnUpdate()
        {
            if (Owner.isGround)
            {
                //�ڒn�����痧����ԂɑJ��
                StateMachine.Dispatch((int)Event.Stand);
            }

            //�ړ�
            SetMoveSpeed();

            //�d��
            Owner.speed = new Vector2(xSpeed, ySpeed);
        }

        //�ړ����x�̌���
        private void SetMoveSpeed()
        {
            //���ړ�
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

            //�W�����v
            ySpeed = -Owner.gravity;
        }

    }
}

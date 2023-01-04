using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerWalking : State
    {
        //�ړ����x
        private float xSpeed = 0.0f;
        protected override void OnEnter(State prevState)
        {
            Debug.Log("Walking");
        }
        protected override void OnUpdate()
        {
            //�ړ��L�[�������ꂽ�痧����ԂɑJ��
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                StateMachine.Dispatch((int)Event.Stand);
                Debug.Log("go to Stand");
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //�X�y�[�X�L�[�����ŃW�����v��ԂɑJ��
                StateMachine.Dispatch((int)Event.Jump);
            }
            else if (!Owner.isGround)
            {
                //���ꂪ�Ȃ������痎����ԂɑJ��
                StateMachine.Dispatch((int)Event.Stand);
            }

            //�ړ�����
            SetMoveSpeed();
        }

        protected override void OnExit(State nextState)
        {
            Owner.transform.localScale = Owner.DEFAULT_SCALE;
        }

        //�ړ����x�̐ݒ�
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

            //�d��
            Owner.speed = new Vector2(xSpeed, Owner.rigidBody.velocity.y);
        }
    }

}

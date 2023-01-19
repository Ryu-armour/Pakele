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
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                if (Owner.canInverse)
                {
                    //F�L�[�����Ŕ��]�\�ȏ�ԂȂ甽�]��ԂɑJ��
                    stateMachine.Dispatch((int)Event.Inverse);
                }
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                //�X�y�[�X�L�[�����ŃW�����v��ԂɑJ��
                StateMachine.Dispatch((int)Event.Jump);
            }

            //�ړ�����
            SetMoveSpeed();
        }

        protected override void OnFixedUpdate()
        {

            if (!Owner.isGround)
            {
                //���ꂪ�Ȃ������痎����ԂɑJ��
                StateMachine.Dispatch((int)Event.Dive);
            }

            //�ړ�
            Owner.speed = new Vector2(xSpeed, Owner.rigidBody.velocity.y);
        }

        protected override void OnExit(State nextState)
        {
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
        }
    }

}


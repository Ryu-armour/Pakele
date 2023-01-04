using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{

    //�W�����v
    public class StatePlayerJumping : State
    {
        //�ړ����x
        private float xSpeed,ySpeed = 0.0f;
        //�W�����v�̍���
        private float jumpPos = 0.0f;

        protected override void OnEnter(State prevState)
        {
            Debug.Log("Jumping");

            //�W�����v�J�n�ʒu�̋L�^
            jumpPos = Owner.transform.position.y;
        }
        protected override void OnUpdate()
        {            
            //�X�y�[�X�L�[��b�����痎����ԂɑJ��
            if(Input.GetKeyUp(KeyCode.Space))
            {
                StateMachine.Dispatch((int)Event.Dive);
            }
            //�W�����v�̍ő�̍����܂ōs�����痎����ԂɑJ��
            else if (Owner.transform.position.y >= jumpPos + Owner.maxJumpHeight)
            {
                StateMachine.Dispatch((int)Event.Dive);
            }

            //�ړ����x�̌���
            SetMoveSpeed();

            //�ړ�
            Owner.speed = new Vector2(xSpeed, ySpeed);            
        }

        //�n�ʂɒ��n�������̒���
        private void AdjustLanding()
        {
            var pos = Owner.transform.position;
            pos.y = Owner.transform.localScale.y * 0.5f;
            Owner.transform.position = pos;
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
            ySpeed = Owner.jumpPower;
        }
    }
}

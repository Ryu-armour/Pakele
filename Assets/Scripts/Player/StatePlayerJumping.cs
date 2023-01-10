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

            //�W�����v���Ԃ̏�����
            Owner.jumpTime = 0.0f;
        }
        protected override void OnUpdate()
        {
            //�W�����v�̐������Ԃ��߂�����
            bool isLimit = Owner.jumpTime >= Owner.jumpLimitTime;
            //�W�����v���ō����B�_�ɒB������
            bool isReach = Owner.transform.position.y >= jumpPos + Owner.maxJumpHeight;
            //�X�y�[�X�L�[�𗣂�����
            bool isDetach = Input.GetKeyUp(KeyCode.Space);

            if (Input.GetKey(KeyCode.F))
            {
                if (Owner.canInverse)
                {
                    //F�L�[�����Ŕ��]�\�ȏ�ԂȂ甽�]��ԂɑJ��
                    stateMachine.Dispatch((int)Event.Inverse);
                }
            }
            else if (Owner.isHead || isLimit || isReach || isDetach)
            {
                //������ԂɑJ��
                stateMachine.Dispatch((int)Event.Dive);
            }

            //�ړ����x�̌���
            SetMoveSpeed();
        }

        protected override void OnFixedUpdate()
        {
            //�W�����v
            ySpeed = Owner.jumpPower - Owner.gravity * Time.deltaTime;

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
        }
    }
}

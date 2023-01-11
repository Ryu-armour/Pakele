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
            //���]�\�ɂ���
            Owner.canInverse = true;

            //�ړ��x�N�g���̏�����
            Owner.rigidBody.velocity = Vector2.zero;

            Debug.Log("Standing");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //A�ED�L�[�����ŕ�����ԂɑJ��
                StateMachine.Dispatch((int)Event.Walk);
            }
            else if (Input.GetKey(KeyCode.F))
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
        }

        protected override void OnFixedUpdate()
        {
            if (!Owner.isGround)
            {
                //���ꂪ�Ȃ������痎����ԂɑJ��
                StateMachine.Dispatch((int)Event.Dive);
            }

            //����
            float gravity = -Owner.gravity;
            //gravity = Mathf.Max(gravity, -Owner.maxFallSpeed);
            //�d��
            //Owner.speed = new Vector2(Owner.rigidBody.velocity.x, gravity);
            Owner.speed = new Vector2(Owner.rigidBody.velocity.x, gravity * Owner.rigidBody.velocity.y);
        }
    }
}
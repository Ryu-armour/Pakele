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
            Debug.Log("Standing");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                //A�ED�L�[�����ŕ�����ԂɑJ��
                StateMachine.Dispatch((int)Event.Walk);
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

            //�d��
            Owner.speed = new Vector2(Owner.rigidBody.velocity.x, -Owner.gravity);
        }
    }
}
using UnityEngine;

using State = StateMachine<Player>.State;

public partial class Player
{
    public class StatePlayerInversion : State
    {
        protected override void OnEnter(State prevState)
        {
            //反転不可にする
            Owner.canInverse = false;

            //反転処理
            //Owner.transform.localScale = new Vector3(Owner.transform.localScale.x, -Owner.transform.localScale.y, Owner.transform.localScale.z);

            //重力反転
            Physics2D.gravity = new Vector2(Physics2D.gravity.x, -Physics2D.gravity.y);

            //描画反転
            Owner.drawFlip = !Owner.drawFlip;
            Owner.ReverseDraw();

            Debug.Log(Physics2D.gravity);
            Debug.Log("Inverse");
        }

        protected override void OnUpdate()
        {
            //落下状態に遷移
            stateMachine.Dispatch((int)Event.Dive);
        }
    }
}


//Stateの抽象クラス
public abstract class PlayerStateBase
{    
    //ステートを開始したときに呼ばれる
    public virtual void OnEnter(Player owner, PlayerStateBase prevState) { }

    //舞フレーム呼ばれる
    public virtual void OnUpdate(Player owner) { }

    //ステートを終了したときに呼ばれる
    public virtual void OnExit(Player owner, PlayerStateBase nextState) { }
}

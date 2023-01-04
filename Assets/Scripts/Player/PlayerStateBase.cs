//State�̒��ۃN���X
public abstract class PlayerStateBase
{    
    //�X�e�[�g���J�n�����Ƃ��ɌĂ΂��
    public virtual void OnEnter(Player owner, PlayerStateBase prevState) { }

    //���t���[���Ă΂��
    public virtual void OnUpdate(Player owner) { }

    //�X�e�[�g���I�������Ƃ��ɌĂ΂��
    public virtual void OnExit(Player owner, PlayerStateBase nextState) { }
}

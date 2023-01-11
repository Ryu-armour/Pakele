using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Player : MonoBehaviour
{
    /// <summary>
    /// �萔
    /// </summary>
    //���������
    public const float FALLING_POWER = 50f;
    //�f�t�H���g�̃X�P�[��
    public Vector3 DEFAULT_SCALE;

    /// <summary>
    /// �p�u���b�N�ϐ�
    /// </summary>
    //�N���X�^�O
    public const string Tag = "Player";
    //�ڒn����(��)
    public GroundCheck ground;
    //�ڒn����(��)
    public GroundCheck head;
    //�d��
    public float gravity = 10f;
    //�ړ����x
    public float walkPower = 3f;
    //�W�����v��
    public float jumpPower = 7f;
    //�W�����v�̍ő�̍���
    public float maxJumpHeight = 4f;
    //�W�����v�̍Œ�̒Ⴓ
    public float minJumpHeight = 1f;
    //�������x���
    public float maxFallSpeed = 20f;
    //�W�����v�̐�������
    public float jumpLimitTime = 1f;

    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    //�f�t�H���g�̊g�嗦
    private Vector3 defaultScale;
    //�v���C���[�̃��W�b�h�{�f�B
    private Rigidbody2D rigidBody;
    //�ړ����x
    private Vector2 speed = Vector2.zero;
    //�ڒn����p�t���O(��)
    private bool isGround = false;
    //�ڒn����p�t���O(��)
    private bool isHead = false;
    //�W�����v���Ă��鎞��
    private float jumpTime = 0f;
    //���]���g�p�\�ɂȂ�t���O
    private bool canInverse = false;
    //�`��̔��]�t���O
    private bool drawFlip = false;

    //��Ԃ��Ǘ�����@�B
    private StateMachine<Player> stateMachine;

    private enum Event : int
    {
        Stand,  //�����~�܂�
        Walk,   //����
        Inverse,//���]
        Jump,   //�W�����v
        Dive,   //����
        Dead,   //���S
    }

    private void Start()
    {
        stateMachine = new StateMachine<Player>(this);
        defaultScale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        
        //�X�e�[�g�J�ڂ̓o�^
        RegistStateTransitionMethod();

        //�J�n�͗����~�܂�����Ԃ���
        stateMachine.Start<StatePlayerStanding>();
    }

    private void Update()
    {
        //��Ԃ̍X�V
        stateMachine.Update();

    }

    private void FixedUpdate()
    {
        //�ڒn����̎擾
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //��Ԃ̍X�V
        stateMachine.FixedUpdate();

        //�ړ�
        rigidBody.velocity = new Vector2(speed.x * this.transform.localScale.x, speed.y * this.transform.localScale.y);
    }

    private void RegistStateTransitionMethod()
    {
        //�����~�܂�����Ԃ������
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerWalking>((int)Event.Walk);
        //�����~�܂�����Ԃ��甽�]
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerInversion>((int)Event.Inverse);
        //�����~�܂�����Ԃ���W�����v
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerJumping>((int)Event.Jump);
        //�����~�܂�����Ԃ��痎��
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerDiving>((int)Event.Dive);
        //��������Ԃ��痧���~�܂�
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerStanding>((int)Event.Stand);
        //��������Ԃ��甽�]
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerInversion>((int)Event.Inverse);
        //��������Ԃ���W�����v
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerJumping>((int)Event.Jump);
        //��������Ԃ��痎��
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerDiving>((int)Event.Dive);
        //���]������Ԃ��痎��
        stateMachine.AddTransition<StatePlayerInversion, StatePlayerDiving>((int)Event.Dive);
        //�W�����v�̏�Ԃ��痧���~�܂�(���n)
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerStanding>((int)Event.Stand);
        //�W�����v�̏�Ԃ��甽�]
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerInversion>((int)Event.Inverse);
        //�W�����v�̏�Ԃ��痎��
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerDiving>((int)Event.Dive);
        //����������Ԃ��痧���~�܂�
        stateMachine.AddTransition<StatePlayerDiving, StatePlayerStanding>((int)Event.Stand);
        //����������Ԃ��甽�]
        stateMachine.AddTransition<StatePlayerDiving, StatePlayerInversion>((int)Event.Inverse);

        //�v���C���[�����񂾂�I��
        stateMachine.AddAnyTransition<StatePlayerDead>((int)Event.Dead);
    }

    //�`��𔽓]����
    private void ReverseDraw()
    {
        GetComponent<SpriteRenderer>().flipY = drawFlip;
    }
}

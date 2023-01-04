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
    //�ڒn����
    public GroundCheck ground;
    //�d��
    public float gravity = 0.0f;
    //�ړ����x
    public float walkPower = 3.0f;
    //�W�����v��
    public float jumpPower = 5.0f;
    //�W�����v�̍ő�̍���
    public float maxJumpHeight = 4.0f;

    /// <summary>
    /// �v���C�x�[�g�ϐ�
    /// </summary>
    //�f�t�H���g�̊g�嗦
    private Vector3 defaultScale;
    //�v���C���[�̃��W�b�h�{�f�B
    private Rigidbody2D rigidBody;
    //�ړ����x
    private Vector2 speed = Vector2.zero;
    //�ڒn����p�t���O
    private bool isGround = false;

    //��Ԃ��Ǘ�����@�B
    private StateMachine<Player> stateMachine;

    private enum Event : int
    {
        Stand,  //�����~�܂�
        Walk,   //����
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

        //�ړ�
        rigidBody.velocity = new Vector2(speed.x, speed.y);
    }

    private void RegistStateTransitionMethod()
    {
        //�����~�܂�����Ԃ������
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerWalking>((int)Event.Walk);
        //�����~�܂�����Ԃ���W�����v
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerJumping>((int)Event.Jump);
        //�����~�܂�����Ԃ��痎��
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerDiving>((int)Event.Dive);
        //��������Ԃ��痧���~�܂�
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerStanding>((int)Event.Stand);
        //��������Ԃ���W�����v
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerJumping>((int)Event.Jump);
        //��������Ԃ��痎��
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerDiving>((int)Event.Dive);
        //�W�����v�̏�Ԃ��痧���~�܂�(���n)
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerStanding>((int)Event.Stand);
        //�W�����v�̏�Ԃ��痎��
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerDiving>((int)Event.Dive);
        //����������Ԃ��痧���~�܂�
        stateMachine.AddTransition<StatePlayerDiving, StatePlayerStanding>((int)Event.Stand);

        //�v���C���[�����񂾂�I��
        stateMachine.AddAnyTransition<StatePlayerDead>((int)Event.Dead);
    }
}
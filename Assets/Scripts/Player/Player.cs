using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class Player : MonoBehaviour
{
    /// <summary>
    /// 定数
    /// </summary>
    //落下する力
    public const float FALLING_POWER = 50f;
    //デフォルトのスケール
    public Vector3 DEFAULT_SCALE;

    /// <summary>
    /// パブリック変数
    /// </summary>
    //クラスタグ
    public const string Tag = "Player";
    //接地判定(足)
    public GroundCheck ground;
    //接地判定(頭)
    public GroundCheck head;
    //重力
    public float gravity = 10.0f;
    //移動速度
    public float walkPower = 3.0f;
    //ジャンプ力
    public float jumpPower = 7.0f;
    //ジャンプの最大の高さ
    public float maxJumpHeight = 4.0f;
    //落下速度上限
    public float maxFallSpeed = 20.0f;
    //ジャンプの制限時間
    public float jumpLimitTime;

    /// <summary>
    /// プライベート変数
    /// </summary>
    //デフォルトの拡大率
    private Vector3 defaultScale;
    //プレイヤーのリジッドボディ
    private Rigidbody2D rigidBody;
    //移動速度
    private Vector2 speed = Vector2.zero;
    //接地判定用フラグ(足)
    private bool isGround = false;
    //接地判定用フラグ(頭)
    private bool isHead = false;
    //ジャンプしている時間
    private float jumpTime = 0.0f;

    //状態を管理する機械
    private StateMachine<Player> stateMachine;

    private enum Event : int
    {
        Stand,  //立ち止まる
        Walk,   //歩く
        Jump,   //ジャンプ
        Dive,   //落下
        Dead,   //死亡
    }

    private void Start()
    {
        stateMachine = new StateMachine<Player>(this);
        defaultScale = transform.localScale;
        rigidBody = GetComponent<Rigidbody2D>();
        
        //ステート遷移の登録
        RegistStateTransitionMethod();

        //開始は立ち止まった状態から
        stateMachine.Start<StatePlayerStanding>();

    }

    private void Update()
    {
        //状態の更新
        stateMachine.Update();

    }

    private void FixedUpdate()
    {
        //接地判定の取得
        isGround = ground.IsGround();
        isHead = head.IsGround();

        //状態の更新
        stateMachine.FixedUpdate();

        //移動
        rigidBody.velocity = new Vector2(speed.x, speed.y);
    }

    private void RegistStateTransitionMethod()
    {
        //立ち止まった状態から歩く
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerWalking>((int)Event.Walk);
        //立ち止まった状態からジャンプ
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerJumping>((int)Event.Jump);
        //立ち止まった状態から落下
        stateMachine.AddTransition<StatePlayerStanding, StatePlayerDiving>((int)Event.Dive);
        //歩いた状態から立ち止まる
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerStanding>((int)Event.Stand);
        //歩いた状態からジャンプ
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerJumping>((int)Event.Jump);
        //歩いた状態から落下
        stateMachine.AddTransition<StatePlayerWalking, StatePlayerDiving>((int)Event.Dive);
        //ジャンプの状態から立ち止まる(着地)
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerStanding>((int)Event.Stand);
        //ジャンプの状態から落下
        stateMachine.AddTransition<StatePlayerJumping, StatePlayerDiving>((int)Event.Dive);
        //落下した状態から立ち止まる
        stateMachine.AddTransition<StatePlayerDiving, StatePlayerStanding>((int)Event.Stand);

        //プレイヤーが死んだら終了
        stateMachine.AddAnyTransition<StatePlayerDead>((int)Event.Dead);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // 使用するコンポーネント //
    Rigidbody _playerRb;

    // アニメーション関連 //
    Animator _playerAnim;

    // 動きに関する変数 //
    float inputVertical;
    float inputHorizontal;
    [SerializeField, Header("移動速度"),Tooltip("キャラクターの移動速度のためのメンバー変数")] float _speed = 1.0f;
    [SerializeField, Header("ジャンプ力"), Tooltip("キャラクターのジャンプ力のためのメンバー変数")] float _jumpForce = 1.0f;
    [SerializeField ,Header("重力の大きさ（倍率）"), Tooltip("重力の大きさのためのメンバー変数")] float _gravityScale = 1.5f;

    //プレイヤーのステータス関連
    [SerializeField, Header("キャラクター名"), Tooltip("キャラクター名のためのメンバー変数")] string _playerName;
    [Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] public int _playerMaxHp;
    [Header("MaxMP"), Tooltip("MaxMPのためのメンバー変数")] public int _playerMaxMp;
    [Header("現在のHP"), Tooltip("現在のHPのためのメンバー変数")] int _currentPlayerHp;
    [Header("現在のMP"), Tooltip("現在のMPのためのメンバー変数")] int _currentPlayerMp;
    //プレイヤーステータスのプロパティ、外部からは読み取りのみ
    public string PlayerName { get => _playerName; private set => _playerName = value; }
    public int PlayerHP { get => _currentPlayerHp; private set => _currentPlayerHp = value; }
    public int PlayerMP { get => _currentPlayerMp; private set => _currentPlayerMp = value; }

    // フラグ判定 //
    //Skillフラグ
    bool _onSkill1;
    bool _onSkill2;
    bool _onSkill3;
    bool _onSkill4;
    //Skillのプロパティ
    public bool OnSkill1 { get => _onSkill1; set => _onSkill1 = value; }
    public bool OnSkill2 { get => _onSkill2; set => _onSkill2 = value; }
    public bool OnSkill3 { get => _onSkill3; set => _onSkill3 = value; }
    public bool OnSkill4 { get => _onSkill4; set => _onSkill4 = value; }
    //Itemフラグ
    bool _onItem1;
    bool _onItem2;
    bool _onItem3;
    //Itemのプロパティ
    public bool OnItem1 { get => _onItem1; set => _onItem1 = value; }
    public bool OnItem2 { get => _onItem2; set => _onItem2 = value; }
    public bool OnItem3 { get => _onItem3; set => _onItem3 = value; }
    //その他
    [Tooltip("キャラクターが地面に接地しているかのフラグ")] bool _onGround;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isMoving;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //重力変更
        Physics.gravity = new Vector3(0, Physics.gravity.y * _gravityScale, 0);

        //ステータス初期化
        PlayerHP = _playerMaxHp;
        PlayerMP = _playerMaxMp;

        //フラグOn
        _onGround = true;
        _isMoving = true;

        //Skillを使える状態にする
        OnSkill1 = true;
        OnSkill2 = true;
        OnSkill3 = true;
        OnSkill4 = true;

        //Itemを使える状態にする
        OnItem1 = true;
        OnItem2 = true;
        OnItem3 = true;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
        UseSkill();
    }

    /// <summary> キャラクターのジャンプに関する処理 /// </summary>
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            _playerAnim.SetBool("isJumping", true);
            _playerRb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
            _onGround = false;
            _isMoving = false;
        }
    }

    /// <summary> キャラクターの移動に関する処理 /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            inputHorizontal = Input.GetAxisRaw("Horizontal");

            //Walkアニメション
            //水平方向
            if (inputHorizontal < -0.5f || inputHorizontal > 0.5)
                _playerAnim.SetBool("inputHorizontal", true);
            else
                _playerAnim.SetBool("inputHorizontal", false);

            //垂直方向
            if (inputVertical < -0.5f || inputVertical > 0.5)
                _playerAnim.SetBool("inputVertical", true);
            else
                _playerAnim.SetBool("inputVertical", false);

            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            //入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // 移動方向にスピードを掛ける ジャンプがある場合、別途Y軸方向の速度ベクトルを足す
            //transform.Translate(transform.position + moveForward * _speed * Time.deltaTime);

            // キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

        }
    }

    void UseSkill() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && OnSkill1)
        {   
            _playerAnim.Play("Attack1");
        }
    }

    /// <summary> 衝突時の処理 /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            _onGround = true;
            _isMoving = true;
            _playerAnim.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

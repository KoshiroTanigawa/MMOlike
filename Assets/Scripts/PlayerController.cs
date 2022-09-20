using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // 使用するコンポーネント //
    Rigidbody _playerRb;

    // 動きに関する変数 //
    [SerializeField, Header("移動速度"),Tooltip("キャラクターの移動速度のためのメンバー変数")] float _speed = 1.0f;
    [SerializeField, Header("ジャンプ力"), Tooltip("キャラクターのジャンプ力のためのメンバー変数")] float _jumpForce = 1.0f;
    [SerializeField ,Header("重力の大きさ（倍率）"), Tooltip("重力の大きさのためのメンバー変数")] float _gravityScale = 1.5f;

    //プレイヤーのステータス関連
    [SerializeField, Header("キャラクター名"), Tooltip("キャラクター名のためのメンバー変数")] string _playerName;
    [SerializeField, Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] int _playerMaxHp;
    [SerializeField, Header("MaxMP"), Tooltip("MaxMPのためのメンバー変数")] int _playerMaxMp;
    int _currentPlayerHp;
    int _currentPlayerMp;
    public int PlayerHP { get => _currentPlayerHp; set => _currentPlayerHp = value; }
    public int PlayerMP { get => _currentPlayerMp; set => _currentPlayerMp = value; }

    // 
    Quaternion _targetRotation;
    [SerializeField, Header("回転制限")] float _maxRotationSpeed;

    // フラグ判定 //
    [Tooltip("キャラクターが地面に接地しているかのフラグ")] bool _onGround;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isMoving;
    [Tooltip("キャラクターが攻撃可能か判定するフラグ")] bool _isAttack;

    // アニメーション関連 //
    Animator _playerAnim;


    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //ステータス初期化
        PlayerHP = _playerMaxHp;
        PlayerMP = _playerMaxMp;

        //重力変更
        Physics.gravity = new Vector3(0, Physics.gravity.y *_gravityScale, 0);

        //フラグOn
        _onGround = true;
        _isMoving = true;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
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
            float inputVertical = Input.GetAxisRaw("Vertical");
            float inputHorizontal = Input.GetAxisRaw("Horizontal");

            // カメラの方向から、X-Z平面の単位ベクトルを取得
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            //入力値とカメラの向きから、移動方向を決定
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // 移動方向にスピードを掛ける ジャンプがある場合、別途Y軸方向の速度ベクトルを足す
            //_playerRb.velocity = moveForward * _speed   ;

            // キャラクターの向きを進行方向に
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

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
}

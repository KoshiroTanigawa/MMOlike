using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    //フィールド変数
    [Tooltip("キャラクターのRigitbodyのメンバー変数")] Rigidbody _rb;
    [Tooltip("キャラクターのAnimatorのメンバー変数")] Animator _anim;

    [SerializeField] [Header("移動速度")] [Tooltip("キャラクターの移動速度のためのメンバー変数")] float _speed = 1.0f;
    [SerializeField] [Header("ジャンプ力")] [Tooltip("キャラクターのジャンプ力のためのメンバー変数")] float _jumpForce = 1.0f;
    [SerializeField] [Header("重力の大きさ（倍率）")] [Tooltip("重力の大きさのためのメンバー変数")] float _gravityScale = 1.5f;

    [Tooltip("キャラクターが地面に接地しているかのフラグ")] bool _onGround;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isMoving;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isAttack;
    //

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, Physics.gravity.y *_gravityScale, 0);
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
            _anim.SetBool("isJumping", true);
            _rb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
            _onGround = false;
            _isMoving = false;
        }
    }

    /// <summary> キャラクターの移動に関する処理 /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            //Forward
            if (Input.GetKey(KeyCode.W))
                _anim.SetBool("isForward", true);
            if (Input.GetKeyUp(KeyCode.W))
                _anim.SetBool("isForward", false);
            //Backward
            if (Input.GetKey(KeyCode.S))
                _anim.SetBool("isBackward", true);
            if (Input.GetKeyUp(KeyCode.S))
                _anim.SetBool("isBackward", false);
            //Right
            if (Input.GetKey(KeyCode.D))
                _anim.SetBool("isRight", true);
            if (Input.GetKeyUp(KeyCode.D))
                _anim.SetBool("isRight", false);
            //Left
            if (Input.GetKey(KeyCode.A))
                _anim.SetBool("isLeft", true);
            if (Input.GetKeyUp(KeyCode.A))
                _anim.SetBool("isLeft", false);

        }
    }

    /// <summary> 衝突時の処理 /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            _onGround = true;
            _isMoving = true;
            _anim.SetBool("isJumping", false);
        }
    }
}

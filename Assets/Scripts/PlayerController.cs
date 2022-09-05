using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //フィールド変数
    [Tooltip("キャラクターのRigitbodyのメンバー変数")] Rigidbody _rb;

    [SerializeField] [Header("移動速度")] [Tooltip("キャラクターの移動速度のためのメンバー変数")] float _speed;
    [SerializeField] [Header("ジャンプ力")] [Tooltip("キャラクターのジャンプ力のためのメンバー変数")] float _jumpForce;
    [SerializeField] [Header("重力の大きさ（倍率）")] [Tooltip("重力の大きさのためのメンバー変数")] float _gravityScale;

    [Tooltip("キャラクターが地面に接地しているかのフラグ")] bool _onGround;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isMoving;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isAttack;
    //

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0,_gravityScale,0);
        _onGround = true;
        _isMoving = true;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    /// <summary> キャラクターのジャンプに関する処理 /// </summary>
    private void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
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
            //水平方向
            float inputHorizontal = Input.GetAxis("Horizontal");
            if (inputHorizontal == 1)
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            if (inputHorizontal == -1)
                transform.Translate(Vector3.left * _speed * Time.deltaTime);

            //垂直方向
            float inputVertical = Input.GetAxis("Vertical");
            if (inputVertical == 1)
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            if (inputVertical == -1)
                transform.Translate(Vector3.back * _speed * Time.deltaTime);
        }
    }

    /// <summary> 衝突時の処理 /// </summary>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            _onGround = true;
            _isMoving = true;
        }
    }
}

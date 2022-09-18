using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    //�t�B�[���h�ϐ�
    [Tooltip("�L�����N�^�[��Rigitbody�̃����o�[�ϐ�")] Rigidbody _rb;
    [Tooltip("�L�����N�^�[��Animator�̃����o�[�ϐ�")] Animator _anim;

    [SerializeField] [Header("�ړ����x")] [Tooltip("�L�����N�^�[�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;
    [SerializeField] [Header("�W�����v��")] [Tooltip("�L�����N�^�[�̃W�����v�͂̂��߂̃����o�[�ϐ�")] float _jumpForce = 1.0f;
    [SerializeField] [Header("�d�͂̑傫���i�{���j")] [Tooltip("�d�͂̑傫���̂��߂̃����o�[�ϐ�")] float _gravityScale = 1.5f;

    [Tooltip("�L�����N�^�[���n�ʂɐڒn���Ă��邩�̃t���O")] bool _onGround;
    [Tooltip("�L�����N�^�[���ړ��\�����肷��t���O")] bool _isMoving;
    [Tooltip("�L�����N�^�[���U���\�����肷��t���O")] bool _isAttack;
    //

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        //�d�͕ύX
        Physics.gravity = new Vector3(0, Physics.gravity.y *_gravityScale, 0);
        //�t���OOn
        _onGround = true;
        _isMoving = true;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
    }

    /// <summary> �L�����N�^�[�̃W�����v�Ɋւ��鏈�� /// </summary>
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

    /// <summary> �L�����N�^�[�̈ړ��Ɋւ��鏈�� /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            float inputVertical = Input.GetAxisRaw("Vertical");
            float inputHorizontal = Input.GetAxisRaw("Horizontal");
            Vector2 direction = new Vector2(inputHorizontal, inputVertical).normalized;

        }
    }

    /// <summary> �Փˎ��̏��� /// </summary>
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

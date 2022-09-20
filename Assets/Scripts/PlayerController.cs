using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    Rigidbody _playerRb;

    // �����Ɋւ���ϐ� //
    [SerializeField, Header("�ړ����x"),Tooltip("�L�����N�^�[�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;
    [SerializeField, Header("�W�����v��"), Tooltip("�L�����N�^�[�̃W�����v�͂̂��߂̃����o�[�ϐ�")] float _jumpForce = 1.0f;
    [SerializeField ,Header("�d�͂̑傫���i�{���j"), Tooltip("�d�͂̑傫���̂��߂̃����o�[�ϐ�")] float _gravityScale = 1.5f;

    //�v���C���[�̃X�e�[�^�X�֘A
    [SerializeField, Header("�L�����N�^�[��"), Tooltip("�L�����N�^�[���̂��߂̃����o�[�ϐ�")] string _playerName;
    [SerializeField, Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] int _playerMaxHp;
    [SerializeField, Header("MaxMP"), Tooltip("MaxMP�̂��߂̃����o�[�ϐ�")] int _playerMaxMp;
    int _currentPlayerHp;
    int _currentPlayerMp;
    public int PlayerHP { get => _currentPlayerHp; set => _currentPlayerHp = value; }
    public int PlayerMP { get => _currentPlayerMp; set => _currentPlayerMp = value; }

    // 
    Quaternion _targetRotation;
    [SerializeField, Header("��]����")] float _maxRotationSpeed;

    // �t���O���� //
    [Tooltip("�L�����N�^�[���n�ʂɐڒn���Ă��邩�̃t���O")] bool _onGround;
    [Tooltip("�L�����N�^�[���ړ��\�����肷��t���O")] bool _isMoving;
    [Tooltip("�L�����N�^�[���U���\�����肷��t���O")] bool _isAttack;

    // �A�j���[�V�����֘A //
    Animator _playerAnim;


    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //�X�e�[�^�X������
        PlayerHP = _playerMaxHp;
        PlayerMP = _playerMaxMp;

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
            _playerAnim.SetBool("isJumping", true);
            _playerRb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
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

            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            //���͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // �ړ������ɃX�s�[�h���|���� �W�����v������ꍇ�A�ʓrY�������̑��x�x�N�g���𑫂�
            //_playerRb.velocity = moveForward * _speed   ;

            // �L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

        }
    }

    /// <summary> �Փˎ��̏��� /// </summary>
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

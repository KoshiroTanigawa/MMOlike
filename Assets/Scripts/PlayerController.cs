using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    Rigidbody _playerRb;

    // �A�j���[�V�����֘A //
    Animator _playerAnim;

    // �����Ɋւ���ϐ� //
    float inputVertical;
    float inputHorizontal;
    [SerializeField, Header("�ړ����x"),Tooltip("�L�����N�^�[�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;
    [SerializeField, Header("�W�����v��"), Tooltip("�L�����N�^�[�̃W�����v�͂̂��߂̃����o�[�ϐ�")] float _jumpForce = 1.0f;
    [SerializeField ,Header("�d�͂̑傫���i�{���j"), Tooltip("�d�͂̑傫���̂��߂̃����o�[�ϐ�")] float _gravityScale = 1.5f;

    //�v���C���[�̃X�e�[�^�X�֘A
    [SerializeField, Header("�L�����N�^�[��"), Tooltip("�L�����N�^�[���̂��߂̃����o�[�ϐ�")] string _playerName;
    [Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] public int _playerMaxHp;
    [Header("MaxMP"), Tooltip("MaxMP�̂��߂̃����o�[�ϐ�")] public int _playerMaxMp;
    [Header("���݂�HP"), Tooltip("���݂�HP�̂��߂̃����o�[�ϐ�")] int _currentPlayerHp;
    [Header("���݂�MP"), Tooltip("���݂�MP�̂��߂̃����o�[�ϐ�")] int _currentPlayerMp;
    //�v���C���[�X�e�[�^�X�̃v���p�e�B�A�O������͓ǂݎ��̂�
    public string PlayerName { get => _playerName; private set => _playerName = value; }
    public int PlayerHP { get => _currentPlayerHp; private set => _currentPlayerHp = value; }
    public int PlayerMP { get => _currentPlayerMp; private set => _currentPlayerMp = value; }

    // �t���O���� //
    //Skill�t���O
    bool _onSkill1;
    bool _onSkill2;
    bool _onSkill3;
    bool _onSkill4;
    //Skill�̃v���p�e�B
    public bool OnSkill1 { get => _onSkill1; set => _onSkill1 = value; }
    public bool OnSkill2 { get => _onSkill2; set => _onSkill2 = value; }
    public bool OnSkill3 { get => _onSkill3; set => _onSkill3 = value; }
    public bool OnSkill4 { get => _onSkill4; set => _onSkill4 = value; }
    //Item�t���O
    bool _onItem1;
    bool _onItem2;
    bool _onItem3;
    //Item�̃v���p�e�B
    public bool OnItem1 { get => _onItem1; set => _onItem1 = value; }
    public bool OnItem2 { get => _onItem2; set => _onItem2 = value; }
    public bool OnItem3 { get => _onItem3; set => _onItem3 = value; }
    //���̑�
    [Tooltip("�L�����N�^�[���n�ʂɐڒn���Ă��邩�̃t���O")] bool _onGround;
    [Tooltip("�L�����N�^�[���ړ��\�����肷��t���O")] bool _isMoving;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //�d�͕ύX
        Physics.gravity = new Vector3(0, Physics.gravity.y * _gravityScale, 0);

        //�X�e�[�^�X������
        PlayerHP = _playerMaxHp;
        PlayerMP = _playerMaxMp;

        //�t���OOn
        _onGround = true;
        _isMoving = true;

        //Skill���g�����Ԃɂ���
        OnSkill1 = true;
        OnSkill2 = true;
        OnSkill3 = true;
        OnSkill4 = true;

        //Item���g�����Ԃɂ���
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
            inputVertical = Input.GetAxisRaw("Vertical");
            inputHorizontal = Input.GetAxisRaw("Horizontal");

            //Walk�A�j���V����
            //��������
            if (inputHorizontal < -0.5f || inputHorizontal > 0.5)
                _playerAnim.SetBool("inputHorizontal", true);
            else
                _playerAnim.SetBool("inputHorizontal", false);

            //��������
            if (inputVertical < -0.5f || inputVertical > 0.5)
                _playerAnim.SetBool("inputVertical", true);
            else
                _playerAnim.SetBool("inputVertical", false);

            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            //���͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * inputVertical + Camera.main.transform.right * inputHorizontal;

            // �ړ������ɃX�s�[�h���|���� �W�����v������ꍇ�A�ʓrY�������̑��x�x�N�g���𑫂�
            //transform.Translate(transform.position + moveForward * _speed * Time.deltaTime);

            // �L�����N�^�[�̌�����i�s������
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

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

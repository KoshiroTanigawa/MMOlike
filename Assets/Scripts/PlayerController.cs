using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    Rigidbody _playerRb;
    [SerializeField, Header("���̋O��")] GameObject _particleSword;
    [SerializeField] Collider _swordCollider;

    //�A�j���[�V�����֘A
    Animator _playerAnim;
    PlayerState _state;
    enum PlayerState
    {
        Idol,
        Run,
        Jump,
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        GetUp,
        Fall,
        Hit
    }

    // �����Ɋւ���ϐ� //
    float inputVertical;
    float inputHorizontal;
    [SerializeField, Header("�ړ����x"),Tooltip("�L�����N�^�[�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;
    [SerializeField, Header("�W�����v��"), Tooltip("�L�����N�^�[�̃W�����v�͂̂��߂̃����o�[�ϐ�")] float _jumpForce = 1.0f;
    [SerializeField ,Header("�d�͂̑傫���i�{���j"), Tooltip("�d�͂̑傫���̂��߂̃����o�[�ϐ�")] float _gravityScale = 1.5f;
    [SerializeField, Header("�^�[�����x�i�{���j"), Tooltip("�^�[�����x�̂��߂̃����o�[�ϐ�")] float _turn = 2f;

    //�v���C���[�̃X�e�[�^�X�֘A
    [SerializeField, Header("�L�����N�^�[��"), Tooltip("�L�����N�^�[���̂��߂̃����o�[�ϐ�")] string _playerName;
    [Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] public int _playerMaxHp;
    [Header("MaxMP"), Tooltip("MaxMP�̂��߂̃����o�[�ϐ�")] public int _playerMaxMp;
    [Header("���݂�HP"), Tooltip("���݂�HP�̂��߂̃����o�[�ϐ�")] int _currentPlayerHp;
    [Header("���݂�MP"), Tooltip("���݂�MP�̂��߂̃����o�[�ϐ�")] int _currentPlayerMp;
    //[Header("���݂�Attack"), Tooltip("���݂�Attack�̂��߂̃����o�[�ϐ�")] int _currentPlayerAttack;
    //�v���C���[�X�e�[�^�X�̃v���p�e�B�A�O������͓ǂݎ��̂�
    public string PlayerName { get => _playerName; private set => _playerName = value; }
    public int PlayerHP
    {
        get => _currentPlayerHp;

        private set 
        {
            _currentPlayerHp = value;
            if (_currentPlayerHp > _playerMaxHp)
                _currentPlayerHp = _playerMaxHp;
            else if (_currentPlayerHp < 0)
                _currentPlayerHp = 0;
        }
    }
    public int PlayerMP 
    { 
        get => _currentPlayerMp;

        private set 
        {
            _currentPlayerMp = value;
            if (_currentPlayerMp > _playerMaxMp)
                _currentPlayerMp = _playerMaxMp;
            else if (_currentPlayerMp < 0)
                _currentPlayerMp = 0;
        }
    }

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

    //Skill&Item�֘A
    //Skill&Item�̃��L���X�g���Ԃ̂��߂̕ϐ�
    int _recastTime;
    //�X�L���̃_���[�W�{���̕ϐ�
    float _skillDamageScale = 1.0f;
    //Skill&Item�̃��L���X�g���Ԃ̂��߂̕ϐ�
    [SerializeField, Header("�X�L��1���L���X�g����")] int _rt1;
    [SerializeField, Header("�X�L��2���L���X�g����")] int _rt2;
    [SerializeField, Header("�X�L��3���L���X�g����")] int _rt3;
    [SerializeField, Header("�X�L��4���L���X�g����")] int _rt4;
    [SerializeField, Header("�A�C�e��1���L���X�g����")] int _rt5;
    [SerializeField, Header("�A�C�e��2���L���X�g����")] int _rt6;
    [SerializeField, Header("�A�C�e��3���L���X�g����")] int _rt7;
    //RT�v���p�e�B
    public int RT1 { get => _rt1; private set => _rt1 = value; }
    public int RT2 { get => _rt2; private set => _rt2 = value; }
    public int RT3 { get => _rt3; private set => _rt3 = value; }
    public int RT4 { get => _rt4; private set => _rt4 = value; }
    public int RT5 { get => _rt5; private set => _rt5 = value; }
    public int RT6 { get => _rt6; private set => _rt6 = value; }
    public int RT7 { get => _rt7; private set => _rt7 = value; }

    //���̑�
    [Tooltip("�L�����N�^�[���n�ʂɐڒn���Ă��邩�̃t���O")] bool _onGround;
    [Tooltip("�L�����N�^�[���ړ��\�����肷��t���O")] bool _isMoving;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //�A�j���[�V����������
        _state = PlayerState.Idol;

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

        //���֘A
        //���̋O��Off
        _particleSword.SetActive(false);
        _swordCollider.enabled = false;
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
        UseSkill();
        UseItem();
    }

    /// <summary> 
    /// ///�L�����N�^�[�̃W�����v�Ɋւ��鏈�� 
    /// /// </summary>
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            _state = PlayerState.Jump;
            _playerRb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
            _onGround = false;
            _isMoving = false;
        }
    }

    /// <summary> 
    /// ///�L�����N�^�[�̈ړ��Ɋւ��鏈�� 
    /// /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            inputHorizontal = Input.GetAxisRaw("Horizontal");

            Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _playerRb.velocity = new Vector3(inputHorizontal, 0, inputVertical) * _speed;
            _playerAnim.SetFloat("Run", _playerRb.velocity.magnitude);   //�����A�j���[�V�����ɐ؂�ւ���

            if (target_dir.magnitude > 0.1)
            {
                //�L�[�����������]��
                Quaternion rotation = Quaternion.LookRotation(target_dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _turn);
            }

        }
    }

    /// <summary>
    /// Skill�g�p���̏���
    /// </summary>
    void UseSkill() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && OnSkill1)
        {
            Skill("Skill1", 50, false, 0, RT1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && OnSkill2)
        {
            Skill("Skill2", 100, false, 0, RT2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && OnSkill3)
        {
            Skill("Skill3", 150, false, 0, RT3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && OnSkill4)
        {
            Skill("Skill4", 150, true, 30, RT4);
        }
    }

    /// <summary>
    /// �X�L���g�p���̏���
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="damage"></param>
    /// /// <param name="heal"></param>
    /// <param name="recastTime"></param>
    void Skill(string skillName, int damage, bool healFlag, int heal, int recastTime)
    {
        //����Collider On
        _swordCollider.enabled = true;

        //���L���X�g���ԃZ�b�g
        _recastTime = recastTime;
        //�_���[�W�l�Z�b�g
        damage = Mathf.FloorToInt(damage * _skillDamageScale);

        //Hp�񕜂̏���
        if (healFlag)
        {
            for (var i = 0; i < 3; i++)
            {
                PlayerHP = PlayerHP + heal;
                StartCoroutine("HealWaitTime");
            }
        }

        //�A�j���[�V���� On
        if (skillName == "Skill1") 
        {
            _state = PlayerState.Skill1;
        }
        //�A�j���[�V���� On
        if (skillName == "Skill2")
        {
            _state = PlayerState.Skill2;
        }
        //�A�j���[�V���� On
        if (skillName == "Skill3")
        {
            _state = PlayerState.Skill3;
        }
        //�A�j���[�V���� On
        if (skillName == "Skill4")
        {
            _state = PlayerState.Skill4;
        }

    }
        


    /// <summary>
    /// Item�g�p���̏���
    /// </summary>
    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && OnItem1)
        {
            Item(true, false, false, 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.E) && OnItem2)
        {
            Item(false, true, false, 100);
        }
        if (Input.GetKeyDown(KeyCode.R) && OnItem3)
        {
            Item(false, false, true, 100);
        }
    }

    /// <summary>
    /// Item�̌���
    /// </summary>
    /// <param name="damageUp"></param>
    /// <param name="hp"></param>
    /// <param name="mp"></param>
    /// <param name="num"></param>
    void Item(bool damageUp, bool hp, bool mp, float num) 
    {
        if (damageUp)
        {
            _skillDamageScale = num;
        }
        else if (hp)
        {
            PlayerHP = PlayerHP + (int)num;
        }
        else if (mp)
        {
            PlayerMP = PlayerMP + (int)num;
        }
    }

    /// <summary> 
    /// ///�Փˎ��̏��� 
    /// /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            _onGround = true;
            _isMoving = true;
            _playerAnim.SetBool("isJumping", false);
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            _playerAnim.SetTrigger("DamageHeavy");       //�v���C���[�̌�������������_���[�W�A�j���[�V��������
        }
    }
    */

    /// <summary>
    /// �p���񕜂Ɏg���҂����Ԃ̃R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator HealWaitTime()
    {
        yield return new WaitForSeconds(2);
    }


    IEnumerator AnimationWaitTime() 
    {
        yield return new WaitForSeconds(_recastTime);
    }
}

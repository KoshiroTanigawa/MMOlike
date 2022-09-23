using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    public Rigidbody _playerRb;
    [SerializeField, Header("���̋O��")] GameObject _particleSword;
    [SerializeField] GameObject _swordCollider;

    //�A�j���[�V�����֘A
    Animator _playerAnim;

    //�G
    EnemyController _enemy;

    // �����Ɋւ���ϐ� //
    float _inputVertical;
    float _inputHorizontal;
    [SerializeField, Header("�ړ����x"),Tooltip("�L�����N�^�[�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed;
    [SerializeField, Header("�W�����v��"), Tooltip("�L�����N�^�[�̃W�����v�͂̂��߂̃����o�[�ϐ�")] float _jumpForce = 1.0f;
    [SerializeField ,Header("�d�͂̑傫���i�{���j"), Tooltip("�d�͂̑傫���̂��߂̃����o�[�ϐ�")] float _gravityScale = 1.5f;

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

         set 
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

         set 
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
    //
    public bool _gcd;
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
    //�_���[�W�p�ϐ�
    int _playerDamage;
    //damage�v���p�e�B
    public int PlayerDamage { get => _playerDamage;  set => _playerDamage = value; }
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
    //�X�L���p�R���[�`��
    IEnumerator _corutine;

    //���̑�
    [Tooltip("�L�����N�^�[���n�ʂɐڒn���Ă��邩�̃t���O")] bool _onGround;
    [Tooltip("�L�����N�^�[���ړ��\�����肷��t���O")] bool _isMoving;
    [Tooltip("�L�����N�^�[��Jump�\�����肷��t���O")] bool _isJumping;

    //Audio�֘A
    AudioSource _audio;
    [SerializeField] AudioClip _audioClip1;
    [SerializeField] AudioClip _audioClip2;
    [SerializeField] AudioClip _audioClip3;
    [SerializeField] AudioClip _audioClip4;
    [SerializeField] AudioClip _audioClip5;
    [SerializeField] AudioClip _audioClip6;
    [SerializeField] AudioClip _audioClip7;
    [SerializeField] AudioClip _audioClip8;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _enemy = GameObject.Find("Enemy").GetComponent<EnemyController>();
        _audio = gameObject.AddComponent<AudioSource>();

        //�d�͕ύX
        Physics.gravity = new Vector3(0, Physics.gravity.y * _gravityScale, 0);

        //�X�e�[�^�X������
        PlayerHP = _playerMaxHp;
        PlayerMP = _playerMaxMp;

        //�t���OOn
        _onGround = true;
        _isMoving = true;
        _isJumping = true;

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
        _swordCollider.SetActive(false);
    }

    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }

    void Update()
    {
        _inputVertical = Input.GetAxisRaw("Vertical");
        _inputHorizontal = Input.GetAxisRaw("Horizontal");
        
        UseSkill();
        UseItem();
        PlayerDie();
    }

    /// <summary> 
    /// ///�L�����N�^�[�̃W�����v�Ɋւ��鏈�� 
    /// /// </summary>
    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround && _isJumping)
        {
            _playerAnim.SetTrigger("Jump");
            _playerRb.AddForce(0, _jumpForce, 0, ForceMode.Impulse);
            _onGround = false;
            _isMoving = false;
            _isJumping = false;
        }
    }

    /// <summary> 
    /// ///�L�����N�^�[�̈ړ��Ɋւ��鏈�� 
    /// /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

            // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
            Vector3 moveForward = cameraForward * _inputVertical + Camera.main.transform.right * _inputHorizontal;

            // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
            _playerRb.velocity = moveForward * _speed + new Vector3(0, _playerRb.velocity.y, 0);

            // �L�����N�^�[�̌�����i�s������
            if (moveForward != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(moveForward);
            }

            _playerAnim.SetFloat("Run", _playerRb.velocity.magnitude);
        }
    }

    /// <summary>
    /// Skill�g�p���̏���
    /// </summary>
    void UseSkill() 
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && OnSkill1)
        {
            Skill("Skill1", 50, false, 0, RT1, 0);
            _audio.PlayOneShot(_audioClip1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && OnSkill2)
        {
            Skill("Skill2", 100, false, 0, RT2, 0);
            _audio.PlayOneShot(_audioClip2);
            StartCoroutine("AudioWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && OnSkill3)
        {
            Skill("Skill3", 150, false, 0, RT3, 0);
            _audio.PlayOneShot(_audioClip3);
            StartCoroutine("AudioWait");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && OnSkill4)
        {
            Skill("Skill4", 0, true, 30, RT4, 30);
            _audio.PlayOneShot(_audioClip4);
        }
    }

    /// <summary>
    /// �X�L���g�p���̏���
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="damage"></param>
    /// /// <param name="heal"></param>
    /// <param name="recastTime"></param>
    void Skill(string skillName, int damage, bool healFlag, int heal, int recastTime, int mp)
    {
        //����Collider On
        _swordCollider.SetActive(true);

        //���̋O�� On
        _particleSword.SetActive(true);

        //���L���X�g���ԃZ�b�g
        _recastTime = recastTime;
        //�_���[�W�l�Z�b�g
        damage = Mathf.FloorToInt(damage * _skillDamageScale);
        PlayerDamage = damage;

        //Hp�񕜂̏���
        if (healFlag)
        {
            _corutine = HealWaitTime();

            PlayerMP -= mp;
            PlayerHP += heal;
            StartCoroutine(_corutine);
            PlayerHP += heal;
            StartCoroutine(_corutine);
            PlayerHP += heal;

            /*
            for (var i = 0; i < 2; i++)
            {
                StartCoroutine(_corutine);
                PlayerHP += heal;
            }
            */
        }

        //�A�j���[�V���� On
        _playerAnim.SetTrigger(skillName);

        StartCoroutine("WaitCollideroff");
        StartCoroutine("WaitTime");
    }

    /// <summary>
    /// Item�g�p���̏���
    /// </summary>
    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && OnItem1)
        {
            Item(true, false, false, 1.5f);
            _audio.PlayOneShot(_audioClip5);

        }
        if (Input.GetKeyDown(KeyCode.E) && OnItem2)
        {
            Item(false, true, false, 200);
            _audio.PlayOneShot(_audioClip6);
        }
        if (Input.GetKeyDown(KeyCode.R) && OnItem3)
        {
            Item(false, false, true, 150);
            _audio.PlayOneShot(_audioClip7);
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
            StartCoroutine("PowarUpItem");
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

     public bool PlayerDie() 
    {
        if (PlayerHP == 0)
        {
            _isMoving = false;
            _playerAnim.SetBool("isDie", true);
            _playerAnim.SetTrigger("Fall");
            return true;
        }
        else
            return false;
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

            StartCoroutine("JumpWaitTime");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemySword") 
        {
            Debug.Log("Hit to Player");
            PlayerHP -= _enemy.EnemyDamage;
            _audio.PlayOneShot(_audioClip8);
        }
    }

    /// <summary>
    /// �p���񕜂Ɏg���҂����Ԃ̃R���[�`��
    /// </summary>
    /// <returns></returns>
    IEnumerator HealWaitTime()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("2�b�҂���");
    }

    IEnumerator JumpWaitTime()
    {
        yield return new WaitForSeconds(0.5f);
        _isJumping = true;
    }

    IEnumerator WaitCollideroff() 
    {
        yield return new WaitForSeconds(0.5f);
        //����Collider Off
        _swordCollider.SetActive(false);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1.5f);
        /*
        //����Collider Off
        _swordCollider.SetActive(false);
        */

        //���̋O�� Off
        _particleSword.SetActive(false);
    }

    IEnumerator PowarUpItem() 
    {
        yield return new WaitForSeconds(10);
        _skillDamageScale = 1;
    }

    IEnumerator AudioWait()
    {
        yield return new WaitForSeconds(1.1f);
        Debug.Log("1.1�b�҂���");
        _audio.PlayOneShot(_audioClip1);
    }

}

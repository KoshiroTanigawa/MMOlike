using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // 使用するコンポーネント //
    Rigidbody _playerRb;
    [SerializeField, Header("剣の軌道")] GameObject _particleSword;
    [SerializeField] Collider _swordCollider;

    //アニメーション関連
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

    // 動きに関する変数 //
    float inputVertical;
    float inputHorizontal;
    [SerializeField, Header("移動速度"),Tooltip("キャラクターの移動速度のためのメンバー変数")] float _speed = 1.0f;
    [SerializeField, Header("ジャンプ力"), Tooltip("キャラクターのジャンプ力のためのメンバー変数")] float _jumpForce = 1.0f;
    [SerializeField ,Header("重力の大きさ（倍率）"), Tooltip("重力の大きさのためのメンバー変数")] float _gravityScale = 1.5f;
    [SerializeField, Header("ターン速度（倍率）"), Tooltip("ターン速度のためのメンバー変数")] float _turn = 2f;

    //プレイヤーのステータス関連
    [SerializeField, Header("キャラクター名"), Tooltip("キャラクター名のためのメンバー変数")] string _playerName;
    [Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] public int _playerMaxHp;
    [Header("MaxMP"), Tooltip("MaxMPのためのメンバー変数")] public int _playerMaxMp;
    [Header("現在のHP"), Tooltip("現在のHPのためのメンバー変数")] int _currentPlayerHp;
    [Header("現在のMP"), Tooltip("現在のMPのためのメンバー変数")] int _currentPlayerMp;
    //[Header("現在のAttack"), Tooltip("現在のAttackのためのメンバー変数")] int _currentPlayerAttack;
    //プレイヤーステータスのプロパティ、外部からは読み取りのみ
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

    //Skill&Item関連
    //Skill&Itemのリキャスト時間のための変数
    int _recastTime;
    //スキルのダメージ倍率の変数
    float _skillDamageScale = 1.0f;
    //Skill&Itemのリキャスト時間のための変数
    [SerializeField, Header("スキル1リキャスト時間")] int _rt1;
    [SerializeField, Header("スキル2リキャスト時間")] int _rt2;
    [SerializeField, Header("スキル3リキャスト時間")] int _rt3;
    [SerializeField, Header("スキル4リキャスト時間")] int _rt4;
    [SerializeField, Header("アイテム1リキャスト時間")] int _rt5;
    [SerializeField, Header("アイテム2リキャスト時間")] int _rt6;
    [SerializeField, Header("アイテム3リキャスト時間")] int _rt7;
    //RTプロパティ
    public int RT1 { get => _rt1; private set => _rt1 = value; }
    public int RT2 { get => _rt2; private set => _rt2 = value; }
    public int RT3 { get => _rt3; private set => _rt3 = value; }
    public int RT4 { get => _rt4; private set => _rt4 = value; }
    public int RT5 { get => _rt5; private set => _rt5 = value; }
    public int RT6 { get => _rt6; private set => _rt6 = value; }
    public int RT7 { get => _rt7; private set => _rt7 = value; }

    //その他
    [Tooltip("キャラクターが地面に接地しているかのフラグ")] bool _onGround;
    [Tooltip("キャラクターが移動可能か判定するフラグ")] bool _isMoving;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();

        //アニメーション初期化
        _state = PlayerState.Idol;

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

        //剣関連
        //剣の軌道Off
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
    /// ///キャラクターのジャンプに関する処理 
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
    /// ///キャラクターの移動に関する処理 
    /// /// </summary>
    void PlayerMove()
    {
        if (_isMoving) 
        {
            inputVertical = Input.GetAxisRaw("Vertical");
            inputHorizontal = Input.GetAxisRaw("Horizontal");

            Vector3 target_dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _playerRb.velocity = new Vector3(inputHorizontal, 0, inputVertical) * _speed;
            _playerAnim.SetFloat("Run", _playerRb.velocity.magnitude);   //歩くアニメーションに切り替える

            if (target_dir.magnitude > 0.1)
            {
                //キーを押し方向転換
                Quaternion rotation = Quaternion.LookRotation(target_dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _turn);
            }

        }
    }

    /// <summary>
    /// Skill使用時の処理
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
    /// スキル使用時の処理
    /// </summary>
    /// <param name="skillName"></param>
    /// <param name="damage"></param>
    /// /// <param name="heal"></param>
    /// <param name="recastTime"></param>
    void Skill(string skillName, int damage, bool healFlag, int heal, int recastTime)
    {
        //剣のCollider On
        _swordCollider.enabled = true;

        //リキャスト時間セット
        _recastTime = recastTime;
        //ダメージ値セット
        damage = Mathf.FloorToInt(damage * _skillDamageScale);

        //Hp回復の処理
        if (healFlag)
        {
            for (var i = 0; i < 3; i++)
            {
                PlayerHP = PlayerHP + heal;
                StartCoroutine("HealWaitTime");
            }
        }

        //アニメーション On
        if (skillName == "Skill1") 
        {
            _state = PlayerState.Skill1;
        }
        //アニメーション On
        if (skillName == "Skill2")
        {
            _state = PlayerState.Skill2;
        }
        //アニメーション On
        if (skillName == "Skill3")
        {
            _state = PlayerState.Skill3;
        }
        //アニメーション On
        if (skillName == "Skill4")
        {
            _state = PlayerState.Skill4;
        }

    }
        


    /// <summary>
    /// Item使用時の処理
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
    /// Itemの効果
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
    /// ///衝突時の処理 
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
            _playerAnim.SetTrigger("DamageHeavy");       //プレイヤーの剣が当たったらダメージアニメーション発生
        }
    }
    */

    /// <summary>
    /// 継続回復に使う待ち時間のコルーチン
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // 使用するコンポーネント //
    public Rigidbody _enemyRb;
    [SerializeField, Header("剣の軌道")] GameObject _particleSword;
    [SerializeField, Header("剣のCollider")] GameObject _enemySwordCollider;

    //player
    PlayerController _player;

    //Attack関連
    float _rand;

    // 動きに関する変数 //
    [SerializeField, Header("移動速度"), Tooltip("Enemyの移動速度のためのメンバー変数")] float _speed = 1.0f;

    //enemyのステータス関連
    [SerializeField, Header("Enemy名"), Tooltip("Enemy名のためのメンバー変数")] string _enemyName;
    [Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] public int _enemyMaxHp;
    [SerializeField] int _enemyDamage;
    int _currentEnemyHp;
    //Enemyステータスのプロパティ、外部からは読み取りのみ
    public string EnemyName { get => _enemyName;  set => _enemyName = value; }
    public int EnemyHP
    {
        get => _currentEnemyHp;

        private set
        {
            _currentEnemyHp = value;
            if (_currentEnemyHp > _enemyMaxHp)
                _currentEnemyHp = _enemyMaxHp;
            else if (_currentEnemyHp < 0)
                _currentEnemyHp = 0;
        }
    }
    //damageプロパティ
    public int EnemyDamage { get => _enemyDamage;  set => _enemyDamage = value; }
    // アニメーション関連 //
    Animator _enemyAnim;

    //NavMeshAgent
    [SerializeField]Transform target;
    NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _enemyAnim = GetComponent<Animator>();
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();

        //

        //速度セット
        _agent.speed = _speed;
        _agent.stoppingDistance = 1;

        //ステータス初期化
        EnemyHP = _enemyMaxHp;

        //剣の軌道Off
        _particleSword.SetActive(false);
    }

    private void FixedUpdate()
    {
        EnemyAttack();
    }

    // Update is called once per frame
    void Update()
    {
        //InputEnemySpeed();
        //EnemyAttack();
        //Nav 目的地更新
        _agent.SetDestination(target.position);
    }

    void InputEnemySpeed() 
    {
        float inputH = _enemyRb.velocity.x;
        float inputV = _enemyRb.velocity.z;

        _enemyAnim.SetFloat("speedh", inputH);
        _enemyAnim.SetFloat("speedv", inputV);
    }

    void EnemyAttack() 
    {
        StartCoroutine("AttackWait");

        if (_rand > 69)
        {
            //剣の軌道On
            _particleSword.SetActive(true);
            //剣のCollider On
            _enemySwordCollider.SetActive(true);

            _enemyAnim.SetTrigger("Attack1");

            StartCoroutine("WaitTime1");
        }
        else if (_rand < 71 && _rand > 49)
        {
            //剣の軌道On
            _particleSword.SetActive(true);
            //剣のCollider On
            _enemySwordCollider.SetActive(true);

            _enemyAnim.SetTrigger("Attack2");

            StartCoroutine("WaitTime2");

        }
        else 
        {
            return;
        }
    }

    public bool EnemyDie()
    {
        if (EnemyHP == 0) 
        {
            _enemyAnim.SetTrigger("Fall1");
            return true;
        }
            
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="PlayerSword") 
        {
            Debug.Log("HIt to Enemy");
            _enemyAnim.SetTrigger("Hit1");
            EnemyHP -= _player.PlayerDamage;
        }
    }

    IEnumerator AttackWait() 
    {
        yield return new WaitForSeconds(5);
        float rand = Random.Range(1, 100);
        _rand = rand;
    }

    IEnumerator WaitTime1()
    {
        yield return new WaitForSeconds(1.5f);
        
        //剣の軌跡 Off
        _particleSword.SetActive(false);
        //剣のCollider Off
        _enemySwordCollider.SetActive(false);

        _enemyAnim.ResetTrigger("Attack1");
    }

    IEnumerator WaitTime2()
    {
        yield return new WaitForSeconds(1.5f);

        //剣の軌跡 Off
        _particleSword.SetActive(false);
        //剣のCollider Off
        _enemySwordCollider.SetActive(false);

        _enemyAnim.ResetTrigger("Attack2");
    }
}

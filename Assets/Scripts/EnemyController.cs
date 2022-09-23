using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    public Rigidbody _enemyRb;
    [SerializeField, Header("���̋O��")] GameObject _particleSword;
    [SerializeField, Header("����Collider")] GameObject _enemySwordCollider;

    //player
    PlayerController _player;

    //Attack�֘A
    float _rand;

    // �����Ɋւ���ϐ� //
    [SerializeField, Header("�ړ����x"), Tooltip("Enemy�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;

    //enemy�̃X�e�[�^�X�֘A
    [SerializeField, Header("Enemy��"), Tooltip("Enemy���̂��߂̃����o�[�ϐ�")] string _enemyName;
    [Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] public int _enemyMaxHp;
    [SerializeField] int _enemyDamage;
    int _currentEnemyHp;
    //Enemy�X�e�[�^�X�̃v���p�e�B�A�O������͓ǂݎ��̂�
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
    //damage�v���p�e�B
    public int EnemyDamage { get => _enemyDamage;  set => _enemyDamage = value; }
    // �A�j���[�V�����֘A //
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

        //���x�Z�b�g
        _agent.speed = _speed;
        _agent.stoppingDistance = 1;

        //�X�e�[�^�X������
        EnemyHP = _enemyMaxHp;

        //���̋O��Off
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
        //Nav �ړI�n�X�V
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
            //���̋O��On
            _particleSword.SetActive(true);
            //����Collider On
            _enemySwordCollider.SetActive(true);

            _enemyAnim.SetTrigger("Attack1");

            StartCoroutine("WaitTime1");
        }
        else if (_rand < 71 && _rand > 49)
        {
            //���̋O��On
            _particleSword.SetActive(true);
            //����Collider On
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
        
        //���̋O�� Off
        _particleSword.SetActive(false);
        //����Collider Off
        _enemySwordCollider.SetActive(false);

        _enemyAnim.ResetTrigger("Attack1");
    }

    IEnumerator WaitTime2()
    {
        yield return new WaitForSeconds(1.5f);

        //���̋O�� Off
        _particleSword.SetActive(false);
        //����Collider Off
        _enemySwordCollider.SetActive(false);

        _enemyAnim.ResetTrigger("Attack2");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // �g�p����R���|�[�l���g //
    Rigidbody _enemyRb;

    // �����Ɋւ���ϐ� //
    [SerializeField, Header("�ړ����x"), Tooltip("Enemy�̈ړ����x�̂��߂̃����o�[�ϐ�")] float _speed = 1.0f;

    //enemy�̃X�e�[�^�X�֘A
    [SerializeField, Header("Enemy��"), Tooltip("Enemy���̂��߂̃����o�[�ϐ�")] string _enemyName;
    [Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] public int _enemyMaxHp;
    int _currentEnemyHp;
    //Enemy�X�e�[�^�X�̃v���p�e�B�A�O������͓ǂݎ��̂�
    public string EnemyName { get => _enemyName; private set => _enemyName = value; }
    public int EnemyHP { get => _currentEnemyHp; private set => _currentEnemyHp = value; }
    
    // �A�j���[�V�����֘A //
    Animator _enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _enemyAnim = GetComponent<Animator>();

        //�X�e�[�^�X������
        EnemyHP = _enemyMaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        EnemyAttack();
        Hit();
    }

    /// <summary>
    /// Enemy�̈ړ��̏���
    /// </summary>
    void EnemyMove() 
    {

    }

    /// <summary>
    /// Enemy�̍U���̏���
    /// </summary>
    void EnemyAttack() 
    {

    }

    /// <summary>
    /// �U���󂯂����̏���
    /// </summary>
    void Hit()
    {

    }

    bool EnemyDie() 
    {
        if (EnemyHP == 0)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}

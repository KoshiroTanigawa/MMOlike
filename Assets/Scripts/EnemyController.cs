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
    [SerializeField, Header("MaxHP"), Tooltip("MaxHP�̂��߂̃����o�[�ϐ�")] int _enemyMaxHp;
    
    int _currentEnemyHp;
    public int EnemyHP { get => _currentEnemyHp; set => _currentEnemyHp = value; }
    
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
        
    }
}

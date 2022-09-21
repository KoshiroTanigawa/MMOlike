using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 使用するコンポーネント //
    Rigidbody _enemyRb;

    // 動きに関する変数 //
    [SerializeField, Header("移動速度"), Tooltip("Enemyの移動速度のためのメンバー変数")] float _speed = 1.0f;

    //enemyのステータス関連
    [SerializeField, Header("Enemy名"), Tooltip("Enemy名のためのメンバー変数")] string _enemyName;
    [Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] public int _enemyMaxHp;
    int _currentEnemyHp;
    //Enemyステータスのプロパティ、外部からは読み取りのみ
    public string EnemyName { get => _enemyName; private set => _enemyName = value; }
    public int EnemyHP { get => _currentEnemyHp; private set => _currentEnemyHp = value; }
    
    // アニメーション関連 //
    Animator _enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _enemyAnim = GetComponent<Animator>();

        //ステータス初期化
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
    /// Enemyの移動の処理
    /// </summary>
    void EnemyMove() 
    {

    }

    /// <summary>
    /// Enemyの攻撃の処理
    /// </summary>
    void EnemyAttack() 
    {

    }

    /// <summary>
    /// 攻撃受けた時の処理
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

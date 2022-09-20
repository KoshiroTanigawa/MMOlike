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
    [SerializeField, Header("MaxHP"), Tooltip("MaxHPのためのメンバー変数")] int _enemyMaxHp;
    
    int _currentEnemyHp;
    public int EnemyHP { get => _currentEnemyHp; set => _currentEnemyHp = value; }
    
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
        
    }
}

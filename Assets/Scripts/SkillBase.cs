using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour
{
    GameObject _manager;
    GameObject _player;
    GameObject _enemy;
    PlayerController _playerController;
    EnemyController _enemyController;

    string _skillName;
    int _damage = 50;
    float _damageScale = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _enemy = GameObject.Find("Enemy");
        _enemyController = _enemy.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void UseSkill() 
    {
        Debug.Log(_skillName + "‚ðŽg—p");
    }
}

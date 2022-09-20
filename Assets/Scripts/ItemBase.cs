using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    GameObject _player;
    GameObject _enemy;
    PlayerController _playerController;
    EnemyController _enemyController;

    string _itemName;

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

    public virtual void UseItem()
    {
        Debug.Log(_itemName + "‚ðŽg—p");
    }
}

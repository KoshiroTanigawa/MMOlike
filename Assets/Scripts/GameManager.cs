using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class GameManager : MonoBehaviour
{
    //�Q��
    UIManager _uiManager;
    AudioManager _audioManager;
    PlayerController _playerController;
    EnemyController _enemyController;

    //�L�����N�^�[�̃I�u�W�F�N�g
    GameObject _player;
    GameObject _enemy;

    // Start is called before the first frame update
    void Start()
    {
        //�e�Q�ƌ����擾
        _uiManager = GetComponent<UIManager>();
        _audioManager = GetComponent<AudioManager>();
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _enemy = GameObject.Find("Enemy");
        _enemyController = _enemy.GetComponent<EnemyController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

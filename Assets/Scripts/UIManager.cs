using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player 関連
    [Header("プレイヤー名のUI"), Tooltip("プレイヤー名のテキストを入れる")] public TextMeshProUGUI _playerNameText;
    [Header("プレイヤーHPのUI"), Tooltip("プレイヤーHPのテキストを入れる")] public TextMeshProUGUI _playerHPText;
    [Header("プレイヤーMPのUI"), Tooltip("プレイヤーMPのテキストを入れる")] public TextMeshProUGUI _playerMPText;

    //Enemy 関連
    [Header("エネミー名のUI"), Tooltip("エネミー名のテキストを入れる")] public TextMeshProUGUI _enemyNameText;
    [Header("エネミーHPのUI"), Tooltip("エネミーHPのテキストを入れる")] public TextMeshProUGUI _enemyHPText;
    [Header("エネミーHPバー（赤）のUI"), Tooltip("エネミーHPバー（赤）のテキストを入れる")] public Image _enemyRedHPBar;
    [Header("エネミーHPバー（灰）のUI"), Tooltip("エネミーHPバー（灰）のテキストを入れる")] public Image _enemyGrayHPBar;

    //PauseMenu 関連
    [Tooltip("Menuを開いているかのフラグ")] bool _onMenu;
    [SerializeField, Header("ポーズメニュー＆メモのUI"), Tooltip("ポーズメニュー＆メモのテキストを入れる")] GameObject _menu;
    [SerializeField, Header("ExitボタンのUI"), Tooltip("Exitボタンを入れる")] GameObject _exitButton;
    [SerializeField, Header("RestartボタンのUI"), Tooltip("Restartボタンを入れる")] GameObject _restartButton;
    [SerializeField, Header("BackボタンのUI"), Tooltip("Backボタンを入れる")] GameObject _backButton;

    //Log 関連
    [Header("LogのUI"), Tooltip("Logのテキストを入れる")] public TextMeshProUGUI _logText;

    //Skill&Item関連
    bool _onSkill1;
    bool _onSkill2;
    bool _onSkill3;
    bool _onSkill4;
    bool _onItem1;
    bool _onItem2;
    bool _onItem3;


    // Start is called before the first frame update
    void Start()
    {
        _menu.SetActive(false);
        _onSkill1 = true;
        _onSkill2 = true;
        _onSkill3 = true;
        _onSkill4 = true;
        _onItem1 = true;
        _onItem2 = true;
        _onItem3 = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputEscape();
    }

    /// <summary> Escapeボタン入力時の処理/// </summary>
    public void InputEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_onMenu)
        {
            _onMenu = true;
            _menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _onMenu)
        {
            _onMenu = false;
            _menu.SetActive(false);
        }
    }

    public void OnRcastingSkill() 
    {
       
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player 関連
    [Header("プレイヤー名のUI"),Tooltip("プレイヤー名のテキストを入れる")] public TextMeshProUGUI _playerNameText;
    [Header("プレイヤーHPのUI"), Tooltip("プレイヤーHPのテキストを入れる")] public TextMeshProUGUI _playerHPText;
    [Header("プレイヤーMPのUI"), Tooltip("プレイヤーMPのテキストを入れる")] public TextMeshProUGUI _playerMPText;

    //Enemy 関連
    [Header("エネミー名のUI"), Tooltip("エネミー名のテキストを入れる")] public TextMeshProUGUI _enemyNameText;
    [Header("エネミーHPのUI"), Tooltip("エネミーHPのテキストを入れる")] public TextMeshProUGUI _enemyHPText;
    [Header("エネミーHPバー（赤）のUI"), Tooltip("エネミーHPバー（赤）のテキストを入れる")] public Image _enemyRedHPBar;
    [Header("エネミーHPバー（灰）のUI"), Tooltip("エネミーHPバー（灰）のテキストを入れる")] public Image _enemyGrayHPBar;

    //PauseMenu 関連
    [Header("ポーズメニュー＆メモのUI"), Tooltip("ポーズメニュー＆メモのテキストを入れる")] public Image _menu;
    [Header("ExitボタンのUI"), Tooltip("Exitボタンを入れる")] public Button _exitButton;
    [Header("RestartボタンのUI"), Tooltip("Restartボタンを入れる")] public Button _restartButton;
    [Header("BackボタンのUI"), Tooltip("Backボタンを入れる")] public Button _backButton;

    //Log 関連
    [Header("LogのUI"), Tooltip("Logのテキストを入れる")] public TextMeshProUGUI _logText;


    // Start is called before the first frame update
    void Start()
    {
        _menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> Escapeボタン入力時の処理/// </summary>
    public void InputEscape() 
    {
        if (Input.GetButtonDown("Escape") && !_menu.enabled)
            _menu.enabled = true;
        else if (Input.GetButtonDown("Escape") && _menu.enabled)
            _menu.enabled = false;
    }

}

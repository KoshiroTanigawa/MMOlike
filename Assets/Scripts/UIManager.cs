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

    //PauseMenu 関連
    [Tooltip("Menuを開いているかのフラグ")] bool _onMenu;
    [SerializeField, Header("ポーズメニュー＆メモのUI"), Tooltip("ポーズメニュー＆メモのテキストを入れる")] GameObject _menu;
    [SerializeField, Header("ExitボタンのUI"), Tooltip("Exitボタンを入れる")] GameObject _exitButton;
    [SerializeField, Header("RestartボタンのUI"), Tooltip("Restartボタンを入れる")] GameObject _restartButton;
    [SerializeField, Header("BackボタンのUI"), Tooltip("Backボタンを入れる")] GameObject _backButton;

    //Log 関連
    [Header("LogのUI"), Tooltip("Logのテキストを入れる")] public TextMeshProUGUI _logText;

    //Skill&Item関連
    //リキャスト中に表示するオブジェクト
    [SerializeField] GameObject _recastingSkill1;
    [SerializeField] GameObject _recastingSkill2;
    [SerializeField] GameObject _recastingSkill3;
    [SerializeField] GameObject _recastingSkill4;
    [SerializeField] GameObject _recastingItem1;
    [SerializeField] GameObject _recastingItem2;
    [SerializeField] GameObject _recastingItem3;
    //リキャストタイムのテキスト
    [SerializeField] TextMeshProUGUI _rtText1;
    [SerializeField] TextMeshProUGUI _rtText2;
    [SerializeField] TextMeshProUGUI _rtText3;
    [SerializeField] TextMeshProUGUI _rtText4;
    [SerializeField] TextMeshProUGUI _rtText5;
    [SerializeField] TextMeshProUGUI _rtText6;
    [SerializeField] TextMeshProUGUI _rtText7;
    //Skillフラグ
    bool _onSkill1;
    bool _onSkill2;
    bool _onSkill3;
    bool _onSkill4;
    //Itemフラグ
    bool _onItem1;
    bool _onItem2;
    bool _onItem3;
    //Skill&Itemのリキャスト時間のための変数
    [SerializeField] int _rt1;
    [SerializeField] int _rt2;
    [SerializeField] int _rt3;
    [SerializeField] int _rt4;
    [SerializeField] int _rt5;
    [SerializeField] int _rt6;
    [SerializeField] int _rt7;


    // Start is called before the first frame update
    void Start()
    {
        //PauseMenu Off
        _menu.SetActive(false);

        //リキャスト中に表示するオブジェクト Off
        _recastingSkill1.SetActive(false);
        _recastingSkill2.SetActive(false);
        _recastingSkill3.SetActive(false);
        _recastingSkill4.SetActive(false);
        _recastingItem1.SetActive(false);
        _recastingItem2.SetActive(false);
        _recastingItem3.SetActive(false);

        //Skillを使える状態にする
        _onSkill1 = true;
        _onSkill2 = true;
        _onSkill3 = true;
        _onSkill4 = true;

        //Itemを使える状態にする
        _onItem1 = true;
        _onItem2 = true;
        _onItem3 = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputEscape();
        UseSkill();
    }

    /// <summary> Escapeボタン入力時の処理/// </summary>
    void InputEscape()
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

    void UseSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _onSkill1) 
        {
            //Skillを使えない状態にする
            _onSkill1 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill1.SetActive(true);

            //リキャスト時間をセット
            _rtText1.text = _rt1.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText1.DOCounter(_rt1, 0, _rt1)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onSkill1 = true;
                        _recastingSkill1.SetActive(false);
                    });
        }

    }
}

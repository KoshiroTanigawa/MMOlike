using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player 関連
    GameObject _player;
    PlayerController _playerController;
    [Header("プレイヤー名のUI"), Tooltip("プレイヤー名のテキストを入れる")] public TextMeshProUGUI _playerNameText;
    [Header("プレイヤーHPのUI"), Tooltip("プレイヤーHPのテキストを入れる")] public TextMeshProUGUI _playerHPText;
    [Header("プレイヤーMPのUI"), Tooltip("プレイヤーMPのテキストを入れる")] public TextMeshProUGUI _playerMPText;

    //Enemy 関連
    GameObject _enemy;
    EnemyController _enemyController;
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
        //プレイヤー関連の参照取得
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();

        //エネミー関連の参照取得
        _enemy = GameObject.Find("Enemy");
        _enemyController = _enemy.GetComponent<EnemyController>();

        //ステータスの初期化
        //プレイヤー
        _playerNameText.text = _playerController.PlayerName;
        _playerHPText.text = "HP : " + _playerController.PlayerHP.ToString() + " / " + _playerController._playerMaxHp.ToString();
        _playerMPText.text = "MP : " + _playerController.PlayerMP.ToString() + " / " + _playerController._playerMaxMp.ToString();
        //エネミー
        _enemyNameText.text = _enemyController.EnemyName;
        _enemyHPText.text = _enemyController.EnemyHP.ToString() + " / " + _enemyController._enemyMaxHp.ToString();

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
        //Skill1についての処理
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
        //Skill2についての処理
        if(Input.GetKeyDown(KeyCode.Alpha2) && _onSkill2) 
        {
            //Skillを使えない状態にする
            _onSkill2 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill2.SetActive(true);

            //リキャスト時間をセット
            _rtText2.text = _rt2.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText2.DOCounter(_rt2, 0, _rt2)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onSkill2 = true;
                        _recastingSkill2.SetActive(false);
                    });
        }
        //Skill3についての処理
        if (Input.GetKeyDown(KeyCode.Alpha3) && _onSkill3)
        {
            //Skillを使えない状態にする
            _onSkill3 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill3.SetActive(true);

            //リキャスト時間をセット
            _rtText3.text = _rt3.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText3.DOCounter(_rt3, 0, _rt3)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onSkill3 = true;
                        _recastingSkill3.SetActive(false);
                    });
        }
        //Skill4についての処理
        if (Input.GetKeyDown(KeyCode.Alpha4) && _onSkill4)
        {
            //Skillを使えない状態にする
            _onSkill4 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill4.SetActive(true);

            //リキャスト時間をセット
            _rtText4.text = _rt4.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText4.DOCounter(_rt4, 0, _rt4)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onSkill4 = true;
                        _recastingSkill4.SetActive(false);
                    });
        }

        //Item1についての処理
        if (Input.GetKeyDown(KeyCode.Q) && _onItem1)
        {
            //Skillを使えない状態にする
            _onItem1 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem1.SetActive(true);

            //リキャスト時間をセット
            _rtText5.text = _rt5.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText5.DOCounter(_rt5, 0, _rt5)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onItem1 = true;
                        _recastingItem1.SetActive(false);
                    });
        }
        //Item2についての処理
        if (Input.GetKeyDown(KeyCode.E) && _onItem2)
        {
            //Skillを使えない状態にする
            _onItem2 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem2.SetActive(true);

            //リキャスト時間をセット
            _rtText6.text = _rt6.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText6.DOCounter(_rt6, 0, _rt6)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onItem2 = true;
                        _recastingItem2.SetActive(false);
                    });
        }
        //Item3についての処理
        if (Input.GetKeyDown(KeyCode.R) && _onItem3)
        {
            //Skillを使えない状態にする
            _onItem3 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem3.SetActive(true);

            //リキャスト時間をセット
            _rtText7.text = _rt7.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText7.DOCounter(_rt7, 0, _rt7)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _onItem3 = true;
                        _recastingItem3.SetActive(false);
                    });
        }
    }
    public void EnemyHPBar() 
    {

    }
}

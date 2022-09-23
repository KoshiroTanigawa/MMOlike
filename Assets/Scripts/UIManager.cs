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
    [Header("エネミーHPスライダーのUI"), Tooltip("エネミーHPのスライダーを入れる")] Slider _enemyHPSlider;


    //PauseMenu 関連
    [Tooltip("Menuを開いているかのフラグ")] bool _onMenu;
    [SerializeField, Header("ポーズメニュー＆メモのUI"), Tooltip("ポーズメニュー＆メモのテキストを入れる")] GameObject _menu;

    /*
    [SerializeField, Header("ExitボタンのUI"), Tooltip("Exitボタンを入れる")] GameObject _exitButton;
    [SerializeField, Header("RestartボタンのUI"), Tooltip("Restartボタンを入れる")] GameObject _restartButton;
    [SerializeField, Header("Back111114ボタンのUI"), Tooltip("Backボタンを入れる")] GameObject _backButton;
    */

    //Log 関連
    [SerializeField, Header("LogのUI"), Tooltip("LogWindowを入れる")] GameObject _logWindow;
    [SerializeField, Header("LogのText"), Tooltip("LogTextを入れる")] Text _logText;
    int _lineCount;

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

    void Start()
    {
        //プレイヤー関連の参照取得
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();

        //エネミー関連の参照取得
        _enemy = GameObject.Find("Enemy");
        _enemyController = _enemy.GetComponent<EnemyController>();
        _enemyHPSlider = GameObject.Find("EnemyHPSlider").GetComponent<Slider>();

        //ステータスの初期化
        /*
        //プレイヤー
        _playerNameText.text = _playerController.PlayerName;
        _playerHPText.text = "HP : " + _playerController.PlayerHP.ToString() + " / " + _playerController._playerMaxHp.ToString();
        _playerMPText.text = "MP : " + _playerController.PlayerMP.ToString() + " / " + _playerController._playerMaxMp.ToString();
        //エネミー
        _enemyNameText.text = _enemyController.EnemyName;
        _enemyHPText.text = _enemyController.EnemyHP.ToString() + " / " + _enemyController._enemyMaxHp.ToString();
        _enemyHPSlider.maxValue = _enemyController._enemyMaxHp;
        */

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

        //Log関連
        //LogTextの初期化
        _logText.text = "";
        //行数カウンターの初期化
        _lineCount = 0;
 
    }

    void Update()
    {
        InputEscape();
        
        SkillTimer();
        EnemyHPSliderContoroller();

        //プレイヤー
        _playerHPText.text = "HP : " + _playerController.PlayerHP.ToString() + " / " + _playerController._playerMaxHp.ToString();
        _playerMPText.text = "MP : " + _playerController.PlayerMP.ToString() + " / " + _playerController._playerMaxMp.ToString();

        ////エネミー
        _enemyHPText.text = _enemyController.EnemyHP.ToString() + " / " + _enemyController._enemyMaxHp.ToString();
    }

    /// <summary> 
    /// ///Escapeボタン入力時の処理
    /// /// </summary>
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

    /// <summary>
    /// スキルのリキャストタイマーの処理
    /// </summary>
    void SkillTimer()
    {
        //Skill1についての処理
        if (Input.GetKeyDown(KeyCode.Alpha1) && _playerController.OnSkill1) 
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Skill1-");

            //Skillを使えない状態にする
            _playerController.OnSkill1 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill1.SetActive(true);

            //リキャスト時間をセット
            _rtText1.text = _playerController.RT1.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText1.DOCounter(_playerController.RT1, 0, _playerController.RT1)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill1 = true;
                        _recastingSkill1.SetActive(false);
                    });
        }
        //Skill2についての処理
        if(Input.GetKeyDown(KeyCode.Alpha2) && _playerController.OnSkill2) 
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Skill2-");

            //Skillを使えない状態にする
            _playerController.OnSkill2 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill2.SetActive(true);

            //リキャスト時間をセット
            _rtText2.text = _playerController.RT2.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText2.DOCounter(_playerController.RT2, 0, _playerController.RT2)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill2 = true;
                        _recastingSkill2.SetActive(false);
                    });
        }
        //Skill3についての処理
        if (Input.GetKeyDown(KeyCode.Alpha3) && _playerController.OnSkill3)
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Skill3-");

            //Skillを使えない状態にする
            _playerController.OnSkill3 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill3.SetActive(true);

            //リキャスト時間をセット
            _rtText3.text = _playerController.RT3.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText3.DOCounter(_playerController.RT3, 0, _playerController.RT3)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill3 = true;
                        _recastingSkill3.SetActive(false);
                    });
        }
        //Skill4についての処理
        if (Input.GetKeyDown(KeyCode.Alpha4) && _playerController.OnSkill4)
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Skill4-");

            //Skillを使えない状態にする
            _playerController.OnSkill4 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingSkill4.SetActive(true);

            //リキャスト時間をセット
            _rtText4.text = _playerController.RT4.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText4.DOCounter(_playerController.RT4, 0, _playerController.RT4)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill4 = true;
                        _recastingSkill4.SetActive(false);
                    });
        }

        //Item1についての処理
        if (Input.GetKeyDown(KeyCode.Q) && _playerController.OnItem1)
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Item1-");

            //Skillを使えない状態にする
            _playerController.OnItem1 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem1.SetActive(true);

            //リキャスト時間をセット
            _rtText5.text = _playerController.RT5.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText5.DOCounter(_playerController.RT5, 0, _playerController.RT5)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem1 = true;
                        _recastingItem1.SetActive(false);
                    });
        }
        //Item2についての処理
        if (Input.GetKeyDown(KeyCode.E) && _playerController.OnItem2)
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Item2-");

            //Skillを使えない状態にする
            _playerController.OnItem2 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem2.SetActive(true);

            //リキャスト時間をセット
            _rtText6.text = _playerController.RT6.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText6.DOCounter(_playerController.RT6, 0, _playerController.RT6)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem2 = true;
                        _recastingItem2.SetActive(false);
                    });
        }
        //Item3についての処理
        if (Input.GetKeyDown(KeyCode.R) && _playerController.OnItem3)
        {
            //Logに表示
            LogOutPut(" " + _playerController.PlayerName + "の -Item3-");

            //Skillを使えない状態にする
            _playerController.OnItem3 = false;

            // //リキャスト中に表示するオブジェクト On
            _recastingItem3.SetActive(true);

            //リキャスト時間をセット
            _rtText7.text = _playerController.RT7.ToString();

            //リキャスト時間分だけカウントダウン
            _rtText7.DOCounter(_playerController.RT7, 0, _playerController.RT7)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem3 = true;
                        _recastingItem3.SetActive(false);
                    });
        }
    }

    /// <summary>
    /// Log欄にLogを表示する
    /// </summary>
    /// <param name="logstr"></param>
    void LogOutPut(string logstr)
    {
        _logText.text += logstr;
        _logText.text += "\n";
        _lineCount += 1;
        // 常にTextの最下部（最新）を表示するように強制スクロール
        _logWindow.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

        //最大行数以上で初期化 ここでは50行
        if (_lineCount > 50)
            _logText.text = "";
    }

    /// <summary>
    /// EnemyHPSliderの処理
    /// </summary>
    void EnemyHPSliderContoroller() 
    {
        _enemyHPSlider.value = _enemyController.EnemyHP;
    }
}

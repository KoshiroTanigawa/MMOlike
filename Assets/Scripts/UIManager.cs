using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player �֘A
    [Header("�v���C���[����UI"), Tooltip("�v���C���[���̃e�L�X�g������")] public TextMeshProUGUI _playerNameText;
    [Header("�v���C���[HP��UI"), Tooltip("�v���C���[HP�̃e�L�X�g������")] public TextMeshProUGUI _playerHPText;
    [Header("�v���C���[MP��UI"), Tooltip("�v���C���[MP�̃e�L�X�g������")] public TextMeshProUGUI _playerMPText;

    //Enemy �֘A
    [Header("�G�l�~�[����UI"), Tooltip("�G�l�~�[���̃e�L�X�g������")] public TextMeshProUGUI _enemyNameText;
    [Header("�G�l�~�[HP��UI"), Tooltip("�G�l�~�[HP�̃e�L�X�g������")] public TextMeshProUGUI _enemyHPText;
    [Header("�G�l�~�[HP�o�[�i�ԁj��UI"), Tooltip("�G�l�~�[HP�o�[�i�ԁj�̃e�L�X�g������")] public Image _enemyRedHPBar;
    [Header("�G�l�~�[HP�o�[�i�D�j��UI"), Tooltip("�G�l�~�[HP�o�[�i�D�j�̃e�L�X�g������")] public Image _enemyGrayHPBar;

    //PauseMenu �֘A
    [Tooltip("Menu���J���Ă��邩�̃t���O")] bool _onMenu;
    [SerializeField, Header("�|�[�Y���j���[��������UI"), Tooltip("�|�[�Y���j���[�������̃e�L�X�g������")] GameObject _menu;
    [SerializeField, Header("Exit�{�^����UI"), Tooltip("Exit�{�^��������")] GameObject _exitButton;
    [SerializeField, Header("Restart�{�^����UI"), Tooltip("Restart�{�^��������")] GameObject _restartButton;
    [SerializeField, Header("Back�{�^����UI"), Tooltip("Back�{�^��������")] GameObject _backButton;

    //Log �֘A
    [Header("Log��UI"), Tooltip("Log�̃e�L�X�g������")] public TextMeshProUGUI _logText;

    //Skill&Item�֘A
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

    /// <summary> Escape�{�^�����͎��̏���/// </summary>
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

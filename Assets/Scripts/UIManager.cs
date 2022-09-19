using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player �֘A
    [Header("�v���C���[����UI"),Tooltip("�v���C���[���̃e�L�X�g������")] public TextMeshProUGUI _playerNameText;
    [Header("�v���C���[HP��UI"), Tooltip("�v���C���[HP�̃e�L�X�g������")] public TextMeshProUGUI _playerHPText;
    [Header("�v���C���[MP��UI"), Tooltip("�v���C���[MP�̃e�L�X�g������")] public TextMeshProUGUI _playerMPText;

    //Enemy �֘A
    [Header("�G�l�~�[����UI"), Tooltip("�G�l�~�[���̃e�L�X�g������")] public TextMeshProUGUI _enemyNameText;
    [Header("�G�l�~�[HP��UI"), Tooltip("�G�l�~�[HP�̃e�L�X�g������")] public TextMeshProUGUI _enemyHPText;
    [Header("�G�l�~�[HP�o�[�i�ԁj��UI"), Tooltip("�G�l�~�[HP�o�[�i�ԁj�̃e�L�X�g������")] public Image _enemyRedHPBar;
    [Header("�G�l�~�[HP�o�[�i�D�j��UI"), Tooltip("�G�l�~�[HP�o�[�i�D�j�̃e�L�X�g������")] public Image _enemyGrayHPBar;

    //PauseMenu �֘A
    [Header("�|�[�Y���j���[��������UI"), Tooltip("�|�[�Y���j���[�������̃e�L�X�g������")] public Image _menu;
    [Header("Exit�{�^����UI"), Tooltip("Exit�{�^��������")] public Button _exitButton;
    [Header("Restart�{�^����UI"), Tooltip("Restart�{�^��������")] public Button _restartButton;
    [Header("Back�{�^����UI"), Tooltip("Back�{�^��������")] public Button _backButton;

    //Log �֘A
    [Header("Log��UI"), Tooltip("Log�̃e�L�X�g������")] public TextMeshProUGUI _logText;


    // Start is called before the first frame update
    void Start()
    {
        _menu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> Escape�{�^�����͎��̏���/// </summary>
    public void InputEscape() 
    {
        if (Input.GetButtonDown("Escape") && !_menu.enabled)
            _menu.enabled = true;
        else if (Input.GetButtonDown("Escape") && _menu.enabled)
            _menu.enabled = false;
    }

}

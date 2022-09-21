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

    //PauseMenu �֘A
    [Tooltip("Menu���J���Ă��邩�̃t���O")] bool _onMenu;
    [SerializeField, Header("�|�[�Y���j���[��������UI"), Tooltip("�|�[�Y���j���[�������̃e�L�X�g������")] GameObject _menu;
    [SerializeField, Header("Exit�{�^����UI"), Tooltip("Exit�{�^��������")] GameObject _exitButton;
    [SerializeField, Header("Restart�{�^����UI"), Tooltip("Restart�{�^��������")] GameObject _restartButton;
    [SerializeField, Header("Back�{�^����UI"), Tooltip("Back�{�^��������")] GameObject _backButton;

    //Log �֘A
    [Header("Log��UI"), Tooltip("Log�̃e�L�X�g������")] public TextMeshProUGUI _logText;

    //Skill&Item�֘A
    //���L���X�g���ɕ\������I�u�W�F�N�g
    [SerializeField] GameObject _recastingSkill1;
    [SerializeField] GameObject _recastingSkill2;
    [SerializeField] GameObject _recastingSkill3;
    [SerializeField] GameObject _recastingSkill4;
    [SerializeField] GameObject _recastingItem1;
    [SerializeField] GameObject _recastingItem2;
    [SerializeField] GameObject _recastingItem3;
    //���L���X�g�^�C���̃e�L�X�g
    [SerializeField] TextMeshProUGUI _rtText1;
    [SerializeField] TextMeshProUGUI _rtText2;
    [SerializeField] TextMeshProUGUI _rtText3;
    [SerializeField] TextMeshProUGUI _rtText4;
    [SerializeField] TextMeshProUGUI _rtText5;
    [SerializeField] TextMeshProUGUI _rtText6;
    [SerializeField] TextMeshProUGUI _rtText7;
    //Skill�t���O
    bool _onSkill1;
    bool _onSkill2;
    bool _onSkill3;
    bool _onSkill4;
    //Item�t���O
    bool _onItem1;
    bool _onItem2;
    bool _onItem3;
    //Skill&Item�̃��L���X�g���Ԃ̂��߂̕ϐ�
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

        //���L���X�g���ɕ\������I�u�W�F�N�g Off
        _recastingSkill1.SetActive(false);
        _recastingSkill2.SetActive(false);
        _recastingSkill3.SetActive(false);
        _recastingSkill4.SetActive(false);
        _recastingItem1.SetActive(false);
        _recastingItem2.SetActive(false);
        _recastingItem3.SetActive(false);

        //Skill���g�����Ԃɂ���
        _onSkill1 = true;
        _onSkill2 = true;
        _onSkill3 = true;
        _onSkill4 = true;

        //Item���g�����Ԃɂ���
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

    /// <summary> Escape�{�^�����͎��̏���/// </summary>
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
            //Skill���g���Ȃ���Ԃɂ���
            _onSkill1 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingSkill1.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText1.text = _rt1.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
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

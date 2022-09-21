using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    //Player �֘A
    GameObject _player;
    PlayerController _playerController;
    [Header("�v���C���[����UI"), Tooltip("�v���C���[���̃e�L�X�g������")] public TextMeshProUGUI _playerNameText;
    [Header("�v���C���[HP��UI"), Tooltip("�v���C���[HP�̃e�L�X�g������")] public TextMeshProUGUI _playerHPText;
    [Header("�v���C���[MP��UI"), Tooltip("�v���C���[MP�̃e�L�X�g������")] public TextMeshProUGUI _playerMPText;

    //Enemy �֘A
    GameObject _enemy;
    EnemyController _enemyController;
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
    [SerializeField, Header("Log��UI"), Tooltip("LogWindow������")] GameObject _logWindow;
    [SerializeField, Header("Log��Text"), Tooltip("LogText������")] Text _logText;
    int _lineCount;

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
        //�v���C���[�֘A�̎Q�Ǝ擾
        _player = GameObject.Find("Player");
        _playerController = _player.GetComponent<PlayerController>();

        //�G�l�~�[�֘A�̎Q�Ǝ擾
        _enemy = GameObject.Find("Enemy");
        _enemyController = _enemy.GetComponent<EnemyController>();

        //�X�e�[�^�X�̏�����
        //�v���C���[
        _playerNameText.text = _playerController.PlayerName;
        _playerHPText.text = "HP : " + _playerController.PlayerHP.ToString() + " / " + _playerController._playerMaxHp.ToString();
        _playerMPText.text = "MP : " + _playerController.PlayerMP.ToString() + " / " + _playerController._playerMaxMp.ToString();
        //�G�l�~�[
        _enemyNameText.text = _enemyController.EnemyName;
        _enemyHPText.text = _enemyController.EnemyHP.ToString() + " / " + _enemyController._enemyMaxHp.ToString();

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

        //Log�֘A
        //LogText�̏�����
        _logText.text = "";
        //�s���J�E���^�[�̏�����
        _lineCount = 0;
 
    }

    // Update is called once per frame
    void Update()
    {
        InputEscape();
        SkillTimer();
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

    void SkillTimer()
    {
        //Skill1�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.Alpha1) && _playerController.OnSkill1) 
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Skill1-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnSkill1 = false;

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
                        _playerController.OnSkill1 = true;
                        _recastingSkill1.SetActive(false);
                    });
        }
        //Skill2�ɂ��Ă̏���
        if(Input.GetKeyDown(KeyCode.Alpha2) && _playerController.OnSkill2) 
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Skill2-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnSkill2 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingSkill2.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText2.text = _rt2.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText2.DOCounter(_rt2, 0, _rt2)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill2 = true;
                        _recastingSkill2.SetActive(false);
                    });
        }
        //Skill3�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.Alpha3) && _playerController.OnSkill3)
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Skill3-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnSkill3 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingSkill3.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText3.text = _rt3.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText3.DOCounter(_rt3, 0, _rt3)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill3 = true;
                        _recastingSkill3.SetActive(false);
                    });
        }
        //Skill4�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.Alpha4) && _playerController.OnSkill4)
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Skill4-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnSkill4 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingSkill4.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText4.text = _rt4.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText4.DOCounter(_rt4, 0, _rt4)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnSkill4 = true;
                        _recastingSkill4.SetActive(false);
                    });
        }

        //Item1�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.Q) && _playerController.OnItem1)
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Item1-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnItem1 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingItem1.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText5.text = _rt5.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText5.DOCounter(_rt5, 0, _rt5)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem1 = true;
                        _recastingItem1.SetActive(false);
                    });
        }
        //Item2�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.E) && _playerController.OnItem2)
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Item2-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnItem2 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingItem2.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText6.text = _rt6.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText6.DOCounter(_rt6, 0, _rt6)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem2 = true;
                        _recastingItem2.SetActive(false);
                    });
        }
        //Item3�ɂ��Ă̏���
        if (Input.GetKeyDown(KeyCode.R) && _playerController.OnItem3)
        {
            //Log�ɕ\��
            LogOutPut(" " + _playerController.PlayerName + "�� -Item3-");

            //Skill���g���Ȃ���Ԃɂ���
            _playerController.OnItem3 = false;

            // //���L���X�g���ɕ\������I�u�W�F�N�g On
            _recastingItem3.SetActive(true);

            //���L���X�g���Ԃ��Z�b�g
            _rtText7.text = _rt7.ToString();

            //���L���X�g���ԕ������J�E���g�_�E��
            _rtText7.DOCounter(_rt7, 0, _rt7)
                    .SetEase(Ease.Linear)
                    .SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        _playerController.OnItem3 = true;
                        _recastingItem3.SetActive(false);
                    });
        }
    }

    void LogOutPut(string logstr)
    {
        _logText.text += logstr;
        _logText.text += "\n";
        _lineCount += 1;
        // ���Text�̍ŉ����i�ŐV�j��\������悤�ɋ����X�N���[��
        _logWindow.GetComponent<ScrollRect>().verticalNormalizedPosition = 0;

        //�ő�s���ȏ�ŏ����� �����ł�50�s
        if (_lineCount > 50)
            _logText.text = "";
    }

    public void EnemyHPBar() 
    {

    }
}

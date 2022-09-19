using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraContoroller : MonoBehaviour
{
    CinemachineFreeLook _freeLookCamera;
    CinemachineOrbitalTransposer _orbitalTransposer;
    Vector2 lastMousePosition;
    Vector2 _cameraAngle = new Vector2(0, 0);   // �J�����̊p�x���i�[����ϐ�

    [SerializeField, Header("�Y�[���̑���")] float _forwardSpeed;
    [SerializeField, Header("�����h���b�O�̑���")] float _riseSpeed;

    void Start()
    {
        _freeLookCamera = GetComponent<CinemachineFreeLook>();
        _orbitalTransposer = _freeLookCamera.GetComponentInChildren<CinemachineOrbitalTransposer>();
        _orbitalTransposer.m_XAxis.Value = 0;
    }

    void Update()
    {
        ForwardViewPoint();
        HeightViewPoint();
    }

    // �O��̃J��������
    private void ForwardViewPoint()
    {
        // �}�E�X�z�C�[���̉�]�l��ϐ� scroll �ɓn��
        float inputMouseScroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 offset = _freeLookCamera.transform.forward * inputMouseScroll * _forwardSpeed;
        _orbitalTransposer.m_FollowOffset -= offset;
    }


    // ���������̃J��������
    private void HeightViewPoint()
    {
        // ���N���b�N������
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;    // �}�E�X���W��ϐ�"lastMousePosition"�Ɋi�[
        }
        // ���h���b�O���Ă����
        else if (Input.GetMouseButton(0))
        {
            float y = lastMousePosition.y - Input.mousePosition.y;
            _orbitalTransposer.m_FollowOffset.y += y * _riseSpeed;
            lastMousePosition = Input.mousePosition;    // �}�E�X���W��ϐ�"lastMousePosition"�Ɋi�[
        }
    }
}

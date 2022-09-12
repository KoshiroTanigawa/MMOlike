using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraContoroller : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;
    CinemachineOrbitalTransposer _orbitalTransposer;
    Vector2 lastMousePosition;
    Vector2 _cameraAngle = new Vector2(0, 0);   // �J�����̊p�x���i�[����ϐ�

    [SerializeField] float _forwardSpeed;
    [SerializeField] float _riseSpeed;

    void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _orbitalTransposer = _virtualCamera.GetComponentInChildren<CinemachineOrbitalTransposer>();
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
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 offset = _virtualCamera.transform.forward * scroll * _forwardSpeed;
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

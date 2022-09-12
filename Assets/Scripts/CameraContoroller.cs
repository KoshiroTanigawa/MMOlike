using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraContoroller : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;
    CinemachineOrbitalTransposer _orbitalTransposer;
    Vector2 lastMousePosition;
    Vector2 _cameraAngle = new Vector2(0, 0);   // カメラの角度を格納する変数

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

    // 前後のカメラ操作
    private void ForwardViewPoint()
    {
        // マウスホイールの回転値を変数 scroll に渡す
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 offset = _virtualCamera.transform.forward * scroll * _forwardSpeed;
        _orbitalTransposer.m_FollowOffset -= offset;
    }


    // 垂直方向のカメラ操作
    private void HeightViewPoint()
    {
        // 左クリックした時
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;    // マウス座標を変数"lastMousePosition"に格納
        }
        // 左ドラッグしている間
        else if (Input.GetMouseButton(0))
        {
            float y = lastMousePosition.y - Input.mousePosition.y;
            _orbitalTransposer.m_FollowOffset.y += y * _riseSpeed;
            lastMousePosition = Input.mousePosition;    // マウス座標を変数"lastMousePosition"に格納
        }
    }
}

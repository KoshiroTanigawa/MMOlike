using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraContoroller : MonoBehaviour
{
    CinemachineFreeLook _freeLookCamera;
    CinemachineOrbitalTransposer _orbitalTransposer;
    Vector2 lastMousePosition;
    Vector2 _cameraAngle = new Vector2(0, 0);   // カメラの角度を格納する変数

    [SerializeField, Header("ズームの速さ")] float _forwardSpeed;
    [SerializeField, Header("垂直ドラッグの速さ")] float _riseSpeed;

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

    // 前後のカメラ操作
    private void ForwardViewPoint()
    {
        // マウスホイールの回転値を変数 scroll に渡す
        float inputMouseScroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 offset = _freeLookCamera.transform.forward * inputMouseScroll * _forwardSpeed;
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

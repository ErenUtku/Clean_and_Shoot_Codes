using System;
using System.Collections;
using System.Collections.Generic;
using Controllers.State;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Second Camera Angle")]
    [SerializeField] private Vector3 desiredPosition;
    [SerializeField] private Quaternion desiredRotation;

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    private Camera _camera;
    private float _moveSpeed = 3f;
    private bool _moveCamera;

    #region UNITY EVENTS
    
    private void Awake()
    {
        _camera = Camera.main;

        if (_camera == null) return;
        
        _defaultPosition = _camera.transform.localPosition;
        _defaultRotation = _camera.transform.localRotation;

        FightState.FightStateActive += CameraMovementActivation;

    }

    private void OnDestroy()
    {
        FightState.FightStateActive -= CameraMovementActivation;
    }

    private void Update()
    {
        if (_moveCamera)
        {
            MoveCamera(desiredPosition, desiredRotation, _moveSpeed);
        }
        else
        {
            MoveCamera(_defaultPosition, _defaultRotation, _moveSpeed);
        }
    }
    
    #endregion


    #region PRIVATE FIELDS

    private void MoveCamera(Vector3 targetPosition, Quaternion targetRotation, float moveSpeed)
    {
        _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

        _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, targetRotation, moveSpeed * Time.deltaTime);
    }

    private void CameraMovementActivation(bool value)
    {
        _moveCamera = value;
    }

    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PuckCollisionEffect : MonoBehaviour{
    private Camera _camera;
    private const float _maxZoomOrthographicSize = 2;
    private const float minTimeScale = 0.05f;
    private const float _zoomSpeed = 0.02f;
    private const float _slowCooldownTime = 1f;
    private const float _normalCooldownTime = 2f;

    private Vector3 _originalPosition;
    private float _originalOrthographicSize;
    
    private float _zoomInterpolate = 0.0f;
    private bool _isZooming = false;
    private bool _isCoolingDown = false;
    private float _cooldown = 0.0f;
    private Vector3 _zoomPoint;
    
    private void Awake(){
        _camera = Camera.main;
        _originalOrthographicSize = _camera.orthographicSize;
        _originalPosition = transform.position;
    }

    private void CheckForZoomCooldown(){
        if (!_isCoolingDown){
            Debug.Log("warming up");
            _cooldown += Time.deltaTime;
            if (_cooldown > _slowCooldownTime * minTimeScale){
                _isCoolingDown = true;
                _cooldown = 0.0f;
            }
        }
    }

    private void CheckForCooldownReset(){
        if (_isCoolingDown){
            Debug.Log("cooling down");
            _cooldown += Time.deltaTime;
            if (_cooldown > _normalCooldownTime){
                _isCoolingDown = false;
                _cooldown = 0.0f;
            }
        }
    }
    
    void Update(){
        CheckForCooldownReset();
        
        if (_isZooming && !_isCoolingDown){
            CheckForZoomCooldown();
            IncrementZoom();
        } else{
            DecrementZoom();
        }
    }

    private void IncrementZoom(){
        _zoomInterpolate += _zoomSpeed;
        if (_zoomInterpolate >= 1){
            _zoomInterpolate = 1;
        }
        
        Time.timeScale = minTimeScale;
        transform.position = Vector3.Lerp(_originalPosition, _zoomPoint, _zoomInterpolate);
        _camera.orthographicSize = Mathf.Lerp(_originalOrthographicSize, _maxZoomOrthographicSize, _zoomInterpolate);
    }
    
    private void DecrementZoom(){
        _zoomInterpolate -= _zoomSpeed;
        if (_zoomInterpolate <= 0){
            _zoomInterpolate = 0;
        }
        
        Time.timeScale = 1;
        transform.position = Vector3.Lerp(_originalPosition, _zoomPoint, _zoomInterpolate);
        _camera.orthographicSize = Mathf.Lerp(_originalOrthographicSize, _maxZoomOrthographicSize, _zoomInterpolate);
    }
    
    public void StartZoomingToPoint(Vector3 point){
        _zoomPoint = new Vector3(point.x, point.y, _originalPosition.z);
        _isZooming = true;
    }
    
    public void StopZoomingToPoint(){
        _isZooming = false;
    }
}

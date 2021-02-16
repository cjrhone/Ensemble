using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Player Controls Parameters")]

    public Transform playerCamera;

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private float _playerSpeed = 6f;

    [SerializeField]
    private float _turnSmoothTimeSeconds = 0.1f;

    [Header("Skill Parameters")]

    [SerializeField]
    private float _dashSpeedMultiplier = 3.0f;

    [SerializeField]
    private float _dashDurationSeconds = 0.5f;

    [SerializeField]
    private float _dashCooldownSeconds = 2.0f;

    private Vector3 _direction;
    private bool _dashAvailable;
    private bool _dashing;
    private float _dashStartTime;
    private float _turnSmoothVelocity;

    public event Action OnDashAvailable;
    public event Action OnDashUnavailable;

    void Start() {
        _dashAvailable = true; //TODO: make this first intiialization controlled by the Unlocking service and Energy management service
        OnDashAvailable?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if(!_dashing) {
            var targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) 
                * Mathf.Rad2Deg
                + playerCamera.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref _turnSmoothVelocity,
                _turnSmoothTimeSeconds);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _direction = _direction.normalized;
        }

        if(inputDirection.magnitude >= 0.1f && !_dashing) {

            if(Input.GetButtonDown("Dash") && _dashAvailable){
                StartDash();
            } else {
                _characterController.Move(_direction * _playerSpeed * Time.deltaTime);
            }
        }
    }

    private void StartDash() {
        _dashing = true;
        _dashAvailable = false;
        OnDashUnavailable?.Invoke();
        StartCoroutine(DashCoolDown());
        StartCoroutine(Dash());
    }
    
    private IEnumerator Dash(){
        _dashStartTime = Time.time;
        var dashEndTime = _dashStartTime + _dashDurationSeconds;
        while(Time.time < dashEndTime) {
            _characterController.Move(_direction * _playerSpeed * Time.deltaTime * _dashSpeedMultiplier);
            yield return new WaitForEndOfFrame();
        }
        _dashing = false;
    }

    private IEnumerator DashCoolDown(){
        var cooldownStart = Time.time;
        var cooldownEndTime = cooldownStart + _dashCooldownSeconds;
        while(Time.time < cooldownEndTime){
            yield return new WaitForFixedUpdate();
        }
        _dashAvailable = true;
        OnDashAvailable?.Invoke();
    }
}

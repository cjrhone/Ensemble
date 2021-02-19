using System.Collections;
using UnityEngine;
using System;
using FMODUnity;

// Most of the movement and camera related code was based off Brackeys' 
// 3rd Person Movement tutorial here: https://youtu.be/4HpC--2iowE
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
    private PlayerGun _playerGun;

    [SerializeField]
    private float _dashSpeedMultiplier = 3.0f;

    [SerializeField]
    private float _dashDurationSeconds = 0.5f;

    [SerializeField]
    private float _dashCooldownSeconds = 2.0f;

    [SerializeField]
    private float _gravity = 9.81f;

    [SerializeField]
    private float _jumpSpeed = 3.5f;

    private Vector3 _direction;
    private bool _dashAvailable;
    private bool _dashing;
    private float _dashStartTime;

    public event Action OnDashAvailable;
    public event Action OnDashUnavailable;

    static public PlayerController Instance;
    //Variable for points -- probably should be seperate script but following this tutorial: https://www.youtube.com/watch?v=V6fB7qmyD1A&ab_channel=Sykoo
    public float points;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start() {
        _dashAvailable = true; //TODO: make this first intiialization controlled by the Unlocking service and Energy management service
        OnDashAvailable?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        HandleShootInput();
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var inputDirection = new Vector3(horizontal, 0f, vertical).normalized;
        _direction.x = inputDirection.x;
        _direction.z = inputDirection.z;

        if (_characterController.isGrounded && !_dashing && Input.GetButtonDown("Jump") ){
            _direction.y = _jumpSpeed;
        }

        _direction.y -= _gravity * Time.deltaTime;

        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var mouseHit = Physics.Raycast(ray, out hit);

        if(!_dashing && mouseHit) {
            var lookAtThis = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Debug.DrawLine(ray.origin, lookAtThis, Color.yellow);
            transform.LookAt(lookAtThis);
        }

        if(/*inputDirection.magnitude >= 0.1f &&*/ !_dashing) {

            if(Input.GetButtonDown("Dash") && _dashAvailable){
                StartDash();
            } else {
                _characterController.Move(_direction * _playerSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleShootInput()
    {
        if(Input.GetButton("Fire1"))
        {
            _playerGun.Shoot();
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

    public void Die()
    {
        print("You've died");
    }
}

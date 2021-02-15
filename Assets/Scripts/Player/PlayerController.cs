using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;
    public float PlayerSpeed = 6f;
    

    [SerializeField]
    private float _dashSpeed;

    [SerializeField]
    private float _dashCooldown;

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f){
            Controller.Move(direction * PlayerSpeed * Time.deltaTime);
        }
        
    }
}

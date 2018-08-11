using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 5f;
    public float RotationSpeed = 8f;
    public float JumpForce = 6f;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _direction;
    private float _speed;
    private bool _jump;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        var stick = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _speed = stick.magnitude;
        _direction = GetDirectionRelativeToCamera(new Vector3(stick.x, 0f, stick.y));
        _jump = Input.GetButtonDown("Jump");

        UpdateAnimator();
    }

    public void FixedUpdate()
    {
        if(_speed > 0.1f)
        {
            _rigidbody.AddForce(_direction * _speed * MovementSpeed * 300f * Time.fixedDeltaTime);
            var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
            _rigidbody.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.fixedDeltaTime));
        }

        if(_jump)
        {
            _rigidbody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void UpdateAnimator()
    {
        _animator.SetBool("walking", _speed > 0.1f);
    }
    
    private Vector3 GetDirectionRelativeToCamera(Vector3 direction)
    {
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;
        return direction.normalized;
    }
}
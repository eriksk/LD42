using System;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
    public float Damping = 0.9f;
    public float FloatForce = 25f;
    private Rigidbody _rigidbody;
    private Quaternion _initialRotation;
    private Vector3 _initialPosition;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _initialRotation = transform.rotation;
        _initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        LockPosition();
        KeepAfloat();        
    }

    private void LockPosition()
    {
        var position = transform.position;
        position.x = _initialPosition.x;
        position.z = _initialPosition.z;
        transform.position = position;
    }

    private void KeepAfloat()
    {
        if(transform.position.y > 0f) return;

        var depth = Mathf.Clamp01(-transform.position.y / 5f);

        var force = -Physics.gravity.y;

        force += FloatForce * depth;
        force -= _rigidbody.velocity.y * Damping;

        _rigidbody.AddForce(Vector3.up * force * _rigidbody.mass);
        transform.rotation = Quaternion.Slerp(transform.rotation, _initialRotation, 0.2f);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _mass;
    [SerializeField] private float _gravity;
    private Vector3 _direction;
    private Vector3 _AccelDirection;
    private float _timer = 0f;
    private float _accelFor = 0f;
    private bool _acceleration = false;
    void Update()
    {
        if (_acceleration) Accel();
        AddForce(Vector3.down * _gravity * Time.deltaTime);
        transform.Translate(_direction * Time.deltaTime,Space.World);
    }

    public void AddForce(Vector3 force)
    {
        force = force / _mass;
        _direction += force;
    }

    public void Accelerate(Vector3 direction ,float accelerationTimer)
    {
        _accelFor = accelerationTimer;
        _AccelDirection = direction;
        _acceleration = true;
    }

    private void Accel()
    {
        if (_accelFor > _timer)
        {
            AddForce(_AccelDirection);
            _timer += Time.deltaTime;
        }
        else
        {
            _timer = 0f;
            _acceleration = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _mass;
    [SerializeField] private float _gravity;
    private Vector3 _direction;

    void Update()
    {
        AddForce(Vector3.down * _gravity * Time.deltaTime);
        transform.Translate(_direction * Time.deltaTime,Space.World);
    }

    public void AddForce(Vector3 force)
    {
        force = force / _mass;
        _direction += force;
    }
}

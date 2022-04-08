using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _timer = 1f;
    [SerializeField] private float _force = 10f;
    [SerializeField] private bool _accelerateBullet;
    [SerializeField] private float _accelerateFor = 2f;
    private float countdown = 0f;
    private Vector3 _direction;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            calculateDirection();
            if(countdown > _timer)
            {
                countdown = 0f;
                GameObject bullet = Instantiate(_bullet, _spawnPoint.transform.position, Quaternion.Euler(_spawnPoint.transform.rotation.eulerAngles), _spawnPoint.transform);
                if(_accelerateBullet) bullet.GetComponent<Bullet>().Accelerate(_direction, _accelerateFor);
                else bullet.GetComponent<Bullet>().AddForce(_direction);
                bullet.transform.parent = null;
                Debug.Log(_direction);
            }
            
        }
        countdown += Time.deltaTime;
    }

    private void calculateDirection()
    {
        _direction = _spawnPoint.transform.position - this.transform.position;
        _direction.Normalize();
        _direction = _direction * _force;
    }
}

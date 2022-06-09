using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [Header("Dependencies")]
    
    [SerializeField] private GravityInteractable _player;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletInstantiator;
    [SerializeField] private CollisionManagerGame _collisionManager;

    [Header("Settings")] 
    [SerializeField] private float _acelerationSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _cadence;
    [SerializeField] private float _bulletSpeed;

    private float _timer = 0f;

    private void FixedUpdate()
    {
        MovePlayer();
        _timer += Time.deltaTime;
    }



    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _player.AddForce(transform.up * _acelerationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            _player.AddForce(-transform.up * _acelerationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _playerObject.transform.Rotate(Vector3.forward,_rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            _playerObject.transform.Rotate(-Vector3.forward, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shootbullet();
        }

    }

    private void Shootbullet()
    {
        if(_timer >= _cadence)
        {
            _timer = 0f;
            GameObject bullet = Instantiate(_bullet,_bulletInstantiator.transform);
            _collisionManager.AddToList(bullet);
            bullet.transform.parent = null;
            bullet.GetComponent<GravityInteractable>().AddForce(bullet.transform.up * _bulletSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Border") || other.CompareTag("Planet") || other.CompareTag("Asteroid"))
        {
            Debug.Log("Destroyed by " + other.tag);
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        Debug.Log("Ship got destroyed");
    }
}

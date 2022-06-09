using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : MonoBehaviour
{

    [Header("Dependencies")]
    
    [SerializeField] private GravityInteractable _player;
    [SerializeField] private GameObject _playerObject;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _bulletInstantiator;
    [SerializeField] private CollisionManagerGame _collisionManager;
    [SerializeField] private TextMeshProUGUI _reloadingStatus;
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _shipModel;
    [SerializeField] private Timer _timerBoard;

    [Header("Settings")] 
    [SerializeField] private float _acelerationSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _cadence;
    [SerializeField] private float _bulletSpeed;

    private float _timer = 0f;
    public bool CanPlay = false;

    private void FixedUpdate()
    {
        if (CanPlay)
        {
            MovePlayer();
            _timer += Time.deltaTime;
            UpdateStatus();
        }
        
    }

    private void Start()
    {
        FinishPlaying();
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

    private void UpdateStatus()
    {
        if (_timer >= _cadence)
        {
            _reloadingStatus.text = "Ready";
            _reloadingStatus.color = Color.green;
        } 
        else
        {
            _reloadingStatus.text = "Reloading";
            _reloadingStatus.color = Color.red;
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
        _player._isGrounded = true;
        _shipModel.SetActive(false);
        Instantiate(_explosion,this.transform.position,Quaternion.Euler(Vector3.zero));
        GameManager.current.LostGame();
        Debug.Log("Ship got destroyed");
    }

    public void StartPlaying()
    {
        CanPlay = true;
        _player._isGrounded = false;
    }

    public void FinishPlaying()
    {
        CanPlay = false;
        _player._isGrounded = true;
    }
}

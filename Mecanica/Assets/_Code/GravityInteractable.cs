using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInteractable : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GravityController gravityController;
    [SerializeField] private CollisionManagerGame collisionManager;

    [Header("Settings")]
    [SerializeField] private bool _canGetGrounded;
    [SerializeField] private bool _orientItself;
    [SerializeField] private float _mass;
    [SerializeField] private bool _applyFriction;
    [SerializeField] private bool _isAsteroid;

    private Vector3 _direction = Vector3.zero;
    public bool _isGrounded = false;
    private float _radius;

    public float Mass { get => _mass; }
    public Vector3 SpeedDirection { get => _direction; }
    public float Radius { get => _radius; }

    public Vector3 Position { get => this.transform.position; }
    

    public void Start()
    {
        gravityController = this.GetComponent<GravityController>();
        Debug.Log(gravityController);
        _radius = this.transform.GetComponent<SphereCollider>().radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        Debug.Log("My radius " + _radius);
    }

    public void Update()
    {
        if(_orientItself)gravityController.Orient(this.gameObject,-gravityController.getClosest(this.transform.position));
        if (gravityController.CollidedWithPlanet(this.transform.position))
        {
            if (_canGetGrounded)
            {
                KillForce();
                this.transform.position = gravityController.onPlanetSurface(this.transform.position);
                _isGrounded = true;
            }
          
        } else if (!_isGrounded)
        {
            if (gravityController != null)
            {
                AddForce(gravityController.calculateGravity(this.transform.position));
                AddForce(gravityController.calculateDrag(_direction));
                transform.Translate(_direction * Time.deltaTime, Space.World);
            }
            //if(gravityController != null) AddForce(gravityController.calculateForces(this.transform.position));
        }
        if (gravityController.CollidedWithPlanet(this.transform.position) && _applyFriction)
        {
            AddForce(gravityController.calculateFriction(_direction,_mass,this.transform.position));
        }
        if (_isGrounded)
        {
            KillForce();
        }
    }

    public void AddForce(Vector3 force)
    {
        _direction += force;
    }

    public void KillForce()
    {
        _direction = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAsteroid && collisionManager != null)
        {
            if (other.CompareTag("Planet") || other.CompareTag("Border") || other.CompareTag("Asteroid"))
            {
                collisionManager.RemoveFromList(this.gameObject);
                collisionManager.AddToScore();
                Debug.Log("Destroyed meteorite " + this.gameObject.name);
                Destroy(this.gameObject);
            }
        }
    }
}

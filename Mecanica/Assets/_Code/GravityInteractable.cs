using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInteractable : MonoBehaviour
{
    [SerializeField] private GravityController gravityController;
    [SerializeField] private bool _canGetGrounded;
    [SerializeField] private bool _orientItself;
    private Vector3 _direction;
    private bool _isGrounded = false;

    public void Start()
    {
        gravityController = this.GetComponent<GravityController>();
        Debug.Log(gravityController);
    }

    public void Update()
    {
        if(_orientItself)gravityController.Orient(this.gameObject,-gravityController.getClosest(this.transform.position));
        if (gravityController.CollidedWithPlanet(this.transform.position) && _canGetGrounded)
        {
            KillForce();
            _isGrounded = true;
        } else if (!_isGrounded)
        {
            if(gravityController != null) AddForce(gravityController.calculateGravity(this.transform.position));
            transform.Translate(_direction * Time.deltaTime, Space.World);
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
}

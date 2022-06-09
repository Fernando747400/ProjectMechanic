using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private GravityInteractable _player;
    [SerializeField] private GameObject _playerObject;

    private Vector3 _direction;


   private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }
    }
}

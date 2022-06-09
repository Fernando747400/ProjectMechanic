using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _player;

    private float _zValue;

    private void Start()
    {
        _zValue = this.transform.position.z;
    }


    void Update()
    {
        this.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _zValue);
    }

  
}

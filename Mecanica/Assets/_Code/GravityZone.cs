using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    private Vector3 position;
    public Vector3 Position
    {
        get { position = this.transform.position; return position; }
    }

    [SerializeField] private float gravity; 
    public float Gravity
    {
        get { return gravity; }
        set { gravity = value; }
    }

}

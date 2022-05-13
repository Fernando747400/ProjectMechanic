using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidZone : MonoBehaviour
{
    private Vector3 position;
    public Vector3 Position
    {
        get { position = this.transform.position; return position; }
    }

    private float parentScale;
    public float ParentScale
    {
        get
        {
            parentScale = this.transform.parent.GetComponent<SphereCollider>().radius * Mathf.Max(transform.parent.lossyScale.x, transform.parent.lossyScale.y, transform.parent.lossyScale.z);
            return parentScale;
        }
    }

    [SerializeField] private float density;
    public float Density
    {
        get { return density; }
        set { density = value; }
    }
}

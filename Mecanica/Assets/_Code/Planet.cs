using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public GameObject cube;
    public void Start()
    {
        Instantiate(cube, this.transform.up, Quaternion.Euler(Vector3.zero));
    }
    void Update()
    {
        Debug.Log(this.transform.up);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcelerationTester : MonoBehaviour
{
    [SerializeField] public GravityInteractable[] test;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach(var obj in test)
            {
                obj.AddForce(new Vector3(0,-4,0));
            }
        }
    }
}

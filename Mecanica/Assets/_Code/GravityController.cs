using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Layer Mask")]
    public LayerMask gravityMask;

    [Header("Debugging")]
    [SerializeField] Logger logger;

    private List<GravityZone> gravityZones;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == gravityMask)
        {
            gravityZones.Add(other.GetComponent<GravityZone>());
            Log("Added " +other.gameObject.name +" to the list");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == gravityMask)
        {
            gravityZones.Remove(other.GetComponent<GravityZone>());
            Log("Removed " + other.gameObject.name + " from the list");
        }
    }

    private Vector3 calculateGravity()
    {
        Vector3 ans = new Vector3(0,0,0);
        if(gravityZones.Count > 0)
        {
            foreach(var G in gravityZones)
            {
                ans += G.Position;
            }
        } else
        {
            ans = Vector3.zero;
        }
        return ans;
    }

    public void getGravity()
    {
       // foreach
    }

    void Log(object message)
    {
        if (logger)
        {
            logger.Log(message, this);
        }
    }
}

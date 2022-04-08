using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Layer Mask")]
    public string gravityTag;

    [Header("Debugging")]
    [SerializeField] Logger logger;

    private List<GravityZone> gravityZones = new List<GravityZone>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(gravityTag))
        {
            gravityZones.Add(other.GetComponent<GravityZone>());
            Log("Added " +other.gameObject.name +" to the list");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(gravityTag))
        {
            gravityZones.Remove(other.GetComponent<GravityZone>());
            Log("Removed " + other.gameObject.name + " from the list");
        }
    }

    public Vector3 calculateGravity(Vector3 gravityObject)
    {
        Vector3 ans = new Vector3(0,0,0);
        Vector3 temp;
        if(gravityZones.Count > 0)
        {
            foreach(var G in gravityZones)
            {
                temp = Vector3.zero;
                temp = G.Position - gravityObject;
                temp.Normalize();
                temp = temp * G.Gravity;
                ans += temp;
            }
            return ans;
        } else
        {
            ans = Vector3.zero;
        }
        return ans;
    }

    public bool CollidedWithPlanet(Vector3 gravityObject)
    {
        if (gravityZones.Count > 0)
        {
            foreach (var G in gravityZones)
            {
                if (Vector3.Distance(gravityObject, G.Position) < G.ParentScale)
                {
                    Log("I'm inside a planet " +G.transform.parent.name);
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    public Vector3 getClosest(Vector3 gravityObject)
    {
        Vector3 closest = Vector3.zero;
        Vector3 temp = Vector3.zero;
        float distance = 0f;
        if (gravityZones.Count > 0)
        {
            distance = Vector3.Distance(gravityObject, gravityZones[0].Position);
            foreach (var G in gravityZones)
            {
                if (Vector3.Distance(gravityObject, G.Position) <= distance)
                {
                    closest = G.Position;
                    distance = Vector3.Distance(gravityObject, G.Position);
                }
            }
            return temp = gravityObject - closest;
        }
        else return temp;
    }

    public void Orient(GameObject gravityObject, Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-gravityObject.transform.up, down) * gravityObject.transform.rotation;
        gravityObject.transform.rotation = Quaternion.Slerp(gravityObject.transform.rotation, orientationDirection, 15f * Time.deltaTime);
    }

    void Log(object message)
    {
        if (logger)
        {
            logger.Log(message, this);
        }
    }
}

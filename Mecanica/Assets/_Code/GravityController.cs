using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Tag")]
    public string gravityTag;

    [Header("Fluid Tag")]
    public string fluidTag;

    [Header("Debugging")]
    [SerializeField] Logger logger;

    private List<GravityZone> gravityZones = new List<GravityZone>();
    private List<FluidZone> fluidZones = new List<FluidZone>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(gravityTag))
        {
            gravityZones.Add(other.GetComponent<GravityZone>());
            Log("Added " +other.gameObject.name +" to the gravity list");
        }

        if (other.gameObject.CompareTag(fluidTag))
        {
            fluidZones.Add(other.GetComponent<FluidZone>());
            Log("Added " + other.gameObject.name + " to the fluid list");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(gravityTag))
        {
            gravityZones.Remove(other.GetComponent<GravityZone>());
            Log("Removed " + other.gameObject.name + " from the gravity list");
        }

        if (other.gameObject.CompareTag(fluidTag))
        {
            fluidZones.Remove(other.GetComponent<FluidZone>());
            Log("Removed " + other.gameObject.name + " from the fluid list");
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

    public Vector3 calculateDrag (Vector3 directionVector)
    {
        Vector3 ans = new Vector3(0, 0, 0);
        float temp;
        if (fluidZones.Count > 0)
        {
            temp = 0;
            foreach (var G in fluidZones)
            {
                temp += G.Density;
            }
            temp = temp / fluidZones.Count;         
            ans = (0.5f * temp * Mathf.Pow((directionVector.magnitude),2) * 0.47f * 1) * Vector3.Normalize(directionVector);
            if (ans.magnitude > directionVector.magnitude)
            {
                return directionVector * -1;
            }
            return ans;
        }
        else
        {
            ans = Vector3.zero;
        }
        return ans;
    }

    public Vector3 calculateFriction(Vector3 directionVector, float mass, Vector3 objectPosition)
    {
        Vector3 friction;
        friction.x = directionVector.x;
        friction.y = directionVector.y;
        friction.z = directionVector.z;
        float weight = calculateGravity(objectPosition).magnitude * mass;
        friction = Vector3.Normalize(friction);
        friction = friction * -1;
        friction = (friction * 0.7f * weight);
        //friction = friction * 2.0f * mass;

        return friction;
    }

    public Vector3 calculateForces(Vector3 Object)
    {
        Vector3 ans = new Vector3(0, 0, 0);
        ans = calculateDrag(calculateGravity(Object));
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

    public Vector3 onPlanetSurface(Vector3 gravityObject)
    {
        Vector3 ans = Vector3.zero;
        ans = gravityObject + (-(getClosest(gravityObject)) / -(getClosestDistance(gravityObject))) * getClosestPlanet(gravityObject).Radius;
        return ans;
    }

    public Vector3 CompensateSurface(Vector3 gravityObject, float radius)
    {
        Vector3 ans = Vector3.zero;
        Vector3 normal = Vector3.zero;
        float depth = 0f;
        float distance = getClosestDistance(gravityObject);
        float radiusB = radius + getClosestPlanet(gravityObject).Radius;
        normal =  getClosestPlanet(gravityObject).Position - gravityObject;
        normal.Normalize();
        depth = radiusB - distance;
        ans = depth * normal;
        return ans;
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

    private float getClosestDistance(Vector3 gravityObject)
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
            return distance;
        }
        else return distance;
    }

    private GravityZone getClosestPlanet(Vector3 gravityObject)
    {
        GravityZone closest = null;
        float distance = 0f;
        if (gravityZones.Count > 0)
        {
            distance = Vector3.Distance(gravityObject, gravityZones[0].Position);
            foreach (var G in gravityZones)
            {
                if (Vector3.Distance(gravityObject, G.Position) <= distance)
                {
                    closest = G;
                    distance = Vector3.Distance(gravityObject, G.Position);
                }
            }
            return closest;
        }
        else return closest;
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

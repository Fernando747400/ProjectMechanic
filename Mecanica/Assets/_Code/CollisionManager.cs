using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
   private GravityInteractable[] Colliders;
    [SerializeField] private float _elasticity = 1.0f;

    private void Start()
    {
        Colliders = GameObject.FindObjectsOfType<GravityInteractable>();
    }

    private void Update()
    {
        CheckColissions();
    }

    private bool CheckLength()
    {
        if (Colliders.Length > 1)
        {
            return true;
        }
        else return false;
         
    }

    void CheckColissions()
    {
        GravityInteractable objectOne;
        GravityInteractable objectTwo;
        if (CheckLength())
        {
            for (int i = 0; i < Colliders.Length -1; i++)
            {
                objectOne = Colliders[i];
                for (int j = i+1; j < Colliders.Length; j++)
                {
                    objectTwo = Colliders[j];
                    if(Vector3.Distance(objectOne.Position, objectTwo.Position) < objectOne.Radius + objectTwo.Radius)
                    {
                        CalculateCollisions(objectOne,objectTwo);
                    }
                }
            }
        }
    }

    private void CalculateCollisions(GravityInteractable objectOne, GravityInteractable objectTwo)
    {
        Vector3 Direction = objectOne.Position - objectTwo.Position;
        float DotOne = Vector3.Dot(objectOne.SpeedDirection,Direction);
        float DotTwo = Vector3.Dot(objectTwo.SpeedDirection,Direction);

        float VPrime = (((objectOne.Mass * objectOne.SpeedDirection.magnitude) + (objectTwo.Mass * objectTwo.SpeedDirection.magnitude)
            - objectTwo.Mass * _elasticity * (objectOne.SpeedDirection.magnitude - objectTwo.SpeedDirection.magnitude))
            / (objectOne.Mass + objectTwo.Mass));

        float AcelOne = VPrime - DotOne;
        float AcelTwo = (objectOne.Mass / objectTwo.Mass) * AcelOne;

        Vector3 vOne = DotOne * Direction.normalized;
        Vector3 vTwo = DotTwo * Direction.normalized;

        Vector3 AcelVectorOne = AcelOne * Direction.normalized;
        Vector3 AcelVectorTwo = AcelTwo * Direction.normalized;

        Vector3 finalOne = vOne + AcelVectorOne;
        Vector3 finalTwo = vTwo + AcelVectorTwo;

        objectOne.AddForce(finalOne);
        objectTwo.AddForce(-finalTwo);
    }
}

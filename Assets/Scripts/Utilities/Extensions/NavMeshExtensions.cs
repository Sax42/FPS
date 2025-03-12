using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshExtensions: MonoBehaviour
{
    public static NavMeshHit ClosestNavMeshPos(Vector3 pPos,float pMaxDistance,int pAreaMask)
    {
        NavMeshHit lHit;
        NavMesh.SamplePosition(pPos,out lHit,pMaxDistance,pAreaMask);
        return lHit;
    }

    public static NavMeshHit ClosestNavMeshPos(Vector3 pPos,float pMaxDistance)
    {
        NavMeshHit lHit;
        int allAreasMask = NavMesh.AllAreas;

        NavMesh.SamplePosition(pPos,out lHit,pMaxDistance,allAreasMask);
        return lHit;
    }
}
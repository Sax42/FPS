using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExtension: MonoBehaviour
{
    public static Vector3 RandomPointInSphere(Vector3 pPos,float pRadius)
    {
        Vector3 lGeneratedPos = pPos + Random.insideUnitSphere * pRadius;
        return lGeneratedPos;
    }

    public static void MeshPickRandomVertex()
    {
    }

    public static int RandomInRange(int pCount)
    {
        int randomSeed = Random.Range(0,15451151);
        Random.InitState(randomSeed);
        int lValue = Random.Range(0,pCount);
        return lValue;
    }
}
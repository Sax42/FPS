using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Force
{
    public Force()
    { }

    public Force(Vector3 pDirection,float pStrength,float pDropStrength)
    {
        direction = pDirection;
        strength = pStrength;
        dropStrength = pDropStrength;
    }

    public Force(Vector3 pDirection,float pStrength,float pDropStrength,float pMaxStrength)
    {
        direction = pDirection;
        strength = pStrength;
        dropStrength = pDropStrength;
        maxStrength = pMaxStrength;
    }

    public Vector3 GetVelocity()
    {
        return strength * direction;
    }

    public string name = null;
    public Vector3 direction;
    public float strength;
    public float dropStrength;
    public AnimationCurve increaseCurve;
    public float maxStrength = -1;
    public AnimationCurve dropCurve;

    public Vector3 Compute()
    {
        Vector3 lVelocity = Vector3.zero;
        strength -= strength * dropStrength;
        strength = Mathf.Clamp(strength,0,maxStrength > 0 ? maxStrength : 10000000);
        lVelocity = direction * strength;
        return lVelocity;
    }
}

public class PhysicHandler: MonoBehaviour
{
    public List<Force> allForces = new List<Force>();
    public Vector3 finalVelocity = Vector3.zero;
    public Force gravity;

    public void GravityHandle()
    {
        AddForce(gravity);
    }

    public void AddForce(Force pForce)
    {
        Force lForce = pForce.DeepCopy();
        if(lForce.name != null)
        {
            int lIndex = GetForceIndex(lForce.name);
            if(lIndex != -1)
            {
                allForces[lIndex] = lForce;
                return;
            }
        }
        allForces.Add(lForce);
    }

    public void SetForce(Force pForce)
    {
        Force lForce = pForce;
        Force lPreviousForce;
        if(pForce.name != null)
        {
            int lIndex = GetForceIndex(pForce.name);
            if(lIndex != -1)
            {
                lPreviousForce = allForces[lIndex];
                allForces[lIndex] = pForce;
                allForces[lIndex].direction += lPreviousForce.direction;
                allForces[lIndex].strength += lPreviousForce.strength;
                return;
            }
        }
        allForces.Add(lForce);
    }

    public int GetForceIndex(string pName)
    {
        int lLength = allForces.Count;
        Force lForce;
        int lIndex = -1;
        for(int i = 0 ; i < lLength ; i++)
        {
            lForce = allForces[i];
            if(lForce.name == pName)
            {
                lIndex = i;
                break;
            }
        }
        return lIndex;
    }

    public Force GetForce(int pIndex)
    {
        if(pIndex >= allForces.Count)
            return null;
        return allForces[pIndex];
    }

    public void ForceCompute()
    {
        int lLength = allForces.Count;
        Force lForce;
        finalVelocity = Vector3.zero;
        Vector3 lVelocity;
        for(int i = lLength - 1 ; i >= 0 ; i--)
        {
            lForce = allForces[i];
            lForce.Compute();
            if(lForce.strength <= 0)
            {
                allForces.RemoveAt(i);
                continue;
            }
            finalVelocity += lForce.GetVelocity();
        }
    }

    public void Movement()
    {
        transform.position += finalVelocity * Time.deltaTime;
    }

    private void Start()
    {
        gravity = new Force(Vector3.down,9.81f,0.1f,9.81f);
        gravity.name = "gravity";
        AddForce(gravity);
    }

    private void Update()
    {
        GravityHandle();
        ForceCompute();
        Movement();
    }
}

public static class ForceExtensions
{
    public static Force DeepCopy(this Force original)
    {
        if(original == null)
            return null;
        Force copy = new Force(original.direction,original.strength,original.dropStrength,original.maxStrength);
        copy.name = original.name;
        copy.increaseCurve = original.increaseCurve;
        copy.dropCurve = original.dropCurve;
        return copy;
    }
}
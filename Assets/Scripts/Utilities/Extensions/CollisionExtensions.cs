using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionEventType
{ OnEnter, OnExit, OnStay }

public enum ColliderType
{ Trigger, Collision }

public enum ShapeType
{
    Sphere, Box
}

[Serializable]
public class ColliderCreationParam
{
    public Vector3 center;
    public Quaternion rotation;
}

[Serializable]
public class BoxColliderCreationParam: ColliderCreationParam
{
    public Vector3 size;
}

[Serializable]
public class SphereColliderCreationParam: ColliderCreationParam
{
    public float radius;
}

public class CollisionExtensions: MonoBehaviour
{
    public static List<RaycastHit> RaycastToObjects(Vector3 pStartPos,List<GameObject> pObjectToCheck)
    {
        List<RaycastHit> lHitList = new List<RaycastHit>();
        int lLength = pObjectToCheck.Count;
        GameObject lObject;
        RaycastHit lHit;
        Ray lRay;
        Vector3 lDirection;
        for(int i = 0 ; i < lLength ; i++)
        {
            lObject = pObjectToCheck[i];
            lDirection = VectorExtensions.Direction(pStartPos,lObject.transform.position);

            Physics.Raycast(pStartPos,lDirection.normalized,out lHit,lDirection.magnitude);
            lHitList.Add(lHit);
        }

        return lHitList;
    }

    public static Collider AddColliderToObject(GameObject pObject)
    {
        Collider lCollider = pObject.AddComponent<Collider>();
        return lCollider;
    }

    public static BoxCollider CreateCubeCollider(Vector3 pPos,GameObject pObject,Vector3 pSize)

    {
        BoxCollider boxCollider = pObject.AddComponent<BoxCollider>();
        boxCollider.size = pSize;
        boxCollider.center = pPos;
        return boxCollider;
    }

    public static Collider CreateSphereCollider(Vector3 pPos,GameObject pObject,float pRadius)
    {
        SphereCollider lSphereCollider = pObject.AddComponent<SphereCollider>();
        lSphereCollider.center = pPos;
        lSphereCollider.radius = pRadius;
        return lSphereCollider;
    }

    public static List<Collider> GetOverlap(BoxColliderCreationParam pColliderCreationParam)
    {
        BoxColliderCreationParam lColliderCreationParam = (BoxColliderCreationParam)pColliderCreationParam;
        Collider[] hitColliders;
        hitColliders = Physics.OverlapBox(lColliderCreationParam.center,lColliderCreationParam.size,lColliderCreationParam.rotation);

        List<Collider> lAllColliders = ListExtension.ArrayToList(hitColliders);
        return lAllColliders;
    }

    public static List<GameObject> CheckForNearbyObject(ColliderCreationParam pColliderCreationParam,ShapeType pType)
    {
        List<Collider> lColliderList = new List<Collider>();
        List<GameObject> lAllGameObjects = new List<GameObject>();
        if(pType == ShapeType.Box)
            lColliderList = GetOverlap(pColliderCreationParam as BoxColliderCreationParam);
        else if(pType == ShapeType.Sphere)
            lColliderList = GetOverlap(pColliderCreationParam as SphereColliderCreationParam);

        lAllGameObjects = GetGameObjectFromColliders(lColliderList);
        return lAllGameObjects;
    }

    public static List<Collider> GetOverlap(SphereColliderCreationParam pColliderCreationParam)
    {
        SphereColliderCreationParam lColliderCreationParam = (SphereColliderCreationParam)pColliderCreationParam;
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(lColliderCreationParam.center,pColliderCreationParam.radius);

        List<Collider> lAllColliders = ListExtension.ArrayToList(hitColliders);
        return lAllColliders;
    }

    public static List<GameObject> GetGameObjectFromColliders(List<Collider> pInputColliders)
    {
        List<GameObject> lAllGameObjects = new List<GameObject>();
        int lLength = pInputColliders.Count;
        Collider lCollider;
        for(int i = 0 ; i < lLength ; i++)
        {
            lCollider = pInputColliders[i];
            lAllGameObjects.Add(lCollider.gameObject);
        }

        return lAllGameObjects;
    }

    public static Ray MouseCameraRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray;
    }

    public static RaycastHit GetRayData(Ray pRay)
    {
        RaycastHit lRaycastHitData;
        Physics.Raycast(pRay,out lRaycastHitData);
        return lRaycastHitData;
    }
}
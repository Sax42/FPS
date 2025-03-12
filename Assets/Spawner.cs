using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner: MonoBehaviour
{
    [Header("Object Settings")]
    public GameObject objectPrefab;

    public int maxObjectCount = 10;
    public Vector2 objectScaleRange = new Vector2(0.8f,1.2f);
    public Vector2 objectRotationRange = new Vector2(0f,360f);

    [Header("Spawn Settings")]
    public Vector2 distanceRange = new Vector2(5f,15f);

    public Vector2 timeRange = new Vector2(2f,5f);
    public bool spawnMultipleAtOnce = false;
    public int multipleSpawnCount = 3;
    public Vector2 multipleSpawnRadius = new Vector2(1f,3f);

    private TimeUse lTimer;
    private int currentObjectCount = 0;

    private void Start()
    {
        float spawnTime = Random.Range(timeRange.x,timeRange.y);
        lTimer = TimeUse.AddTimeUse(gameObject,SpawnObjects,TimerState.CallEveryXTime,spawnTime,false);
        lTimer.SetTimeUse(TimerState.CallEveryXTime,SpawnObjects,spawnTime);
    }

    private void SpawnObjects()
    {
        if(currentObjectCount >= maxObjectCount)
        {
            return; // Don't spawn if max count reached
        }

        if(spawnMultipleAtOnce)
        {
            SpawnMultiple();
        }
        else
        {
            SpawnSingle();
        }

        lTimer.SetTimeUse(TimerState.CallEveryXTime,SpawnObjects,Random.Range(timeRange.x,timeRange.y));
    }

    private void SpawnSingle()
    {
        if(currentObjectCount >= maxObjectCount)
            return;

        Vector3 spawnPosition = GetRandomNavMeshPosition();
        if(spawnPosition != Vector3.zero)
        {
            GameObject newObject = Instantiate(objectPrefab,spawnPosition,GetRandomRotation(),transform);
            ApplyRandomScale(newObject);
            currentObjectCount++;
        }
    }

    private void SpawnMultiple()
    {
        for(int i = 0 ; i < multipleSpawnCount ; i++)
        {
            if(currentObjectCount >= maxObjectCount)
                break;

            Vector3 basePosition = GetRandomNavMeshPosition();
            if(basePosition != Vector3.zero)
            {
                Vector3 randomOffset = Random.insideUnitSphere * Random.Range(multipleSpawnRadius.x,multipleSpawnRadius.y);
                Vector3 spawnPosition = NavMeshExtensions.ClosestNavMeshPos(basePosition + randomOffset,100).position;
                if(spawnPosition == Vector3.zero)
                    continue;

                GameObject newObject = Instantiate(objectPrefab,spawnPosition,GetRandomRotation(),transform);
                ApplyRandomScale(newObject);
                currentObjectCount++;
            }
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 playerPosition = PlayerInput.instance.transform.position;
        Vector3 randomPoint = RandomExtension.RandomPointInSphere(playerPosition,Random.Range(distanceRange.x,distanceRange.y));
        Vector3 navMeshPosition = NavMeshExtensions.ClosestNavMeshPos(randomPoint,100).position;

        if(navMeshPosition == Vector3.zero)
        {
            Debug.LogWarning("No NavMesh position found within range.");
        }

        return navMeshPosition;
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0,Random.Range(objectRotationRange.x,objectRotationRange.y),0);
    }

    private void ApplyRandomScale(GameObject spawnedObject)
    {
        float scale = Random.Range(objectScaleRange.x,objectScaleRange.y);
        spawnedObject.transform.localScale = new Vector3(scale,scale,scale);
    }

    public void ObjectDestroyed()
    {
        currentObjectCount--;
    }
}
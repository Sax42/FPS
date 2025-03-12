using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner: MonoBehaviour
{
    public Vector2 enemyRange;
    public Vector2 distanceRange;
    public Vector2 timeRange;

    public GameObject enemyPrefab;

    private TimeUse lTimer;

    // Start is called before the first frame update
    private void Start()
    {
        lTimer = TimeUse.AddTimeUse(gameObject,SpawnEnemies,TimerState.CallEveryXTime,timeRange.x,false);
        lTimer.SetTimeUse(TimerState.CallEveryXTime,SpawnEnemies,timeRange.x);
    }

    private void SpawnEnemies()
    {
        Vector3 lPos = NavMeshExtensions.ClosestNavMeshPos(
             RandomExtension.RandomPointInSphere(PlayerInput.instance.transform.position,distanceRange.x),100).position;
        Instantiate(enemyPrefab,lPos,Quaternion.identity,transform);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy: Living
{
    private NavMeshAgent agent;
    public float updateTime = 5f;
    private TimeUse lTimer;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
        lTimer = TimeUse.AddTimeUse(gameObject,UpdateDestination,TimerState.CallEveryXTime,updateTime,false);
        lTimer.SetTimeUse(TimerState.CallEveryXTime,UpdateDestination,updateTime);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();

        if(agent.isStopped)
        {
            UpdateDestination();
        }
    }

    private void UpdateDestination()
    {
        agent.SetDestination(PlayerInput.instance.transform.position);
    }
}
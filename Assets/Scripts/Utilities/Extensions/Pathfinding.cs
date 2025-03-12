using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfinding: MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 50;

    [SerializeField] private float _MaxAngularSpeed = 50;
    [SerializeField] private float _MaxAccelerationSpeed = 50;
    [SerializeField] private float _StoppingDistance = 50;

    [Header("PathFinding")]
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private Vector3 target;

    public bool active = false;

    public void SetTarget(Vector3 pTarget,bool pSetActive = true)
    {
        target = pTarget;
        if(pSetActive)
            SetActive(true);
        agent?.SetDestination(target);
    }

    public void SetActive(bool pActive)
    {
        active = pActive;
    }

    private void Awake()
    {
        SetNavMeshAgent();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        /*if(active)
            AgentCompute();*/
    }

    private void AgentCompute()
    {
    }

    private void SetNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null)
            agent = gameObject.AddComponent<NavMeshAgent>();
        /*agent.speed = speed;
        agent.angularSpeed = _MaxAngularSpeed;
        agent.acceleration = _MaxAccelerationSpeed;
        agent.stoppingDistance = _StoppingDistance;*/
    }
}
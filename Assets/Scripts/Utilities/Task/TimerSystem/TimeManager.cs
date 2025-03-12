using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeedEventParams: EventArgs
{
    public float newTimeScale;
}

public class TimeManager: MonoBehaviour
{
    private float _TimeSpeed = 1f;
    private float elapsedTime = 0f;
    public EventHandler OnTimeScaleChange;
    public TimeSpeedEventParams timeSpeedEventParams = new TimeSpeedEventParams();
    public List<TimeUse> allActiveTimeUse = new List<TimeUse>();

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public void TimeUseMachine()
    {
        float lTargetTime;
        foreach(TimeUse lTimeUse in allActiveTimeUse)
        {
            if(!lTimeUse.CheckIfActive())
                continue;
            lTargetTime = lTimeUse.apparitionTime + lTimeUse.delay;

            if(elapsedTime >= lTargetTime)
            {
                lTimeUse.FireEvent();
                Debug.Log("timer ended");
                continue;
            }
        }
    }

    public void SetTimeSpeed(float pSpeed)
    {
        _TimeSpeed = pSpeed;
        timeSpeedEventParams.newTimeScale = pSpeed;
        OnTimeScaleChange(this,new EventArgs());
    }

    public float GetTimeSpeed()
    {
        return _TimeSpeed;
    }

    public void AddTimeUse()
    {
    }

    private static TimeManager _Instance;

    public static TimeManager GetInstance()
    {
        if(_Instance is null)
        {
            GameObject lObject = Instantiate(new GameObject());

            _Instance = lObject.AddComponent<TimeManager>();
        }
        return _Instance;
    }

    private void Awake()
    {
        if(_Instance is null)
            _Instance = this;
    }

    private void Update()
    {
        TimeUseMachine();
        elapsedTime += Time.deltaTime;
    }
}
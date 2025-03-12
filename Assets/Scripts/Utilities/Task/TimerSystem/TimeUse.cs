using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimerState
{
    None, CallEveryXTime
}

public class TimeUse: MonoBehaviour
{
    public TimeManager TimeManager;
    public bool activated;
    public TimerState state;
    public float delay;
    public float apparitionTime;
    public EventHandler OnEventFired;
    public float timeScale;
    public Action MethodCall;
    public bool destroyAfterUse = false;

    private void Awake()
    {
        TimeManager = TimeManager.GetInstance();
        TimeManager.OnTimeScaleChange += OnTimeScaleChange;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void FireEvent()
    {
        if(MethodCall != null)
            MethodCall();
        OnEventFired?.Invoke(this,new EventArgs());
        SetActive(false);
        if(destroyAfterUse)
        {
            Destroy(this);
            return;
        }

        if(state == TimerState.CallEveryXTime)
        {
            SetTimeUse(state,MethodCall,delay,destroyAfterUse);
        }
    }

    private void OnDestroy()
    {
        TimeManager.allActiveTimeUse.Remove(this);
    }

    private void OnTimeScaleChange(object pSender,EventArgs pEventArgs)
    {
        timeScale = TimeManager.GetTimeSpeed();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void SetActive(bool pActive)
    {
        if(pActive)
        {
            apparitionTime = TimeManager.GetElapsedTime();
        }

        activated = pActive;
    }

    public bool CheckIfActive()
    {
        return activated;
    }

    public static TimeUse AddTimeUse(GameObject pContainer,Action pMethodToCall,TimerState pState,float pDelay,bool pDestroyAfterUse,bool pAutostart = false)
    {
        TimeUse lTimeUse = pContainer.AddComponent<TimeUse>();
        TimeManager.GetInstance().allActiveTimeUse.Add(lTimeUse);
        if(pAutostart)
            lTimeUse.SetTimeUse(pState,pMethodToCall,pDelay,pDestroyAfterUse);

        return lTimeUse;
    }

    public void SetTimeUse(TimerState pState,Action pMethodToCall,float pDelay,bool pDestroyAfterUse = false)
    {
        MethodCall = pMethodToCall;
        state = pState;
        destroyAfterUse = pDestroyAfterUse;
        delay = pDelay;
        apparitionTime = TimeManager.GetElapsedTime();
        SetActive(true);
    }
}
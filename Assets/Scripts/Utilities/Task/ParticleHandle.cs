using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandle: MonoBehaviour
{
    private ParticleSystem system;

    public bool autoDelete = true;

    private float totalDuration;

    // Start is called before the first frame update
    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        if(system == null)
            system = GetComponentInChildren<ParticleSystem>();
        system.Play();
        totalDuration = GetParticleSystemTotalDuration(system);
        if(system.main.loop)
        {
            if(autoDelete)
            {
                Invoke(nameof(Destroy),totalDuration);
            }
        }
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public static float GetParticleSystemTotalDuration(ParticleSystem particleSystem)
    {
        if(particleSystem == null)
            return 0f;

        ParticleSystem.MainModule main = particleSystem.main;

        // For looping systems, return the duration of one cycle
        if(main.loop)
            return main.duration;

        // For non-looping systems: duration + max particle lifetime
        float maxLifetime = main.startLifetime.constant;

        if(main.startLifetime.mode == ParticleSystemCurveMode.TwoConstants)
            maxLifetime = Mathf.Max(main.startLifetime.constantMax,main.startLifetime.constantMin);
        else if(main.startLifetime.mode == ParticleSystemCurveMode.Curve)
            maxLifetime = main.startLifetime.curveMultiplier;

        return main.duration + maxLifetime;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Profiling;

public class Physic: MonoBehaviour
{
    public float gravity;
    private Func<float> linkedMethod; // Stores reference to a method
    public string velocityMethodName;

    public float velocity => linkedMethod != null ? linkedMethod() : 0f; // Dynamically fetches value

    public void SetupLinkedVelocity(string pMethodToFetch,object pValueProvider)
    {
        if(pValueProvider == null)
            return;
        MethodInfo method = pValueProvider.GetType().GetMethod(pMethodToFetch);

        if(method != null && method.ReturnType == typeof(float) && method.GetParameters().Length == 0)
        {
            linkedMethod = (Func<float>)Delegate.CreateDelegate(typeof(Func<float>),pValueProvider,method);
            Debug.Log($"Successfully linked {pMethodToFetch} to velocity.");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
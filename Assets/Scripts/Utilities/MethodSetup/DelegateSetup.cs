using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateSetup: MonoBehaviour
{
    /*
     *
     * Follow a method from an other class
     *
     * Class1:
     *
     *
     * public float gravity;
       private Func<float> linkedMethod; // Stores reference to a method

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

     *
     *
     */
}
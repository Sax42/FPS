using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtension
{
    public static T DeepCopy<T>(this T obj)
    {
        if(obj == null)
            return default;
        Type type = obj.GetType();
        T copy = (T)Activator.CreateInstance(type);

        foreach(var field in type.GetFields())
        {
            object fieldValue = field.GetValue(obj);
            field.SetValue(copy,fieldValue is ICloneable cloneable ? cloneable.Clone() : fieldValue);
        }
        return copy;
    }
}
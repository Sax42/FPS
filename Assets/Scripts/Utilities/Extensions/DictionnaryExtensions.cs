using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionnaryExtensions
{
    public static Dictionary<int,T> ListToDictionaryByIndex<T>(List<T> list)
    {
        Dictionary<int,T> dictionary = new Dictionary<int,T>();
        for(int i = 0 ; i < list.Count ; i++)
        {
            dictionary.Add(i,list[i]);
        }
        return dictionary;
    }

    /// <summary>
    /// Converts two lists into a dictionary using the first list as keys and the second as values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    /// <param name="keys">The list containing keys.</param>
    /// <param name="values">The list containing values.</param>
    /// <returns>A dictionary mapping keys to values.</returns>
    /// <exception cref="ArgumentException">Thrown if the lists are of different lengths.</exception>
    public static Dictionary<TKey,TValue> ListToDictionary<TKey, TValue>(List<TKey> keys,List<TValue> values)
    {
        if(keys.Count != values.Count)
        {
            throw new ArgumentException("Keys and values lists must have the same number of elements.");
        }

        Dictionary<TKey,TValue> dictionary = new Dictionary<TKey,TValue>();
        for(int i = 0 ; i < keys.Count ; i++)
        {
            dictionary.Add(keys[i],values[i]);
        }
        return dictionary;
    }
}
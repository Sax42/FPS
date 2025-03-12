using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    // Define your event types
    public enum EventType
    {
        // Collider-related events
        ColliderOnEnter,

        ColliderOnExit,
        ColliderOnStay,

        // Custom or other events
        CustomEvent1,

        CustomEvent2,
        // … add more as needed
    }

    // A dictionary mapping each EventType to a callback (Action) that can receive a parameter.
    private static Dictionary<EventType,Action<object>> eventDictionary = new Dictionary<EventType,Action<object>>();

    /// <summary>
    /// Subscribes a callback to a specific event type.
    /// </summary>
    /// <param name="eventType">The type of event to subscribe to.</param>
    /// <param name="callback">The method to call when the event is triggered.</param>
    public static void Subscribe(EventType eventType,Action<object> callback)
    {
        if(eventDictionary.TryGetValue(eventType,out Action<object> existingCallbacks))
        {
            existingCallbacks += callback;
            eventDictionary[eventType] = existingCallbacks;
        }
        else
        {
            eventDictionary.Add(eventType,callback);
        }
    }

    /// <summary>
    /// Unsubscribes a callback from a specific event type.
    /// </summary>
    /// <param name="eventType">The type of event to unsubscribe from.</param>
    /// <param name="callback">The method to remove.</param>
    public static void Unsubscribe(EventType eventType,Action<object> callback)
    {
        if(eventDictionary.TryGetValue(eventType,out Action<object> existingCallbacks))
        {
            existingCallbacks -= callback;
            if(existingCallbacks == null)
                eventDictionary.Remove(eventType);
            else
                eventDictionary[eventType] = existingCallbacks;
        }
    }

    /// <summary>
    /// Invokes all callbacks subscribed to the given event type.
    /// </summary>
    /// <param name="eventType">The event to trigger.</param>
    /// <param name="parameter">An optional parameter to pass to the callbacks (can be null).</param>
    public static void Invoke(EventType eventType,object parameter = null)
    {
        if(eventDictionary.TryGetValue(eventType,out Action<object> callbacks))
        {
            callbacks?.Invoke(parameter);
        }
    }
}

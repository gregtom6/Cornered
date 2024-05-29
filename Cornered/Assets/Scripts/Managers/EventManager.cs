/// <summary>
/// Filename: EventManager.cs
/// Author: Tamas Gregus
/// Creation Date: 18.05.2024.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private interface IListener { }

    private class Listener<EVENT_TYPE> : IListener where EVENT_TYPE : struct
    {
        private System.Action<EVENT_TYPE> m_Action = delegate { };

        public void AddAction(System.Action<EVENT_TYPE> action)
        {
            m_Action += action;
        }

        public void RemoveAction(System.Action<EVENT_TYPE> action)
        {
            m_Action -= action;
        }

        public void Raise(EVENT_TYPE ev)
        {
            m_Action(ev);
        }
    }

    private Dictionary<System.Type, IListener> m_ListenerDict = new Dictionary<System.Type, IListener>();

    public static void AddListener<EVENT_TYPE>(System.Action<EVENT_TYPE> action) where EVENT_TYPE : struct
    {
        Listener<EVENT_TYPE> listener;
        if (Instance.m_ListenerDict.TryGetValue(typeof(EVENT_TYPE), out IListener listenerInterface))
        {
            listener = listenerInterface as Listener<EVENT_TYPE>;
        }
        else
        {
            listener = new Listener<EVENT_TYPE>();
            Instance.m_ListenerDict.Add(typeof(EVENT_TYPE), listener);
        }

        listener.AddAction(action);
    }

    public static void RemoveListener<EVENT_TYPE>(System.Action<EVENT_TYPE> action) where EVENT_TYPE : struct
    {
        Listener<EVENT_TYPE> listener;
        if (Instance.m_ListenerDict.TryGetValue(typeof(EVENT_TYPE), out IListener listenerInterface))
        {
            listener = listenerInterface as Listener<EVENT_TYPE>;
            listener.RemoveAction(action);
        }
    }

    public static void Raise<EVENT_TYPE>(EVENT_TYPE ev) where EVENT_TYPE : struct
    {
        if (!Instance.m_ListenerDict.ContainsKey(typeof(EVENT_TYPE)))
        {
            return;
        }

        Listener<EVENT_TYPE> listener = Instance.m_ListenerDict[typeof(EVENT_TYPE)] as Listener<EVENT_TYPE>;
        listener.Raise(ev);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

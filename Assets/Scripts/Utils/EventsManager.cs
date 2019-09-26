using System.Collections.Generic;
using UnityEngine;

public class Event { }

public class OnTap:Event
{
    public bool isRight;
    public OnTap(bool pIsRight)
    {
        isRight = pIsRight;
    }
}

public class OnScoreChanged:Event { }

public class OnGameStateChanged:Event { }

public class OnEndGame:Event { }

public class EventsManager
{
    static EventsManager instance = null;
    public static EventsManager Instance {
        get {
            if (instance == null)
            {
                instance = new EventsManager();
            }

            return instance;
        }
    }

    public delegate void EventDelegate<T>(T e) where T : Event;
    private delegate void EventDelegate(Event e);

    private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
    private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();

    //Events.Instance.AddListener<Event>(Event);
    public void AddListener<T>(EventDelegate<T> del) where T : Event
    {
        if (delegateLookup.ContainsKey(del))
            return;

        // Create a new non-generic delegate which calls our generic one.
        // This is the delegate we actually invoke.
        // search "Expressions lambda (Guide de programmation C#)" for details
        EventDelegate internalDelegate = (e) => del((T)e);
        delegateLookup[del] = internalDelegate;

        EventDelegate tempDel;
        if (delegates.TryGetValue(typeof(T), out tempDel))
        {
            delegates[typeof(T)] = tempDel += internalDelegate;
        }
        else
        {
            delegates[typeof(T)] = internalDelegate;
        }
    }

    public void RemoveListener<T>(EventDelegate<T> del) where T : Event
    {
        EventDelegate internalDelegate;
        if (delegateLookup.TryGetValue(del, out internalDelegate))
        {
            EventDelegate tempDel;
            if (delegates.TryGetValue(typeof(T), out tempDel))
            {
                tempDel -= internalDelegate;
                if (tempDel == null)
                {
                    delegates.Remove(typeof(T));
                }
                else
                {
                    delegates[typeof(T)] = tempDel;
                }
            }

            delegateLookup.Remove(del);
        }
    }

    //EventsManager.Instance.Raise(new Event(param = null));
    public void Raise(Event e)
    {
        EventDelegate del;
        if (delegates.TryGetValue(e.GetType(), out del))
        {
            del.Invoke(e);
        }
    }
}


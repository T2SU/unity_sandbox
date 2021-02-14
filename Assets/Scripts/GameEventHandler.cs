using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class GameEventHandler : MonoBehaviour
{
    Dictionary<EventMethod, List<GameEvent>> _events = new Dictionary<EventMethod, List<GameEvent>>();

    public void Handle(EventMethod method)
    {
        if (_events.TryGetValue(method, out var events))
        {
            foreach (var ev in events)
                ev.DoEvent();
        }
    }

    public void Register(EventMethod method, GameEvent evt)
    {
        if (!_events.TryGetValue(method, out var events))
            _events[method] = events = new List<GameEvent>();
        events.Add(evt);
    }
}
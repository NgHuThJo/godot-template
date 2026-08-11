using System;
using System.Collections.Generic;
using System.Linq;
using Game.Common.GameEvents.Base;
using Godot;

namespace Game.Utilities.Autoloads;

public partial class EventBus : Node
{
    public Dictionary<Type, List<Delegate>> EventHandlerDictionary { get; init; } = [];
    public static EventBus Instance { get; private set; }

    public override void _Ready()
    {
        Instance = this;
    }

    public void Subscribe<T>(Action<T> action)
        where T : IGameEvent
    {
        var type = typeof(T);
        if (!EventHandlerDictionary.TryGetValue(type, out _))
        {
            EventHandlerDictionary[type] = [];
        }

        EventHandlerDictionary[type].Add(action);
    }

    public void Unsubscribe<T>(Action<T> action)
        where T : IGameEvent
    {
        var type = typeof(T);
        if (!EventHandlerDictionary.TryGetValue(type, out var list))
        {
            GD.Print($"Game event type {type.Name} has no registered handlers");
            return;
        }

        list.Remove(action);
    }

    public void Publish<T>(T gameEvent)
        where T : IGameEvent
    {
        var type = typeof(T);
        if (!EventHandlerDictionary.TryGetValue(type, out var list))
        {
            GD.Print($"Game event type {type.Name} has no registered handlers");
            return;
        }

        foreach (Action<T> listener in list.Cast<Action<T>>())
        {
            listener(gameEvent);
        }
    }
}

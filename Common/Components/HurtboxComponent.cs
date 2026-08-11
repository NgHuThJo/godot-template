using System;
using Game.Common.GameEvents.Events;
using Godot;

namespace Game.Common.Components;

public partial class HurtboxComponent : Area2D
{
    public event Action<HitReceivedEvent> HitReceived;

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public override void _ExitTree()
    {
        AreaEntered -= OnAreaEntered;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HitboxComponent hitbox)
        {
            var context = new HitReceivedEvent { HitSource = hitbox };

            HitReceived?.Invoke(context);
        }
    }
}

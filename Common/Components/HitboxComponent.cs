using System;
using Game.Common.GameEvents;
using Game.Resources.Attack;
using Godot;

namespace Game.Common.Components;

public partial class HitboxComponent : Area2D
{
    public event Action<HitDoneEvent> HitApplied;
    public AttackData AttackData { get; set; }

    public override void _Ready()
    {
        AreaEntered += OnAreaEntered;
    }

    public void Initialize(AttackData data)
    {
        AttackData = data;
    }

    public void OnAreaEntered(Area2D area)
    {
        if (area is HurtboxComponent hurtbox)
        {
            var context = new HitDoneEvent { Attack = AttackData };

            HitApplied?.Invoke(context);
        }
    }
}

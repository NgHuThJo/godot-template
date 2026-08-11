using Game.Common.GameEvents.Base;
using Game.Resources.Attack;

namespace Game.Common.GameEvents;

public record HitDoneEvent : IGameEvent
{
    public AttackData Attack { get; init; }
}

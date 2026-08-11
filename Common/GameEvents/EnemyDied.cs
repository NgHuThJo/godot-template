using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record EnemyDied : IGameEvent
{
    public int Points { get; init; }
}

using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record HealthChanged : IGameEvent
{
    public required float CurrentHealth { get; init; }
}

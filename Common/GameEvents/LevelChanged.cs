using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record LevelChanged : IGameEvent
{
    public int CurrentLevel { get; init; }
}

using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record ScoreChanged : IGameEvent
{
    public int Score { get; init; }
}

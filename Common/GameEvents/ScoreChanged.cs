namespace Game.Common.GameEvents;

public record ScoreChanged : IGameEvent
{
    public required int Score { get; init; }
}

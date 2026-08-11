using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record BridgeDestroyed : IGameEvent
{
    public int Score { get; set; }
}

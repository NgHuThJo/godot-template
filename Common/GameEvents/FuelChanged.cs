using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public class FuelChanged : IGameEvent
{
    public float Fuel { get; init; }
}

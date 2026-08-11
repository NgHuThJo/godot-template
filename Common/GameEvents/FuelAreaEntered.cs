using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public partial class FuelAreaEntered : IGameEvent
{
    public float FuelUp { get; init; }
}

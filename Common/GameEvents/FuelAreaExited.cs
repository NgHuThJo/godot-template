using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public partial class FuelAreaExited : IGameEvent
{
    public float FuelDown { get; init; }
}

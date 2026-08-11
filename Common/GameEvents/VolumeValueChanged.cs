using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents;

public record VolumeValueChanged : IGameEvent
{
    public string BusName { get; set; }
}

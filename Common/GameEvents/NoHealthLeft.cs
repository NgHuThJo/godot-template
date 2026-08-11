using Game.Common.GameEvents.Base;
using Godot;

namespace Game.Common.GameEvents;

public record NoHealthLeft : IGameEvent
{
    public Node Source { get; set; }
}

using Game.Common.Components;
using Game.Common.GameEvents.Base;

namespace Game.Common.GameEvents.Events
{
    public record HitReceivedEvent : IGameEvent
    {
        public HitboxComponent HitSource;
    }
}

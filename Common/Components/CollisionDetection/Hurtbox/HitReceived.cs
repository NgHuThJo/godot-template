using Game.Common.Components.CollisionDetection.Hitbox;
using Game.Common.GameEvents;

namespace Game.Common.Components.CollisionDetection.Hurtbox;

public record HitReceived : IGameEvent
{
    public HitboxComponent Hitter;
}

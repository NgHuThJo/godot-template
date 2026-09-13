using Godot;

namespace Game.Resources.Combat.Health;

[GlobalClass]
public partial class HealthData : Resource
{
    [Export]
    public float MaxHealth { get; set; }
}

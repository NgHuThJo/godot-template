using Godot;

namespace Game.Utilities.World.Levels;

public partial class SpawnPoint : Node2D
{
    [Export]
    public Marker2D Marker { get; private set; }

    [Export]
    public PackedScene Entity { get; private set; }

    public override void _Ready() { }

    public override void _Process(double delta) { }

    public void SpawnEntity()
    {
        var instance = Entity.Instantiate<Node2D>();

        AddChild(instance);
        instance.GlobalPosition = Marker.GlobalPosition;
        instance.GlobalRotation = Marker.GlobalRotation;
    }
}

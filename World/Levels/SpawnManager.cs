using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Game.Utilities.World.Levels;

public partial class SpawnManager : Node
{
    public List<SpawnPoint> SpawnPoints { get; private set; }

    public override void _Ready()
    {
        var spawnPoints = GetTree().GetNodesInGroup("SpawnPoints").ToList();
    }

    public override void _Process(double delta) { }
}

using Godot;

namespace Game.Utilities.World.Chunks;

public partial class TileMapLayerManager : Node
{
    [Export]
    public TileMapLayer[] Layers = [];
}

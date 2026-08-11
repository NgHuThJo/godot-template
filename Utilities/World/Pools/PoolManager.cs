using Game.Common.ObjectPools;
using Game.Entities.Items.Fuel;
using Godot;

namespace Game.Utilities.Pools;

public partial class PoolManager : Node
{
    [Export]
    public ObjectPool<Fuel> FuelPool { get; private set; }

    public override void _Ready()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnFuel();
        }
    }

    public void SpawnFuel()
    {
        var fuel = FuelPool.GetItem();
        fuel.GlobalPosition = new Vector2 { X = 320, Y = GD.RandRange(0, 800) };
    }
}

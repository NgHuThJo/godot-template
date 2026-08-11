using System.Collections.Generic;
using System.Linq;
using Game.Utilities.Debug;
using Godot;

namespace Game.Utilities.World.Chunks;

public partial class ChunkManager : Node2D
{
    [Export]
    public float ChunkSpeed { get; private set; } = 100;

    [Export]
    public PackedScene[] Chunks { get; set; } = [];

    public Queue<Node2D> ChunkQueue { get; set; } = new();

    public List<PackedScene> ChunkList { get; set; } = [];
    private const int ChunkHeight = 360;

    public override void _Ready()
    {
        foreach (var scene in Chunks)
        {
            ChunkList.Add(scene);
        }

        SpawnMap();

        if (DebugSettings.Instance.SuperSpeed)
        {
            ChunkSpeed = 200;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = Input.GetAxis("move_up", "move_down");

        if (direction > 0)
        {
            var currentDeceleration = 100 * delta;
            ChunkSpeed -= (float)currentDeceleration;
        }
        else if (direction < 0)
        {
            var currentAcceleration = 100 * delta;
            ChunkSpeed += (float)currentAcceleration;
        }

        ChunkSpeed = Mathf.Clamp(ChunkSpeed, 100 * 0.5f, 100 * 1.5f);

        Position += new Vector2 { X = 0, Y = ChunkSpeed * (float)delta };

        WrapChunkAround();
    }

    public void SpawnMap()
    {
        foreach (PackedScene scene in ChunkList)
        {
            var instance = scene.Instantiate<Node2D>();
            CallDeferred(Node.MethodName.AddChild, instance);
            var y = ChunkQueue.Count == 0 ? 0 : -(ChunkQueue.Count * ChunkHeight);

            instance.GlobalPosition = new Vector2 { X = 0, Y = y };
            ChunkQueue.Enqueue(instance);
        }
    }

    public void WrapChunkAround()
    {
        if (ChunkQueue.Peek().GlobalPosition.Y > ChunkHeight)
        {
            var oldChunk = ChunkQueue.Dequeue();
            var last = ChunkQueue.Last();
            oldChunk.QueueFree();

            var scene = ResourceLoader.Load<PackedScene>(oldChunk.SceneFilePath);
            var newChunk = scene.Instantiate<Node2D>();
            AddChild(newChunk);

            newChunk.GlobalPosition = last.GlobalPosition + new Vector2 { X = 0, Y = -ChunkHeight };

            ChunkQueue.Enqueue(newChunk);
        }
    }
}

using Game.Common.GameEvents;
using Game.Common.Persistence;
using Game.Entities.Enemies.Jet;
using Game.Entities.Player;
using Game.UI;
using Game.Utilities.Autoloads;
using Game.Utilities.World.Chunks;
using Godot;
using Utils;

namespace Game.Utilities.World.Levels;

public partial class LevelManager : Node, ISaveable
{
    [Export]
    public Player Player { get; set; }

    [Export]
    public ChunkManager ChunkManager { get; set; }

    [Export]
    public JetSpawner JetSpawner { get; set; }
    private int Score { get; set; } = 0;
    private int Level { get; set; } = 1;

    public override void _EnterTree()
    {
        Player.SetSpawnContainer(this);
        GD.Print($"LEVEL MANAGER ENTER TREE {GetInstanceId()}");
    }

    public override void _Ready()
    {
        UIManager.Instance.HudManager.ResetHud(Player.FuelTank.CurrentFuel, Score, Level);
        JetSpawner.Initialize(ChunkManager);

        GD.Print($"LEVEL MANAGER READY {GetInstanceId()}");
        UIManager.Instance.HudManager.Show();

        EventBus.Instance.Subscribe<PlayerCollided>(OnPlayerCollided);
        EventBus.Instance.Subscribe<EnemyDied>(OnEnemyDied);
        EventBus.Instance.Subscribe<NoFuelLeft>(OnNoFuelLeft);
        EventBus.Instance.Subscribe<BridgeDestroyed>(OnBridgeDestroyed);
    }

    public override void _Process(double delta)
    {
        // GD.Print(
        //     $"OLD LEVEL PROCESS "
        //         + $"id={GetInstanceId()} "
        //         + $"frame={Engine.GetProcessFrames()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );
    }

    public override void _PhysicsProcess(double delta)
    {
        // GD.Print(
        //     $"LEVEL MANAGER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );
    }

    public override void _ExitTree()
    {
        GD.Print($"LEVEL MANAGER EXIT TREE {GetInstanceId()}");
        EventBus.Instance.Unsubscribe<PlayerCollided>(OnPlayerCollided);
        EventBus.Instance.Unsubscribe<EnemyDied>(OnEnemyDied);
        EventBus.Instance.Unsubscribe<NoFuelLeft>(OnNoFuelLeft);
        EventBus.Instance.Unsubscribe<BridgeDestroyed>(OnBridgeDestroyed);
    }

    public void OnEnemyDied(EnemyDied context)
    {
        IncreaseScore(context.Points);

        var newContext = new ScoreChanged { Score = Score };

        EventBus.Instance.Publish(newContext);
    }

    public void OnBridgeDestroyed(BridgeDestroyed context)
    {
        IncreaseScore(context.Score);
        IncrementLevel();

        var scoreContext = new ScoreChanged { Score = Score };
        var levelContext = new LevelChanged { CurrentLevel = Level };

        EventBus.Instance.Publish(scoreContext);
        EventBus.Instance.Publish(levelContext);
    }

    public void OnNoFuelLeft(NoFuelLeft context)
    {
        ShowGameoverScreen();
    }

    public void OnPlayerCollided(PlayerCollided context)
    {
        ShowGameoverScreen();
    }

    public void IncreaseScore(int score)
    {
        Score += score;
    }

    public void IncrementLevel()
    {
        Level++;
    }

    public void ShowGameoverScreen()
    {
        // GD.Print(
        //     $"CREATING GAMEOVER "
        //         + $"physics={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"caller={GetInstanceId()}"
        // );
        SaveManager.Instance.Save();
        var instance = LoadedScenes.GameoverScreen.Instantiate<UIScreen>();
        UIManager.Instance.Push(instance);
        UIManager.Instance.HudManager.Hide();
    }

    public void Save()
    {
        SaveManager.Instance.GameSaveState.Highscore = Mathf.Max(
            SaveManager.Instance.GameSaveState.Highscore,
            Score
        );
        SaveManager.Instance.GameSaveState.HighestLevel = Mathf.Max(
            SaveManager.Instance.GameSaveState.HighestLevel,
            Level
        );
    }
}

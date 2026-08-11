using System;
using Game.Common.Components;
using Game.Common.GameEvents;
using Game.Utilities.Autoloads;
using Godot;

namespace Game.Entities.Player;

public partial class Player : CharacterBody2D
{
    [Export]
    public PlayerController Controller { get; private set; }

    [Export]
    public HealthComponent Health { get; private set; }

    [Export]
    public MovementComponent Movement { get; private set; }

    [Export]
    public AttackComponent Attack { get; private set; }

    [Export]
    public HurtboxComponent Hurtbox { get; private set; }

    [Export]
    public PlayerData Data { get; private set; }
    public PlayerStateMachine StateMachine { get; init; } = new();
    public FuelTank FuelTank { get; private set; } = new(0.5f);
    private Node SpawnContainer { get; set; }
    public float FuelDrain { get; init; } = 0.02f;
    public float FuelUp { get; set; } = 0;
    public int FuelCounter { get; set; } = 0;
    private bool IsDead { get; set; } = false;

    // public override void _EnterTree()
    // {
    //     GD.Print(
    //         $"PLAYER ENTER TREE "
    //             + $"physics={Engine.GetPhysicsFrames()} "
    //             + $"process={Engine.GetProcessFrames()}"
    //     );
    // }

    public override void _Ready()
    {
        // GD.Print($"PLAYER READY {GetInstanceId()}");
        Initialize();

        StateMachine.ChangeState(new PlayerIdleState(this, StateMachine));
        EventBus.Instance.Subscribe<FuelAreaEntered>(OnFuelAreaEntered);
        EventBus.Instance.Subscribe<FuelAreaExited>(OnFuelAreaExited);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (IsDead)
        {
            return;
        }

        // GD.Print(
        //     $"PLAYER PHYSICS "
        //         + $"frame={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()} "
        //         + $"id={GetInstanceId()} "
        //         + $"inside={IsInsideTree()} "
        //         + $"queued={IsQueuedForDeletion()}"
        // );

        if (FuelTank.IsEmpty())
        {
            IsDead = true;

            var context = new NoFuelLeft();
            EventBus.Instance.Publish(context);
            return;
        }

        var direction = Controller.MovementDirection;
        Movement.ApplyHorizontalVelocity(direction.X);
        Movement.ApplyGravity(delta);
        MoveAndSlide();

        if (GetSlideCollisionCount() > 0)
        {
            IsDead = true;

            var context = new PlayerCollided { };

            EventBus.Instance.Publish(context);
            return;
        }

        if (Controller.IsShooting)
        {
            Attack.Attack();
        }

        StateMachine.Update(delta);
    }

    public override void _ExitTree()
    {
        // GD.Print(
        //     $"PLAYER EXIT "
        //         + $"physics={Engine.GetPhysicsFrames()} "
        //         + $"process={Engine.GetProcessFrames()}"
        // );
        EventBus.Instance.Unsubscribe<FuelAreaEntered>(OnFuelAreaEntered);
        EventBus.Instance.Unsubscribe<FuelAreaExited>(OnFuelAreaExited);
    }

    public void Initialize()
    {
        Health.Initialize(Data.HealthData);
        Movement.Initialize(Data.MovementData);
        Attack.Initialize(Data.AttackData, SpawnContainer);
    }

    public void SetSpawnContainer(Node spawnContainer)
    {
        SpawnContainer = spawnContainer;
    }

    public void OnFuelAreaEntered(FuelAreaEntered context)
    {
        FuelUp += context.FuelUp;
        FuelCounter++;

        StateMachine.ChangeState(new PlayerRefuelState(this, StateMachine));
    }

    public void OnFuelAreaExited(FuelAreaExited context)
    {
        FuelUp -= context.FuelDown;
        FuelCounter--;

        if (FuelCounter == 0)
        {
            FuelUp = 0;
            StateMachine.ChangeState(new PlayerIdleState(this, StateMachine));
        }
    }
}

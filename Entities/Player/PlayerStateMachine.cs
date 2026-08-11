using Game.Common.GameEvents;
using Game.Common.StateMachines;
using Game.Utilities.Autoloads;
using Godot;

namespace Game.Entities.Player;

public abstract class PlayerState(Player player, PlayerStateMachine stateMachine) : IState
{
    public Player Player { get; init; } = player;
    public PlayerStateMachine StateMachine { get; protected set; } = stateMachine;

    public virtual void Enter() { }

    public virtual void Exit() { }

    public virtual void Input(InputEvent @event) { }

    public virtual void PhysicsUpdate(double delta) { }

    public virtual void UnhandledInput(InputEvent @event) { }

    public virtual void Update(double delta) { }
}

public class PlayerIdleState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine)
{
    public override void Update(double delta)
    {
        var fuelDrain = Player.FuelDrain * (float)delta;

        Player.FuelTank.ReduceFuel(fuelDrain);

        var context = new FuelChanged { Fuel = Player.FuelTank.CurrentFuel };

        EventBus.Instance.Publish(context);
    }
}

public class PlayerRefuelState(Player player, PlayerStateMachine stateMachine)
    : PlayerState(player, stateMachine)
{
    public override void Update(double delta)
    {
        var fuelUp = Player.FuelUp * (float)delta;

        Player.FuelTank.AddFuel(fuelUp);

        var context = new FuelChanged { Fuel = Player.FuelTank.CurrentFuel };

        EventBus.Instance.Publish(context);
    }
}

public class PlayerStateMachine : StateMachine<PlayerState> { }

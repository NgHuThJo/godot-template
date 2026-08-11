using Godot;

namespace Game.Common.StateMachines;

public interface IState
{
    public void Enter();
    public void Exit();
    public void Input(InputEvent @event);
    public void UnhandledInput(InputEvent @event);
    public void PhysicsUpdate(double delta);
    public void Update(double delta);
    public bool CanTransition(IState current, IState next);
}

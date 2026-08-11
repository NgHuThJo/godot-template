using Game.Common.GameEvents;
using Game.Entities.Items.Fuel;
using Game.Utilities.Autoloads;
using Godot;
using Utils;

namespace Game.UI.HUD;

public partial class HUDManager : CanvasLayer
{
    [Export]
    public FuelDisplay FuelDisplay { get; private set; }

    [Export]
    public ScoreDisplay ScoreDisplay { get; private set; }

    [Export]
    public LevelDisplay LevelDisplay { get; private set; }

    public override void _Ready()
    {
        EventBus.Instance.Subscribe<FuelChanged>(OnFuelChanged);
        Hide();
    }

    public override void _ExitTree()
    {
        EventBus.Instance.Unsubscribe<FuelChanged>(OnFuelChanged);
    }

    public void ShowFuelDisplay()
    {
        FuelDisplay.Show();
    }

    public void HideFuelDisplay()
    {
        FuelDisplay.Hide();
    }

    public void ResetHud(float fuel, int score, int level)
    {
        FuelDisplay.SetFuel(fuel);
        ScoreDisplay.SetScore(score);
        LevelDisplay.SetLevel(level);
    }

    public void OnFuelChanged(FuelChanged context)
    {
        var fuel = context.Fuel;

        FuelDisplay.SetFuel(fuel);
    }
}

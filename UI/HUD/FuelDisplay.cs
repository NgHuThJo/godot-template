using Godot;

namespace Game.UI.HUD;

public partial class FuelDisplay : PanelContainer
{
    [Export]
    public float CurrentFuel { get; private set; } = 0.5f;

    [Export]
    public HSlider Slider { get; private set; }

    public override void _Ready()
    {
        Slider.Value = CurrentFuel;
    }

    public void SetFuel(float fuel)
    {
        CurrentFuel = fuel;
        Slider.Value = CurrentFuel;
    }
}

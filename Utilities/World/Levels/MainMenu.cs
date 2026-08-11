using Game.UI;
using Game.UI.Settings;
using Game.Utilities.Autoloads;
using Godot;
using Utils;

namespace Game.Utilities.World.Levels;

public partial class MainMenu : UIScreen
{
    [Export]
    public Button StartButton { get; set; }

    [Export]
    public Button Settings { get; set; }

    [Export]
    public Button ExitButton { get; set; }

    public override void _Ready()
    {
        StartButton.Pressed += OnStartPressed;
        Settings.Pressed += OnSettingsPressed;
        ExitButton.Pressed += OnExitPressed;
    }

    public override void _ExitTree()
    {
        StartButton.Pressed -= OnStartPressed;
        Settings.Pressed -= OnSettingsPressed;
        ExitButton.Pressed -= OnExitPressed;
    }

    public async void OnStartPressed()
    {
        await TransitionManager.Instance.TransitionToScene<LevelManager>(LoadedScenes.LevelManager);
    }

    public async void OnSettingsPressed()
    {
        var instance = LoadedScenes.Settings.Instantiate<Settings>();

        UIManager.Instance.Push(instance);
    }

    public void OnExitPressed()
    {
        GetTree().Quit();
    }
}

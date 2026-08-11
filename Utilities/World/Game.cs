using Game.Utilities.Autoloads;
using Game.Utilities.World.Levels;
using Godot;

namespace Game.Utilities.World;

public partial class Game : Node
{
    public override void _Ready()
    {
        SceneManager.Instance.ChangeScene<MainMenu>(LoadedScenes.MainMenu);
    }
}

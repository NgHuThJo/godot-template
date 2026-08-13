using Godot;

namespace Game.Utilities.World;

public static class LoadedScenes
{
    public static readonly PackedScene MainMenu = GD.Load<PackedScene>("uid://52bgij52xouq");
    public static readonly PackedScene Settings = GD.Load<PackedScene>("uid://bw4k558m3yh1h");
    public static readonly PackedScene LevelManager = GD.Load<PackedScene>("uid://b08ecx2fw5lju");
    public static readonly PackedScene GameoverScreen = GD.Load<PackedScene>("uid://d3seefhkbo7ky");
    public static readonly PackedScene Jet = GD.Load<PackedScene>("uid://8h5lwffvvsgq");
}

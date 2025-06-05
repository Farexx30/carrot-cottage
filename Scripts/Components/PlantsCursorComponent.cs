using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;
using System.Linq;

namespace CarrotCottage.Scripts.Components;

public partial class PlantsCursorComponent : Node
{
    [Export]
    public TileMapLayer GroundTileMapLayer { get; set; } = default!;

    private const float MaxDistanceToTilledCell = 20.0f;

    private ToolsManager _toolsManager = default!;

    private PackedScene _wheatScene = GD.Load<PackedScene>("res://Scenes/Objects/Plants/Wheat.tscn");
    private PackedScene _tomatoScene = GD.Load<PackedScene>("res://Scenes/Objects/Plants/Tomato.tscn");

    private Player _player = default!;

    private Vector2 _currentMousePosition = Vector2.Zero;
    private Vector2I _currentCellPosition = Vector2I.Zero;
    private int _currentCellSourceId = -1;
    private Vector2 _currentLocalCellPosition = Vector2.Zero;
    private float _currentDistance = 0.0f;

    public override void _Ready()
    {
        _toolsManager = GetNode<ToolsManager>(GlobalNames.ToolsManager);
        _player = (Player)GetTree().GetFirstNodeInGroup("Player");
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed(InputConstants.RemoveObject)
            && _toolsManager.CurrentTool == PlayerTools.Hoe)
        {
            UpdateCurrentCellUnderCursor();
            TryRemovePlant();
        }
        else if (@event.IsActionPressed(InputConstants.Hit)
                && (_toolsManager.CurrentTool == PlayerTools.WheatSeeds
                    || _toolsManager.CurrentTool == PlayerTools.TomatoSeeds))
        {
            UpdateCurrentCellUnderCursor();
            TryAddPlant();
        }
    }

    private void UpdateCurrentCellUnderCursor()
    {
        _currentMousePosition = GroundTileMapLayer.GetLocalMousePosition();
        _currentCellPosition = GroundTileMapLayer.LocalToMap(_currentMousePosition);
        _currentCellSourceId = GroundTileMapLayer.GetCellSourceId(_currentCellPosition);
        _currentLocalCellPosition = GroundTileMapLayer.MapToLocal(_currentCellPosition);
        _currentDistance = _player.GlobalPosition.DistanceTo(_currentLocalCellPosition);

        //GD.Print($"Current Mouse Position: {_currentMousePosition}" +
        //    $" Current Cell Position: {_currentCellPosition}, " +
        //    $" Source ID: {_currentCellSourceId}, " +
        //    $" Distance: {_currentDistance}");
    }

    private void TryAddPlant()
    {
        if (_currentDistance >= MaxDistanceToTilledCell
            || _currentCellSourceId == -1)
        {
            return;
        }

        var plantFields = GetParent()
            .FindChild("PlantFields");

        foreach (var node in plantFields.GetChildren().Cast<Node2D>())
        {
            if (node.GlobalPosition == _currentLocalCellPosition)
            {
                return; // Plant already exists at this position
            }
        }

        GetPlantSceneInstance(out var plantScene);
        var plantInstance = plantScene.Instantiate<Node2D>();
        plantInstance.GlobalPosition = _currentLocalCellPosition;

        plantFields.AddChild(plantInstance);
    }

    private void TryRemovePlant()
    {
        if (_currentDistance >= MaxDistanceToTilledCell)
        {
            return;
        }

        var plantNodes = GetParent()
            .FindChild("PlantFields")
            .GetChildren()
            .Cast<Node2D>();

        foreach (var node in plantNodes)
        {
            if (node.GlobalPosition == _currentLocalCellPosition)
            {
                node.QueueFree();
                break;
            }
        }
    }

    private PackedScene GetPlantSceneInstance(out PackedScene plantScene)
    {
        return plantScene = _toolsManager.CurrentTool switch
        {
            PlayerTools.WheatSeeds => _wheatScene,
            PlayerTools.TomatoSeeds => _tomatoScene,
            _ => default!
        };
    }


}

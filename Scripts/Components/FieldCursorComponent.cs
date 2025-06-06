using CarrotCottage.Scripts.Characters.PlayerScripts;
using CarrotCottage.Scripts.Characters.PlayerScripts.Inputs;
using CarrotCottage.Scripts.Common;
using CarrotCottage.Scripts.Globals;
using Godot;
using System;

namespace CarrotCottage.Scripts.Components;

public partial class FieldCursorComponent : Node
{
    [Export]
    public TileMapLayer OnWaterTileMapLayer { get; set; } = default!;

    [Export]
    public TileMapLayer GroundTileMapLayer { get; set; } = default!;

    [Export]
    public int TerrainSet { get; set; } = 0;

    [Export]
    public int Terrain { get; set; } = 1;

    private const float MaxDistanceToTilledCell = 20.0f;

    private ToolsManager _toolsManager = default!;

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
        if (_toolsManager.CurrentTool != PlayerTools.Hoe)
        {
            return;
        }

        if (@event.IsActionPressed(InputConstants.RemoveObject))
        {
            UpdateCurrentCellUnderCursor();
            TryRemoveTilledSuperWideDirtCell();
        }
        else if (@event.IsActionPressed(InputConstants.Hit))
        {
            UpdateCurrentCellUnderCursor();
            TryAddTilledSuperWideDirtCell();
        }
    }

    private void UpdateCurrentCellUnderCursor()
    {
        _currentMousePosition = OnWaterTileMapLayer.GetLocalMousePosition();
        _currentCellPosition = OnWaterTileMapLayer.LocalToMap(_currentMousePosition);
        _currentCellSourceId = OnWaterTileMapLayer.GetCellSourceId(_currentCellPosition);
        _currentLocalCellPosition = OnWaterTileMapLayer.MapToLocal(_currentCellPosition);
        _currentDistance = _player.GlobalPosition.DistanceTo(_currentLocalCellPosition);

        //GD.Print($"Current Mouse Position: {_currentMousePosition}" +
        //    $" Current Cell Position: {_currentCellPosition}, " +
        //    $" Source ID: {_currentCellSourceId}, " +
        //    $" Distance: {_currentDistance}");
    }
    private void TryAddTilledSuperWideDirtCell()
    {
        if (_currentDistance < MaxDistanceToTilledCell
            && _currentCellSourceId != -1)
        {
            GroundTileMapLayer.SetCellsTerrainConnect(cells: [_currentCellPosition],
                terrainSet: TerrainSet,
                terrain: Terrain,
                ignoreEmptyTerrains: true
            );
        }
    }

    private void TryRemoveTilledSuperWideDirtCell()
    {
        if (_currentDistance < MaxDistanceToTilledCell
            && _currentCellSourceId != -1)
        {
            GroundTileMapLayer.SetCellsTerrainConnect(cells: [_currentCellPosition],
                terrainSet: TerrainSet,
                terrain: -1,
                ignoreEmptyTerrains: true
            );
        }
    }
}

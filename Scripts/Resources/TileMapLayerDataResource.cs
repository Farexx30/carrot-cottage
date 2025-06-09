using Godot;
using Godot.Collections;

namespace CarrotCottage.Scripts.Resources;

public partial class TileMapLayerDataResource : NodeDataResource
{
    [Export]
    public Array<Vector2I> TilemapLayerUsedCells { get; set; } = [];

    [Export]
    public int TerrainSet { get; set; } = 0;

    [Export]
    public int Terrain { get; set; } = 9;

    public override void SaveData(Node2D node)
    {
        base.SaveData(node);

        var tilemapLayer = (TileMapLayer)node;
        Array<Vector2I> cells = tilemapLayer.GetUsedCells();

        TilemapLayerUsedCells = cells;
    }

    public override void LoadData(Window window)
    {
        var tilemapLayer = window.GetNodeOrNull<TileMapLayer>(NodePath);
        tilemapLayer?.SetCellsTerrainConnect(TilemapLayerUsedCells, TerrainSet, Terrain, true);
    }

}
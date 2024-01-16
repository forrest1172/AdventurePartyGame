using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public Vector3Int Position { get; set; }
    public int tileId { get; set; }

    public string tileName { get; set; }
    public double weight { get; set; }

    public TileData(Vector3Int pos, int tileNum, double weight, string tileName)
    {
        this.Position = pos;
        this.tileId = tileNum;
        this.weight = weight - 0.2f;
        this.tileName = tileName;
    }
}
    


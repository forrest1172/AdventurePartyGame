using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public Vector3Int Position { get; set; }
    public double weight { get; set; }

    public string tileName { get; set; }

    public bool isWater {  get; set; }

    public bool isCity { get; set; }

    public int g {  get; set; }
    public int h { get; set; }
    public int f { get; set; }

    public TileData parent;


    public TileData(Vector3Int pos, double weight, string tileName ,bool isWater = false, bool isCity = false)
    {
        this.Position = pos;
        this.tileName = tileName;
        this.weight = weight - 0.2f;
        this.isWater = isWater;
        this.isCity = isCity;
        this.g = int.MaxValue;
        this.h = 0;
        this.f = 0;
        parent = null;

    }
}
    


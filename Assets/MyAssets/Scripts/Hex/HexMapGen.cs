using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class HexMapGen : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public float scale = 5f;
    public double exponent;
    public float fudge_factor;
    [SerializeField]private double power = 0.8;

    public float offsetX = 100;
    public float offsetY = 100;

    public Tilemap worldMap;
    public Tilemap overlay;
    [SerializeField] private int radius = 5; // Map radius in hexes

    [SerializeField] private TileBase[] tile;
    public List<TileData> tileDatas = new List<TileData>();
    private void Start()
    {
        tileDatas.Clear();
        
        worldMap = GameObject.Find("TileMap").GetComponent<Tilemap>();
        overlay = GameObject.Find("OverLayMap").GetComponent<Tilemap>();
        GenerateMap();
    }
    public void GenerateMap()
    {
        tileDatas.Clear();
        worldMap.ClearAllTiles();
        offsetX = UnityEngine.Random.Range(0, 9999);
        offsetY = UnityEngine.Random.Range(0, 9999);

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                // Adjust for hexagonal grid offset
                //int x_offset = y % 2 == 0 ? x : x - 1;

                Vector3Int hexPos = new Vector3Int(x, y, 0);

                //make one big island
                double nx = x / width - 0.5f;
                double ny = y / height - 0.5f;
                double d = 2 * Math.Max(Math.Abs(nx), Math.Abs(ny));
                //calculate elevation
                double e = 1 * CalculateHeight(1 * x, 1 * y)
                + 0.5f * CalculateHeight(2 * x, 2 * y)
                + 0.25f * CalculateHeight(4 * x, 4 * y);
                //raise d to a power. the higher the power the larger the islands
                d = Math.Pow(d, power);
                e = (1 + e - d) / 2;
                double E = Math.Pow(e * fudge_factor, exponent);
                SetTile(hexPos,E);
            }
        }
    }

    private void SetTile(Vector3Int hexPos, double e)
    {
       
        if(e < 0.2f)
        {
            worldMap.SetTile(hexPos, tile[0]);
            TileData tileData = new TileData(hexPos, e, tile[0].name,true);
            tileDatas.Add(tileData);
        }
        else if(e < 0.3f)
        {
            worldMap.SetTile(hexPos, tile[1]);
            TileData tileData = new TileData(hexPos, e, tile[1].name);
            tileDatas.Add(tileData);
        }
        else if(e < 0.4f)
        {
            worldMap.SetTile(hexPos, tile[2]);
            TileData tileData = new TileData(hexPos, e, tile[2].name);
            tileDatas.Add(tileData);
        }
        else if(e < 0.45f)
        {
            worldMap.SetTile(hexPos, tile[3]);
            TileData tileData = new TileData(hexPos, e , tile[3].name);
            tileDatas.Add(tileData);
        }
        else if (e < 0.5f)
        {
            worldMap.SetTile(hexPos, tile[4]);
            TileData tileData = new TileData(hexPos, e, tile[4].name);
            tileDatas.Add(tileData);
        }
        else
        {
            worldMap.SetTile(hexPos, tile[5]);
            TileData tileData = new TileData(hexPos, e, tile[5].name);
            tileDatas.Add(tileData);
        }
       
    }

    float CalculateHeight(double nx, double ny)
    {

        float xCoord = (float)(nx / radius * scale + offsetX);
        float yCoord = (float)(ny / radius * scale + offsetY);
        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    

}

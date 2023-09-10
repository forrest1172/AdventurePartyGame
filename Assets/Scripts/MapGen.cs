using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;


public class MapGen : MonoBehaviour
{
    

    public double width = 10;
    public double height = 10;

    public float scale = 5f;
    public double exponent;
    public float fudge_factor;

    public float offsetX = 100;
    public float offsetY = 100;
    [SerializeField]
    private double power = 0.8;


    public Tilemap tileMap;

    public TileBase[] water;
    public TileBase[] desert;
    public TileBase[] grass;
    public TileBase[] swamp;
    public TileBase[] beach;
    public TileBase[] forest;
    public TileBase[] iceMountains;
    public TileBase[] Cities;
    
    public TileBase[] debugSprite;

    private double e;

    public List<TileData> tileDatas = new List<TileData>();
    
    public float moistureLvl;
    private double m;

    // Start is called before the first frame update
    void Start()
    {
        tileDatas.Clear();
        tileMap.ClearAllTiles();
        Generate();

    }

    public void Generate()
    {
        tileDatas.Clear();
        tileMap.ClearAllTiles();
        moistureLvl = UnityEngine.Random.Range(0f, 0.75f);

        offsetX = UnityEngine.Random.Range(0, 9999);
        offsetY = UnityEngine.Random.Range(0, 9999);
       

        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {

                if (x == width && y == height)
                {
                    Debug.Log("map done...");
                    return;
                }

                double nx = x / width - 0.5f;
                double ny = y / height - 0.5f;


                m = CalculateMoisture(x, y);
                   


                double d = 2 * Math.Max(Math.Abs(nx), Math.Abs(ny));

                e = 1 * CalculateHeight(1 * x, 1 * y)
                + 0.5f * CalculateHeight(2 * x, 2 * y)
                + 0.25f * CalculateHeight(4 * x, 4 * y);
                //raise d to a power. the higher the power the larger the islands
                d = Math.Pow(d, power);
                e = (1 + e - d) / 2;
                m = (1 + m - d) / 2;
                double E = Math.Pow(e * fudge_factor, exponent);



                Biome(E, x, y, m);

            }
        }
    }
    public void Biome(double E, int x, int y, double m)
    {
        if (E < 0.2) tileMap.SetTile(new Vector3Int(x, y, 0), water[0]);

        else if(E < 0.3)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), water[1]);
        }

        else if (E < 0.35)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), beach[0]);
            TileData tileData = new TileData(new Vector3Int(x,y,0), 0, E);
            tileDatas.Add(tileData);
           
            
        }

        else if (E < 0.4)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), grass[0]);
            TileData tileData = new TileData(new Vector3Int(x, y, 0), 1, E);
            tileDatas.Add(tileData);
        }
        else if (E < 0.5)
        {
            
           
                tileMap.SetTile(new Vector3Int(x, y, 0), forest[0]);
                TileData tileData = new TileData(new Vector3Int(x, y, 0), 2, E);
                tileDatas.Add(tileData);


        }
        else if (E < 0.65)
        {
            if(m < 0.2)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), desert[0]);
                TileData tileData = new TileData(new Vector3Int(x, y, 0), 3, E);
                tileDatas.Add(tileData);
            }
            else if (m < 0.5)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), grass[0]);
                TileData tileData = new TileData(new Vector3Int(x, y, 0), 1, E);
                tileDatas.Add(tileData);
            }
            else if (m < 0.65)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), forest[1]);
                TileData tileData = new TileData(new Vector3Int(x, y, 0), 6, E);
                tileDatas.Add(tileData);
            }
           else  if (m < 0.7)
            {
                tileMap.SetTile(new Vector3Int(x, y, 0), swamp[0]);
                TileData tileData = new TileData(new Vector3Int(x, y, 0), 4, E);
                tileDatas.Add(tileData);
            }
        }
        else if (E < 0.8)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), desert[0]);
            TileData tileData = new TileData(new Vector3Int(x, y, 0), 3, E);
            tileDatas.Add(tileData);

        }
        else if (E < 1.5)
        {
            tileMap.SetTile(new Vector3Int(x, y, 0), iceMountains[0]);
            TileData tileData = new TileData(new Vector3Int(x, y, 0), 5, E);
            tileDatas.Add(tileData);

        }

        else tileMap.SetTile(new Vector3Int(x, y, 0), debugSprite[0]);
    }

    float CalculateHeight(double nx, double ny)
    {

        float xCoord = (float)(nx / width * scale + offsetX);
        float yCoord = (float)(ny / height * scale + offsetY);
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
    float CalculateMoisture(double x, double y)
    {

        float xCoord = (float)(x / width * scale + offsetX);
        float yCoord = (float)(y / height * scale + offsetY);
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
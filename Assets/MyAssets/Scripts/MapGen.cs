//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;
//using System;
//using System.Security.Cryptography;
//using Unity.Mathematics;
//using UnityEngine.WSA;
//using Unity.VisualScripting;

//public class MapGen : MonoBehaviour
//{
//    public double width = 10;
//    public double height = 10;

//    public float scale = 5f;
//    public double exponent;
//    public float fudge_factor;

//    public float offsetX = 100;
//    public float offsetY = 100;
//    [SerializeField]
//    private double power = 0.8;

//    [SerializeField]
//    private int numCity_Spawn = 25;

//    public CharacterSpawn spawner;

//    public Tilemap tileMap;

//    public TileBase[] water;
//    public TileBase[] desert;
//    public TileBase[] grass;
//    public TileBase[] swamp;
//    public TileBase[] beach;
//    public TileBase[] forest;
//    public TileBase[] iceMountains;
//    public TileBase[] Cities;

//    public TileBase[] debugSprite;

//    private double e;

//    public List<TileData> tileDatas = new List<TileData>();
//    List<TileData> possCityList = new List<TileData>();

//    private double m;

//    // Start is called before the first frame update
//    void Start()
//    {
//        tileDatas.Clear();
//        tileMap.ClearAllTiles();
//        Generate();

       
//    }

//    public void Generate()
//    {
//        possCityList.Clear();
//        tileDatas.Clear();
//        tileMap.ClearAllTiles();
//        spawner = GetComponent<CharacterSpawn>();

//        offsetX = UnityEngine.Random.Range(0, 9999);
//        offsetY = UnityEngine.Random.Range(0, 9999);


//        for (int x = 0; x < width; x++)
//        {

//            for (int y = 0; y < height; y++)
//            {
                
                
//                double nx = x / width - 0.5f;
//                double ny = y / height - 0.5f;


//                m = CalculateMoisture(x, y);

//                double d = 2 * Math.Max(Math.Abs(nx), Math.Abs(ny));

//                e = 1 * CalculateHeight(1 * x, 1 * y)
//                + 0.5f * CalculateHeight(2 * x, 2 * y)
//                + 0.25f * CalculateHeight(4 * x, 4 * y);
//                //raise d to a power. the higher the power the larger the islands
//                d = Math.Pow(d, power);
//                e = (1 + e - d) / 2;
//                m = (1 + m - d) / 2;
//                double E = Math.Pow(e * fudge_factor, exponent);

//                //Biome(E, x, y, m);

//                if (x == width - 1 && y == height - 1)
//                {
//                    Debug.Log("map done...");
//                    spawner = GetComponent<CharacterSpawn>();
//                    spawner.SpawnChars(0);
//                    AddCities();

//                }

//            }
//        }
//    }

//    //public void Biome(double E, int x, int y, double m)
//    //{
//    //    //if (E < 0.2)
//    ////    {
//    ////        tileMap.SetTile(new Vector3Int(x, y, 0), water[0]);
//    ////        TileData tileData = new TileData(new Vector3Int(x, y, 0), 0, E, water[0].name, true);
//    ////        tileDatas.Add(tileData);
//    ////    }

//    //        //    else if (E < 0.25)
//    //        //    {
//    //        //        tileMap.SetTile(new Vector3Int(x, y, 0), water[1]);
//    //        //        TileData tileData = new TileData(new Vector3Int(x, y, 0), 0, E, water[1].name, true);
//    //        //        tileDatas.Add(tileData);
//    //        //    }

//    //        //    else if (E < 0.3)
//    //        //    {

//    //        //        tileMap.SetTile(new Vector3Int(x, y, 0), beach[0]);
//    //        //        TileData tileData = new TileData(new Vector3Int(x, y, 0), 0, E, beach[0].name);
//    //        //        tileDatas.Add(tileData);

//    //        //    }

//    //        //    else if (E < 0.4)
//    //        //    {

//    //        //        tileMap.SetTile(new Vector3Int(x, y, 0), grass[0]);
//    //        //        TileData tileData = new TileData(new Vector3Int(x, y, 0), 1, E, grass[0].name);
//    //        //        tileDatas.Add(tileData);
//    //        //    }
//    //        //    else if (E < 0.5)
//    //        //    {


//    //        //        tileMap.SetTile(new Vector3Int(x, y, 0), forest[0]);
//    //        //        TileData tileData = new TileData(new Vector3Int(x, y, 0), 2, E, forest[0].name);
//    //        //        tileDatas.Add(tileData);


//    //        //    }
//    //        //    else if (E < 0.6)
//    //        //    {
//    //        //        if (m < 0.2)
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), desert[0]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 3, E, desert[0].name);
//    //        //            tileDatas.Add(tileData);
//    //        //        }
//    //        //        else if (m < 0.4)
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), grass[0]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 1, E, grass[0].name);
//    //        //            tileDatas.Add(tileData);
//    //        //        }
//    //        //        else if (m < 0.5)
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), forest[1]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 6, E, forest[1].name);
//    //        //            tileDatas.Add(tileData);
//    //        //        }
//    //        //        else
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), swamp[0]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 4, E, swamp[0].name);
//    //        //            tileDatas.Add(tileData);
//    //        //        }
//    //        //    }
//    //        //    else if (E < 0.8)
//    //        //    {
//    //        //        if (m < 0.5)
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), desert[0]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 3, E, desert[0].name);
//    //        //            tileDatas.Add(tileData);
//    //        //        }
//    //        //        else
//    //        //        {
//    //        //            tileMap.SetTile(new Vector3Int(x, y, 0), grass[0]);
//    //        //            TileData tileData = new TileData(new Vector3Int(x, y, 0), 2, E, grass[0].name);
//    //        //            tileDatas.Add(tileData);

//    //        //        }

//    //        //    }
//    //        //    else if (E < 1.5)
//    //        //    {
//    //        //        tileMap.SetTile(new Vector3Int(x, y, 0), iceMountains[0]);
//    //        //        TileData tileData = new TileData(new Vector3Int(x, y, 0), 5, E, iceMountains[0].name);
//    //        //        tileDatas.Add(tileData);

//    //        //    }

//    //        //    else tileMap.SetTile(new Vector3Int(x, y, 0), debugSprite[0]);
//    //        //}
//    //}
//        float CalculateHeight(double nx, double ny)
//    {

//        float xCoord = (float)(nx / width * scale + offsetX);
//        float yCoord = (float)(ny / height * scale + offsetY);
//        return Mathf.PerlinNoise(xCoord, yCoord);
//    }
//    float CalculateMoisture(double x, double y)
//    {

//        float xCoord = (float)(x / width * scale + offsetX);
//        float yCoord = (float)(y / height * scale + offsetY);
//        return Mathf.PerlinNoise(xCoord, yCoord);
//    }

////    void AddCities()
////    {



        

////        foreach(TileData tile in tileDatas)
////        {
////            if(tile.isWater == false && tile.tileName != "swamp")
////            {
////                TileData possCityData = tile;
////                possCityList.Add(possCityData);
                

////            }
           
////        }

////        int leng = possCityList.Count;
////        for (int i = 0; i < numCity_Spawn; i++)
////        {
////            int randNum = UnityEngine.Random.Range(0, leng);
////            Vector3Int cityLocal = possCityList[randNum].Position;
////            tileDatas.Remove(possCityList[randNum]);
////            tileMap.SetTile(cityLocal, Cities[0]);
////            //TileData tileData = new TileData(new Vector3Int(cityLocal.x, cityLocal.y, 0), 5, 0f, Cities[0].name,false,true);
////            //tileDatas.Add(tileData);

////        }
        
////    }
////}
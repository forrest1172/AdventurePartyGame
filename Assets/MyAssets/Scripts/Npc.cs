//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class Npc : MonoBehaviour
//{
//    public GameObject manager;
//    private HexMapGen map;
//    [SerializeField]
//    private List<TileData> myNeighbors = new List<TileData>();

//    private void Awake()
//    {
//        manager = GameObject.Find("GameManager");
//        map = manager.GetComponent<MapGen>();
//        FindNeighbors();
//    }
//    private TileData GetTileData(Vector3Int pos)
//    {
//        //pulls the tile at position argument. Could be used to check if tile is water or not as well
//        foreach (TileData tile in manager.GetComponent<MapGen>().tileDatas)
//        {

//            if (tile.Position == pos)
//            {

//                TileData myTile = tile;
//                return myTile;
//            }



//        }
//        return null;

//    }

//    public void FindNeighbors()
//    {
//        Vector3Int myPos = map.tileMap.WorldToCell(this.transform.position);
//        int i = 0;
//        for (int x = -1; x <= 1; x++)
//        {
//            for(int y = -1; y <= 1; y++)
//            {
               
//                TileData tile = GetTileData(new Vector3Int(myPos.x + x,myPos.y + y, 0));
//                if (tile != null)
//                {
                    
//                    myNeighbors.Add(tile);
//                    Debug.Log(myNeighbors[i].Position);
//                    i++;
//                }
//                else
//                {
//                    return;
//                }
                
//            }
//        }
//        Debug.Log(myNeighbors);
//    }
//}

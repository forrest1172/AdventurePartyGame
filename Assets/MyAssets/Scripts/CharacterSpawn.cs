using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] CharactersToSpawn;
    [SerializeField] private HexMapGen map;
    private void Start()
    {
       map = this.GetComponent<HexMapGen>();

    }

    public void SpawnChars()
    {
        //while charactertospawn.length > 0
        //foreach char in charactertospawn
            //spawn(char)
        int index = CharactersToSpawn.Length - 1;
        //Debug.Log(index);
        while(index >= 0)
        {          
            TileData checkedTile = FindSpawnableTile();

            if (checkedTile != null)
            {
                Vector3 spawnPoint = map.worldMap.CellToWorld(checkedTile.Position) - map.worldMap.tileAnchor;
                
                Instantiate(CharactersToSpawn[index], spawnPoint, Quaternion.identity);
                Debug.Log("spawnPoint for " + CharactersToSpawn[index].name + spawnPoint.ToString());
                index--;
                
            }
            else
            {
                continue;
            }
        }
    }

    TileData FindSpawnableTile()
    {
        map = GetComponent<HexMapGen>();
        int randTile = UnityEngine.Random.Range(0, map.tileDatas.Count - 1);
        TileData tileType = map.tileDatas[randTile];
        if (tileType == null)
        {
            return null;
        }
        else if(tileType.isWater == true)
        {
            return null;
        }
        else
        {
            return tileType;
        }
    }
}

////    private void Start()
////    {
////        map = this.GetComponent<MapGen>();

////    }

//    public void SpawnChars(int start)
//    {
//        for (int i = start; i < CharactersToSpawn.Length; i++)
//        {
//            int randTile = Random.Range(0, map.tileDatas.Count);
//            TileData tileType = map.tileDatas[randTile];
//            TileData checkedTile = FindSpawnableTile(tileType);
//            if (checkedTile == null)
//            {
//                int index = i;
//                SpawnChars(index);
//                return;

//            }
//            else
//            {
//                Vector3 spawnPoint = map.tileDatas[randTile].Position;
//                Instantiate(CharactersToSpawn[i], new Vector3(spawnPoint.x, spawnPoint.y + 0.5f), Quaternion.identity);
//                Debug.Log("spawnPoint for " + CharactersToSpawn[i].name + spawnPoint.ToString());
//            }






//        }
//    }
//    TileData FindSpawnableTile(TileData tileType)
//    {
//        if (tileType.isWater == true)
//        {
//            return null;
//        }
//        else
//        {
//            return tileType;
//        }
//    }
//    void Spawn(Vector3 spawnPoint)
//    {

//    }
//}

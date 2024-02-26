//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterSpawn : MonoBehaviour
//{
//    [SerializeField] private GameObject[] CharactersToSpawn;
//    [SerializeField] private HexMapGen map;

//    private void Start()
//    {
//        map = this.GetComponent<MapGen>();

//    }

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

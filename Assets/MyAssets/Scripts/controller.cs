using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.Image;

public class controller : MonoBehaviour
{
    
    private TileData myTile;
    [SerializeField]
    private GameObject Manager;
    private Tilemap map;
    [SerializeField]
    private Camera cam;

    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;

    private Vector3 playerData;
    

    public GameObject selected = null;

    private void Start()
    {
        ResetCamera = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        Manager = GameObject.Find("GameManager");
        map = Manager.GetComponentInChildren<Tilemap>();
    }
    private TileData GetTileData(Vector3Int pos)
    {
        //pulls the tile at position argument. Could be used to check if tile is water or not as well
        foreach (TileData tile in Manager.GetComponent<MapGen>().tileDatas)
        {
            
            if(tile.Position == pos)
            {
              
               myTile = tile;
               return myTile;
            }
           
           
            
        }
        return null;

    }

    void MovePlayer()
    {
        var playerCon = selected.GetComponent<player>();
        Vector3Int playerPos = new Vector3Int(Mathf.RoundToInt(selected.transform.position.x - 0.5f), Mathf.RoundToInt(selected.transform.position.y - 0.5f));

        ResetCamera = new Vector3(playerPos.x + 0.4f, playerPos.y + 0.4f, -5);


        if (playerCon.isSelected == true && playerCon.numOfLandMoves > 0)
        {


            if (Input.GetKeyDown("d"))
            {
                var tileToMove = GetTileData(new Vector3Int(playerPos.x + 1, playerPos.y, 0));
                if (tileToMove.isWater == false)
                {
                    selected.transform.position = new Vector3(selected.transform.position.x + 1, selected.transform.position.y, 0);
                    playerCon.numOfLandMoves -= 1;
                    playerData = playerCon.GetPlayerData();
                    Manager.GetComponent<UiManager>().DisplayPlayerData(playerData);
                }
                else
                {
                    print("Cannot move there!");
                }



            }
            if (Input.GetKeyDown("a"))
            {
                var tileToMove = GetTileData(new Vector3Int(playerPos.x - 1, playerPos.y, 0));
                if (tileToMove.isWater == false)
                {
                    selected.transform.position = new Vector3(selected.transform.position.x - 1, selected.transform.position.y, 0);
                    playerCon.numOfLandMoves -= 1;
                    playerData = playerCon.GetPlayerData();
                    Manager.GetComponent<UiManager>().DisplayPlayerData(playerData);
                }
                else
                {
                    print("Cannot move there!");
                }
            }

            if (Input.GetKeyDown("w"))
            {
                var tileToMove = GetTileData(new Vector3Int(playerPos.x, playerPos.y + 1, 0));
                if (tileToMove.isWater == false)
                {
                    selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y + 1, 0);
                    playerCon.numOfLandMoves -= 1;
                    playerData = playerCon.GetPlayerData();
                    Manager.GetComponent<UiManager>().DisplayPlayerData(playerData);
                }
                else
                {
                    print("Cannot move there!");
                }
            }
            if (Input.GetKeyDown("s"))
            {
                var tileToMove = GetTileData(new Vector3Int(playerPos.x, playerPos.y - 1, 0));
                if (tileToMove.isWater == false)
                {
                    selected.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - 1, 0);
                    playerCon.numOfLandMoves -= 1;
                    playerData = playerCon.GetPlayerData();
                    Manager.GetComponent<UiManager>().DisplayPlayerData(playerData);
                }
                else
                {
                    print("Cannot move there!");
                }
            }
        }
    }
    void Update()
    {
        //check if selected has a gameObject
        if (selected != null && selected.GetComponent<player>() != null)
        {
            MovePlayer();

        }
        

        if (Input.GetMouseButtonDown(0))
        {
            
            Ray point = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(point);
            
            if (hit.collider != null)
            {
               

                if (hit.collider.gameObject.CompareTag("character") == true)
                {
                    
                    var hitChar = hit.collider.gameObject;
                    selected = hitChar;
                    var hitCharRend = hitChar.GetComponent<SpriteRenderer>();
                    hitCharRend.color = UnityEngine.Color.yellow;
                    playerData = hitChar.GetComponent<player>().GetPlayerData();
                    Manager.GetComponent<UiManager>().DisplayPlayerData(playerData);
                    Debug.Log("char hit");
                    

                }
  
            }

            
            else if (hit.collider == null)
            {
                
                Manager.GetComponent<UiManager>().DisablePlayerData();
                
                if(selected != null)
                {
                    
                    selected.GetComponent<player>().UnSelect();
                    selected = null;
                }
                
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickedPos = map.WorldToCell(mousePos);
                Vector3Int tilePos = new Vector3Int(clickedPos.x, clickedPos.y, 0);
                //Debug.Log(tilePos);
                TileData SelectedTile = GetTileData(tilePos);
                if (SelectedTile != null)
                {
                    double chance = SelectedTile.weight * 100;
                    string world_data = ("Coords: " + SelectedTile.Position + "\n" + SelectedTile.tileName.ToUpper() + "\n" +
                         chance.ToString("<color=red>0.00</color>") + "<color=red>%</color>" + 
                        " chance for an encounter");
                    Manager.GetComponent<UiManager>().DisplayWorldData(world_data);
                }
                else
                {
                    Debug.Log("tile is null");
                }

            }

            else
            {
                return;
            }

        }
        if (Input.GetMouseButton(2))
           {
            Diference = (cam.ScreenToWorldPoint(Input.mousePosition)) - cam.transform.position;
            if (Drag == false)
            {
                Drag = true;
                Origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
           }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            cam.transform.position = Origin - Diference;
        }
        //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
        if (Input.GetMouseButton(1))
        {
            cam.transform.position = ResetCamera;
            cam.orthographicSize = 6;
        }
       

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize -= 5;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize += 5;
        }
    }
    private bool IsmouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

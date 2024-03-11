using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.Image;

public class controller : MonoBehaviour
{
    public float speed = 0.1f;
    private TileData myTile;
    [SerializeField]
    private GameObject Manager;
    public Tilemap map;
    [SerializeField]
    private Camera cam;
    public TileBase HighlightTile;
    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    public Tilemap highlightMap;
    private Vector3 playerData;
    [SerializeField] private Vector3Int[] neighbors;
    public Vector3Int tilePos;
    private TileData currentTile = null;

    public GameObject selected = null;
    public Stack<TileData> path;
    private void Awake()
    {
        ResetCamera = new Vector3(this.transform.position.x, this.transform.position.y, -10);
        Manager = GameObject.Find("GameManager");
        map = Manager.GetComponent<HexMapGen>().worldMap;
        highlightMap = Manager.GetComponent<HexMapGen>().overlay;

    }


    public void GetNeighbors(Vector3Int hexPos)
    {
        //convert to oddr offset. 
        neighbors = new Vector3Int[6];
        Vector3Int[] oddrDirectionsEvenRow = {
        new Vector3Int(+1, 0, 0), new Vector3Int(0, -1, 0), new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 0, 0), new Vector3Int(-1, +1, 0), new Vector3Int(0, +1, 0)
        };

        Vector3Int[] oddrDirectionsOddRow = {
        new Vector3Int(+1, 0, 0), new Vector3Int(+1, -1, 0), new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0), new Vector3Int(0, +1, 0), new Vector3Int(+1, +1, 0)
        };

        //handles if even row/if odd row
        Vector3Int[] directions = hexPos.y % 2 == 0 ? oddrDirectionsEvenRow : oddrDirectionsOddRow;

       
        for (int i = 0; i < 6; i++)
        {
            TileData checkTile = GetTileData(hexPos + directions[i]);
            if(checkTile.isWater == true)
            {
                continue;
            }
            else
            {
                neighbors[i] = hexPos + directions[i];
                highlightMap.SetTile(neighbors[i], HighlightTile);
            }
        }
    }
    private TileData GetTileData(Vector3Int pos)
    {
        

        //pulls the tile at position argument. Could be used to check if tile is water or not as well
        foreach (TileData tile in Manager.GetComponent<HexMapGen>().tileDatas)
        {
            
            if(tile.Position == pos)
            {
              
               myTile = tile;
               return myTile;
            }
           
               

        }
        return null;

    }

   
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
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
                    //GetNeighbors(playerHexPos);
                }
  
            }
            
            
            else if (hit.collider == null)
            {
                
                Manager.GetComponent<UiManager>().DisablePlayerData();
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3Int clickedPos = map.WorldToCell(mousePos);
                tilePos = new Vector3Int(clickedPos.x, clickedPos.y, 0);
                Debug.Log(tilePos);
                TileData SelectedTile = GetTileData(tilePos);
                if(SelectedTile == null)
                {
                    selected.GetComponent<player>().UnSelect();
                    selected = null;
                    Debug.Log("tile is null");
                    return;
                }
                if (selected != null)
                {
                    Vector3 playerPos = selected.transform.position;
                    Vector3Int playerHexPos = map.WorldToCell(playerPos + map.tileAnchor);
                    TileData playerTile = GetTileData(playerHexPos);
                    path = new Stack<TileData>(Manager.GetComponent<HexMapGen>().GetPath(playerTile, SelectedTile));
                    if (path == null)
                    {
                        return;
                    }
                    Debug.Log(path.Count + " tiles in path");
                    GetNextTile();
                    selected.GetComponent<player>().UnSelect();
                }

                else if (SelectedTile.isCity == false)
                {
                        double chance = SelectedTile.weight * 100;
                        string world_data = ("Coords: " + SelectedTile.Position + "\n" + SelectedTile.tileName.ToUpper() + "\n" +
                         chance.ToString("<color=red>0.00</color>") + "<color=red>%</color>" + 
                        " chance for an encounter");
                        Manager.GetComponent<UiManager>().DisplayWorldData(world_data);
                }
                else if(SelectedTile.isCity == true)
                {
                    string world_data = ("Coords: " + " X " + SelectedTile.Position.x + " Y " + SelectedTile.Position.y + "\n" + SelectedTile.tileName.ToUpper());
                    Manager.GetComponent<UiManager>().DisplayWorldData(world_data);
                    Debug.Log("this is a city");
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
        void MoveOnPath()
        {
            if (currentTile == null) return;
            selected.transform.position = map.CellToWorld(currentTile.Position) - map.tileAnchor;
            ResetCamera = new Vector3(selected.transform.position.x, selected.transform.position.y, -10);
            if (selected.transform.position == map.CellToWorld(currentTile.Position) - map.tileAnchor)
            {
                StartCoroutine(DoDelay());
                
            }
            
        }
        IEnumerator DoDelay()
        {
            yield return new WaitForSeconds(speed);
            GetNextTile();
        }

        void GetNextTile()
        {
            if (path.Count > 0)
            {
                foreach(TileData tile in path)
                {
                    highlightMap.SetTile(tile.Position, HighlightTile);
                }
                currentTile = path.Pop();
                MoveOnPath();
            }
            else 
            { 
                foreach (TileData tile in path)
                {
                    tile.parent = null;
                }
                //maybe roads and rivers?
                highlightMap.ClearAllTiles();
                currentTile = null;
                path.Clear();
                Manager.GetComponent<HexMapGen>().pathfinding.openList.Clear();
                Manager.GetComponent<HexMapGen>().pathfinding.closedList.Clear();
                selected = null;
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
            cam.orthographicSize = 5;
        }
       

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(cam.orthographicSize <= 1)
            {
                cam.orthographicSize = 1;
            }
            else cam.orthographicSize -= 1;

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

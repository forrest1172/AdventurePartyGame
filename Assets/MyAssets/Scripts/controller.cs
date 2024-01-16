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

    public GameObject selected = null;
    private void Start()
    {
        Manager = GameObject.Find("GameManager");
        map = Manager.GetComponentInChildren<Tilemap>();
    }
    private TileData GetTileData(Vector3Int pos)
    {
        //TO DO
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
    void Update()
    {

        
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
                    Vector3 playerData = hitChar.GetComponent<player>().GetPlayerData();
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
        ResetCamera = new Vector3(this.transform.position.x, this.transform.position.y, -10);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            cam.orthographicSize--;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            cam.orthographicSize++;
        }
    }
    private bool IsmouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}

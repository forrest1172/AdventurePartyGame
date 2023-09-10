using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;
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
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedPos = map.WorldToCell(mousePos);
            Vector3Int tilePos = new Vector3Int(clickedPos.x, clickedPos.y,0);
            //Debug.Log(tilePos);
            TileData SelectedTile = GetTileData(tilePos);
            if (SelectedTile != null)
            {
                Debug.Log(SelectedTile.Position + " " + "Tile ID: " + SelectedTile.tileId + " " + "Tile weight: " + SelectedTile.weight.ToString("0.00"));
            }
            else Debug.Log("tile is null");
            
            
            

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
}

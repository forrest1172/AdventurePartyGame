using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AgentMovement : MonoBehaviour
{
    public HexMapGen tilemap; 
    public float moveSpeed = 0.2f;
    private Stack<Vector3Int> path;

    private void Awake()
    {
        GameObject manager = GameObject.Find("GameManager");
        tilemap = manager.GetComponent<HexMapGen>();
        
    }

    public void GetOrders()
    {
        
    }
}

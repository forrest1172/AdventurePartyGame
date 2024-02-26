using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
    [SerializeField] private int wealth = 0;
    public bool isSelected = false;
    public int numOfLandMoves = 7;
    public int numOfWaterMoves = 1;
    


    private void Start()
    {
        
        numOfLandMoves -= wealth;
        numOfWaterMoves += wealth;
    }

    public void UnSelect()
    {
        //remove this for move control
        numOfLandMoves += 7;
        //
        isSelected = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public Vector3 GetPlayerData()
    {
        
        isSelected = true;
        
        return new Vector3(speed, currentHealth, numOfLandMoves);
    }

  
}

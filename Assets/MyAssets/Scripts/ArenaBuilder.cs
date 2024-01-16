using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] grassTile;
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 20;
    [SerializeField] private Transform mapParent;
    [SerializeField] private GameObject[] characters;
    public void Awake()
    {

        Gen();
    }
    public void Gen()
    {
        for (int x = 0;  x < width; x++)
        {
            for(int y = 0; y < height; y++) 
            {
                Instantiate(grassTile[0], new Vector3(x, 0, y), Quaternion.identity, mapParent);
                if(x == width / 2 && y == height / 2)
                {
                    Instantiate(characters[0], new Vector3(x, 0.5f, y), Quaternion.identity);

                }
            }
        }
    }
}

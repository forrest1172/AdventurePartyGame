using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileLogic : MonoBehaviour
{
   public Color originColor;

    private void Awake()
    {
        originColor = gameObject.GetComponent<Renderer>().material.color;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = originColor;
    }

    public void clearMaterial()
    {
        gameObject.GetComponent<Renderer>().material.color = originColor;
    }
        
}

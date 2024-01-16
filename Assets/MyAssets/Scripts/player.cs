using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Rendering;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;


   
    public void UnSelect()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public Vector3 GetPlayerData()
    {
        return new Vector3(speed, currentHealth, damage);
    }
}

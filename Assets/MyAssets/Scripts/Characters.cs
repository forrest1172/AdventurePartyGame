using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Characters : ScriptableObject
{
    [SerializeField] private GameObject model;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float speed;
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float damage;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship.Enum;
using System;
using UnityEngine.UI;

public enum TargetType
{
    Enemy,
    Self
}

[Serializable]
public class Card 
{
    public Texture img;
    public string name;
    [TextArea(15, 20)] public string description;
    public Color color = Color.white;
    public AudioClip audioClip;
    [Range(0.0f, 1.0f)] public float volume;
    public CardType type;
    public int power;   //Damage, heal
    public TargetType target;
    public bool leaveObstacle;
    public bool reveal;
    public int energyCost = 0;
    public int additionalHits = 0;


    public List<Vector2Int> aoe = new List<Vector2Int>() { Vector2Int.zero };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Ship
{
    public string shipName;
    public int shipModel;
    public ShipClass shipClass;
    public List<Vector2Int> size = new List<Vector2Int>() { Vector2Int.zero };
    [TextArea(15, 20)] public string description;
    public int health = 1;
    public int speed = 1;
    public int startFuel = 5;

    //USED FOR BATTLE
    private int fuel;
    public List<Vector2Int> hitboxLocations = new List<Vector2Int>();
    [HideInInspector] public Vector2Int originPoint;
    [HideInInspector] public int rotateMode;

    //public Ship(string name, int model, ShipClass)

    public enum ShipClass
    {
        Submarine,
        Cruiser,
        Battleship,
        Destroyer,
        Carrier
    }


}

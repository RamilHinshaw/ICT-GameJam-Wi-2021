using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Ship
{
    public string shipName;
    public ShipClass shipClass;
    public int health = 1;
    public int speed = 1;
    public int startFuel = 5;
    private int fuel;

    public enum ShipClass
    {
        Submarine,
        Cruiser,
        Battleship,
        Destroyer,
        Carrier
    }


}

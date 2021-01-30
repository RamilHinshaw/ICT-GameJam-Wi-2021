using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShipManager", order = 1)]
public class ShipDatabase : ScriptableObject
{
    public List<Ship> ships;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public int commanderID;
    public string playerName;

    public bool premadeDeck = true;
    public int deckID = 0;
    
    public List<int> ships;

}

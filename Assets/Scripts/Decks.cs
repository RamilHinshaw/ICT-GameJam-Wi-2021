using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DeckManager", order = 1)]
public class Decks : ScriptableObject
{
    public List<Deck> decks = new List<Deck>();
}

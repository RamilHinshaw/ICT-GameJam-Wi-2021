using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Deck", order = 1)]
public class Deck : ScriptableObject
{
    public string deckName;
    [SerializeField] public List<int> cards = new List<int>();
}

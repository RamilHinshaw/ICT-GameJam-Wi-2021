using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardManager", order = 1)]
public class CardDatabase : ScriptableObject
{
    [SerializeField] public List<Card> cards = new List<Card>();
}

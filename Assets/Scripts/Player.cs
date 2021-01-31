﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship;

[System.Serializable]
public class Player
{
    public string playerName;
    public Commander commander;
    //public int deckID;
    public List<int> cardsInDeck = new List<int>();

    public List<Ship> ships = new List<Ship>();

    public int selectedShip;
    public int deckCounter;

    public Battleship.Grid playerViewGrid;
    public Battleship.Grid enemyViewGrid;

    public List<int> cardsInHand = new List<int>();

    public Player(PlayerData data)
    {
        playerName = data.playerName;

        Deck referencedDeck = GameManager.Instance.deckDatabase.decks[data.deckID];

        //Add each card into the players deck
        for (int i = 0; i < referencedDeck.cards.Count; i++)
        {
            cardsInDeck.Add(referencedDeck.cards[i]);
        }

        //Shuffle Deck
        System.Random rng = new System.Random();
        int n = cardsInDeck.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = cardsInDeck[k];
            cardsInDeck[k] = cardsInDeck[n];
            cardsInDeck[n] = value;
        }      

        //Add each ship based on its id
        for (int i = 0; i < data.ships.Count; i++)
        {
            //Add ship info to this - DUMB ISSUE HERE :(

            Ship referencedShip = GameManager.Instance.shipDatabase.ships[data.ships[i]];
            Ship newShip = new Ship();

            //Carry Properties over :(
            newShip.shipName = referencedShip.shipName;
            newShip.shipModel = referencedShip.shipModel;
            newShip.shipClass = referencedShip.shipClass;
            newShip.size = referencedShip.size;
            newShip.description = referencedShip.description;
            newShip.health = referencedShip.health;
            newShip.speed = referencedShip.speed;
            newShip.startFuel = referencedShip.startFuel;
                       


            ships.Add(newShip);
        }
    }

    public bool PlaceShip(int shipIndex, List<Vector2Int> hitboxes)
    {
        //IF all ships placed then tell gameManager player is ready
        if (selectedShip > ships.Count)
            return true;

        ships[shipIndex].hitboxLocations = hitboxes;
        selectedShip++;


        return false;
    }

    public void DrawCard(int num = 1)
    {
        for (int i = 0; i < num; i++)
        {
            if (cardsInHand.Count >= 7)
            {
                Debug.Log("MAX CARDS REACHED!");
                return;
            }

            cardsInHand.Add(cardsInDeck[deckCounter + i]);                
        }

        deckCounter += num;
    }


}

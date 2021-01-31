using System.Collections;
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

    //public Battleship.Grid playerViewGrid = new Battleship.Grid(20,20); //Used for obstacles that enemy placed on you
    //public Battleship.Grid enemyViewGrid = new Battleship.Grid(20, 20); //Used to see where you shot last time and obstacles you placed

    public List<Vector2Int> attackGrid = new List<Vector2Int>();
    public List<Vector2Int> defenseGrid = new List<Vector2Int>();

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

    public bool PlaceShip(int shipIndex, List<Vector2Int> hitboxes, int increment = 1)
    {
        ships[shipIndex].hitboxLocations = new List<Vector2Int>();

        //Define this ships hitbox!
        for (int i = 0; i < hitboxes.Count; i++)
            ships[shipIndex].hitboxLocations.Add(hitboxes[i]);

        selectedShip+= increment;

        //IF all ships placed then tell gameManager player is ready
        if (selectedShip >= ships.Count)
            return true;


        return false;
    }

    public void AddPlayerAttackMarker(List<Vector2Int> hitboxes)
    {
        //enemyViewGrid.AddGrid(hitboxes);

        for (int i = 0; i < hitboxes.Count; i++)
        {
            attackGrid.Add(hitboxes[i]);
        }
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

            if (cardsInDeck[deckCounter + i] >= cardsInDeck.Count)
                deckCounter = 0;

                cardsInHand.Add(cardsInDeck[deckCounter + i]);                
        }

        deckCounter += num;
    }

    public List<int> AttackArea(List<Vector2Int> aoe)
    {
        List<int> shipsThatGotHit = new List<int>();

        for (int i = 0; i < ships.Count; i++)
        {
            for (int j = 0; j < ships[i].hitboxLocations.Count; j++)
            {
                for (int k = 0; k < aoe.Count; k++)
                {
                    if (aoe[k] == ships[i].hitboxLocations[j])
                    {
                        shipsThatGotHit.Add(i);
                        j = ships[i].hitboxLocations.Count; //To navigate to next ship
                        break;
                    }
                }
            }
        }

        return shipsThatGotHit;
    }

    public void NextShip()
    {
        selectedShip++;

        if (selectedShip >= ships.Count)
            selectedShip = 0;
    }

    public void DamageShip(int shipIndex, int dmg)
    {
        ships[shipIndex].health -= dmg;
    }


}

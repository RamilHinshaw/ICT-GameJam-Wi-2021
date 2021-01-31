using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//GameManger is a singleton Monobehavior
//This class will become big :)
public class GameManager : MonoBehaviour
{
    #region Instance

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                print("Instance of GameObject does not exist!");

            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    #endregion

    //Managers
    [Header("Managers")]
    public GuiManager guiManager;

    [Header("Database")]
    public CardDatabase cardDatabase;
    public ShipDatabase shipDatabase;
    public Decks deckDatabase;
    //public ShipDatabase ship;


    [Header("General")]
    //[HideInInspector] public Arena arena;
    public List<Player> players = new List<Player>();

    [Header("Data")]
    public PlayerData Player1;
    public bool npcPlayer2 = true;
    public PlayerData Player2;


    public int selectedCard;
    public UICard UIcard;

    //public enum GameMode
    //{
    //    PlaceUnits, //Everyone Places their units and Okays it
    //    Game,   //Normal Gameplay Loop
    //    Finished //Game has ended logic
    //}
    public enum Phases
    {
        PlaceShips,
        TurnStart, //Switch to next ship, draw card
        Action, //Move & Attack
        TurnEnd, //Declaration that turn ended (Might be good for passives)        
    }

    //public GameMode mode;
    public Phases phase = Phases.Action;

    private void Start()
    {
        //Translate PlayerData into Player List
        players.Add(new Player(Player1));
        players.Add(new Player(Player2));

        //Player Give cards to player
        players[0].DrawCard(7);
        guiManager.UpdateCardsInHand(players[0]);
    }

    private void Update()
    {
        switch (phase)
        {
            case Phases.PlaceShips:
                break;
            case Phases.TurnStart:                
                break;
            case Phases.Action:
                break;
            case Phases.TurnEnd:
                break;
            default:
                break;
        }
    }

    //Attack, move, etc from tile click
    public void PerformGridAction() //ON CLICK TILE!
    {
        //Perform action after selecting grid
        //Use card effect here!

        switch (phase)
        {
            case Phases.PlaceShips:
                //Get next ship to place from player
                //If all ships are placed then move to next phase

                var lastCoordinate = guiManager.lastCoordinate;

                if (guiManager.legal) //If allowed to place ship there!
                {
                    if (players[0].PlaceShip(players[0].selectedShip, guiManager.lastColoredTiles) == true)
                    {
                        //IF True than all ships have been placed! SWITCH PHASE
                        guiManager.ShowCards();
                        phase = Phases.Action;
                    }

                    //Give appropiate hitbox of the ship based on the UI selection
                    //players[0].ships[players[0].selectedShip].hitboxLocations = guiManager.lastColoredTiles;
                    //players[0].ships[players[0].selectedShip].hitboxLocations = new List<Vector2Int>(guiManager.lastColoredTiles);
                    //players[0].ships[players[0].selectedShip].hitboxLocations = guiManager.GetLastCoordinate();
                    //guiManager.lastColoredTiles.CopyTo(players[0].ships[players[0].selectedShip].hitboxLocations);

                    guiManager.ClearTiles();

                    guiManager.lastPlayer = players[0];
                    guiManager.ShowPlayerShips(players[0], Color.green);
                    
                }


                //HARD CODE AI HERE TO PLACE THEIR SHIPS...

                break;
            case Phases.TurnStart:
                break;
            case Phases.Action:
                //UIcard.gameObject.SetActive(false);
                RemoveCardFromHand(UIcard);
                players[0].DrawCard();

                guiManager.UpdateCardsInHand(players[0]);

                guiManager.ClearTiles();
                guiManager.ShowCards();
                break;
            case Phases.TurnEnd:
                break;
            default:
                break;
        }


    }

    public void RemoveCardFromHand(UICard card)
    {
        //guiManager.cardsInHand.Remove(card);

        //Find matching cardID in player hand and remove it
        //Update all Cards in hand again

        for (int i = 0; i < players[0].cardsInHand.Count; i++)
        {
            if (players[0].cardsInHand[i] == card.cardID)
            {
                players[0].cardsInHand.RemoveAt(i);
                break;
            }            
        }        
    }

    public void SelectCard(int cardID)
    {
        selectedCard = cardID;
        guiManager.ShowGrid();
    }

    public void Test()
    {
        print("TEST!");
    }



}




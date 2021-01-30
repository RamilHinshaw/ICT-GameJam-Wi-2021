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
    //public ShipDatabase ship;


    [Header("General")]
    [HideInInspector] public Arena arena;
    public List<Player> players;

    public int selectedCard;

    public enum GameMode
    {
        PlaceUnits, //Everyone Places their units and Okays it
        Game,   //Normal Gameplay Loop
        Finished //Game has ended logic
    }
    public enum Phases
    {
        Start, //Switch to next ship, draw card
        Combat, //Move & Attack
        CardEffect,
        TurnEnd, //Declaration that turn ended (Might be good for passives)        
    }

    public GameMode mode;
    public Phases phase;

    private void Start()
    {
        arena.Init();
    }

    private void Update()
    {
        switch (mode)
        {
            case GameMode.PlaceUnits:
                //Place each unit, when all is placed give okay

                //If player 1 & player 2 placed all units then move to game mode
                break;
            case GameMode.Game:

                switch (phase)
                {
                    case Phases.Start:
                        //Start the selected players turn based on ship speed
                        break;
                    case Phases.Combat:

                        break;
                    case Phases.CardEffect:

                        break;
                    case Phases.TurnEnd:

                        break;
                    default:
                        break;
                }


                break;

            case GameMode.Finished:
                //Gameplay has finished!
                break;
            default:
                break;
        }
    }

    //Attack, move, etc from tile click
    public void PerformGridAction()
    {
        //Perform action after selecting grid
        //Use card effect here!

        guiManager.ClearTiles();
        guiManager.ShowCards();
    }

    public void SelectCard(int cardID)
    {
        selectedCard = cardID;
        guiManager.ShowGrid();
    }

}




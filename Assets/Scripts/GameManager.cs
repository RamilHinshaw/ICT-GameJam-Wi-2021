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


    [Header("General")]
    //Selected Card
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

    private void Start()
    {

    }

    private void Update()
    {

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




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    const int GRID_SIZE_X = 20;
    const int GRID_SIZE_Y = 20;

    public int rotateMode = 0;

    public Tile[,] grid = new Tile[GRID_SIZE_X, GRID_SIZE_Y];

    //Settings of the grid
    public bool legal = true;
    public Vector2Int lastCoordinate;
    public List<Vector2Int> lastColoredTiles = new List<Vector2Int>();
    public Player lastPlayer;


    //Keep track of cells
    public GameObject ui_Grid;
    public GameObject ui_Cards;
    //public GameObject ui_Cards;

    public List<UICard> cardsInHand = new List<UICard>();

    public void Update()
    {
        if (ui_Grid.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                rotateMode++;

                if (rotateMode >= 4)
                    rotateMode = 0;

                ClearTiles();
                ColorTiles(lastCoordinate);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (lastPlayer != null)
                    ShowPlayerShips(lastPlayer, Color.green);

                print("PRESSED!");
            }
        }
    }

    public void OnHoverCell(int x, int y)
    {
        //Updates the red marks based on the cells
        //Event call on hover on the cells
    }

    public void ShowGrid()
    {
        ui_Grid.SetActive(true);
        ui_Cards.SetActive(false);
    }

    public void ShowCards()
    {
        ui_Grid.SetActive(false);
        ui_Cards.SetActive(true);
    }

    public void ColorTiles(Vector2Int coordinate)
    {
        //Colors tiles based on the phase!
        lastCoordinate = coordinate;
        lastColoredTiles.Clear();
        ClearTiles();

        switch (GameManager.Instance.phase)
        {
            case GameManager.Phases.PlaceShips:
                if (lastPlayer != null)
                    ShowPlayerShips(lastPlayer, Color.green);
                
                PlaceShipColors(coordinate);
                break;

            case GameManager.Phases.Action:
                legal = true;
   
                ActionColors(coordinate);
                break;

            default:
                break;
        }        
    }

    public void PlaceShipColors(Vector2Int coordinate)
    {
        //BASED ON SHIP ID INSTEAD!
        int shipID = 0;//Get from current Player to place ships
        Ship ship = GameManager.Instance.shipDatabase.ships[shipID];

        //For each tile!
        for (int i = 0; i < ship.size.Count; i++)
        {
            Vector2Int takenSpace = ship.size[i];

            //Rotations
            var tempArea = takenSpace;
            if (rotateMode == 1)
            {
                takenSpace.x = tempArea.y;
                takenSpace.y = -tempArea.x;
            }
            else if (rotateMode == 2)
            {
                takenSpace.x = -tempArea.x;
                takenSpace.y = -tempArea.y;
            }
            else if (rotateMode == 3)
            {
                takenSpace.x = -tempArea.y;
                takenSpace.y = tempArea.x;
            }
            //-----------------------------

            //When considering the coordinate along with the aoe
            Vector2Int takenSpaceOffset = coordinate + takenSpace;

            //If out of range skip!
            if (takenSpaceOffset.y >= GRID_SIZE_Y ||
                takenSpaceOffset.x >= GRID_SIZE_X ||
                takenSpaceOffset.y < 0 || takenSpaceOffset.x < 0)
                continue;

            grid[takenSpaceOffset.x, takenSpaceOffset.y].img.color = Color.green;

            lastColoredTiles.Add( new Vector2Int(takenSpaceOffset.x, takenSpaceOffset.y));
        }

        //CHANGE!
        legal = true;
    }

    public void ActionColors(Vector2Int coordinate)
    {
        //Colors based on selected card
        int cardID = GameManager.Instance.selectedCard;
        Card card = GameManager.Instance.cardDatabase.cards[cardID];

        for (int i = 0; i < card.aoe.Count; i++)
        {
            Vector2Int damagedArea = card.aoe[i];

            //Rotations
            var tempArea = damagedArea;
            if (rotateMode == 1)
            {
                damagedArea.x = tempArea.y;
                damagedArea.y = -tempArea.x;
            }
            else if (rotateMode == 2)
            {
                damagedArea.x = -tempArea.x;
                damagedArea.y = -tempArea.y;
            }
            else if (rotateMode == 3)
            {
                damagedArea.x = -tempArea.y;
                damagedArea.y = tempArea.x;
            }

            //When considering the coordinate along with the aoe
            Vector2Int additiveDamagedArea = coordinate + damagedArea;

            //If out of range skip!
            if (additiveDamagedArea.y >= GRID_SIZE_Y ||
                additiveDamagedArea.x >= GRID_SIZE_X ||
                additiveDamagedArea.y < 0 || additiveDamagedArea.x < 0)
                continue;

            grid[additiveDamagedArea.x, additiveDamagedArea.y].img.color = Color.red;
        }
    }

    public void ClearTiles()
    {
        for (int i = 0; i < GRID_SIZE_X; i++)
            for (int j = 0; j < GRID_SIZE_Y; j++)
                grid[i, j].img.color = Color.white;
    }

    public void UpdateTiles(Battleship.Grid playerGrid, Color color)
    {
        for (int i = 0; i < GRID_SIZE_X; i++)
            for (int j = 0; j < GRID_SIZE_Y; j++)
                if (playerGrid.grid[i,j] == true)
                {
                    grid[i, j].img.color = color;
                }                
    }

    public void ShowPlayerShips(Player player, Color color)
    {
        var ships = player.ships;

        for (int i = 0; i < ships.Count; i++)
        {
            var hitboxLocations = ships[i].hitboxLocations;
            for (int j = 0; j < hitboxLocations.Count; j++)
            {
                Vector2Int tile = hitboxLocations[j];
                grid[tile.x, tile.y].img.color = color;
            }            
        }
    }

    public List<Vector2Int> GetLastCoordinate()
    {
        List<Vector2Int> hitboxes = new List<Vector2Int>();

        for (int i = 0; i < lastColoredTiles.Count; i++)
        {
            hitboxes.Add(lastColoredTiles[i]);
        }

        return hitboxes;
    }
    public void UpdateCardsInHand(Player player)
    {
        //Clear all cards in hand then refresh
        for (int i = 0; i < cardsInHand.Count; i++)
            cardsInHand[i].gameObject.SetActive(false);

        //Update all cards in hand
        for (int i = 0; i < player.cardsInHand.Count; i++)
        {
            cardsInHand[i].UpdateCard(player.cardsInHand[i]);
            cardsInHand[i].gameObject.SetActive(true);
        }
    }

}

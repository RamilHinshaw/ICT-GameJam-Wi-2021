﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour
{
    const int GRID_SIZE_X = 20;
    const int GRID_SIZE_Y = 20;

    public Tile[,] grid = new Tile[GRID_SIZE_X, GRID_SIZE_Y];


    //Keep track of cells
    public GameObject ui_Grid;
    public GameObject ui_Cards;
    //public GameObject ui_Cards;


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
        //Colors based on selected card
        int cardID = GameManager.Instance.selectedCard;
        Card card = GameManager.Instance.cardDatabase.cards[cardID];

        for (int i = 0; i < card.aoe.Count; i++)
        {
            Vector2Int damagedArea = card.aoe[i];

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

}
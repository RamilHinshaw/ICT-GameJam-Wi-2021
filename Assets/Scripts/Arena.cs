﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battleship;

public class Arena : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(20,20);
    //public List<Battleship.Grid> grids = new List<Battleship.Grid>(); //Player 1 & 2 grid
    
    public void Init()
    {
        //Setup grid based on this arena for both players


        ////PLAYER 1 GRIDS
        //grids.Add(new Battleship.Grid(gridSize.x, gridSize.y)); //THEIR VIEW OF THEIR OWN GRID
        //grids.Add(new Battleship.Grid(gridSize.x, gridSize.y)); //THEIR VIEW OF THEIR OWN GRID

        //grids.Add(new Battleship.Grid(gridSize.x, gridSize.y)); //Player 2 Grid
    }

    public void PlaceShip()
    {

    }


}

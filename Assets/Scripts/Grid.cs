using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Battleship
{    public class Grid
    {
        public bool[,] grid;

        public Grid(int x, int y)
        {
            grid = new bool[x, y];
        }

        public void AddGrid(Grid valGrid)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (valGrid.grid[i, j] == true)
                        grid[i, j] = true;
                }
        }

        public void AddGrid(List<Vector2Int> hitboxes)
        {
            for (int i = 0; i < hitboxes.Count; i++)          
                grid[hitboxes[i].x, hitboxes[i].y] = true;
                
        }
    }
}


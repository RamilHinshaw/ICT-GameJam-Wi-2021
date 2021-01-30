using System.Collections;
using System.Collections.Generic;


namespace Battleship
{    public class Grid
    {
        private bool[,] grid;

        public Grid(int x, int y)
        {
            grid = new bool[x, y];
        }
    }
}


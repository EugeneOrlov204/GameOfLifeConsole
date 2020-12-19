using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameEngine
    {
        private bool[,] field;
        private readonly int rows;
        private readonly int cols;
        public int AmountOfLivingCells { get; private set; }
        public uint CurrentGenaration { get; private set; }
        private Random random = new Random();

        public GameEngine(int rows, int cols, int density)
        {

            this.rows = rows;
            this.cols = cols;
            field = new bool[cols, rows];

            Random random = new Random();
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    field[x, y] = random.Next(density) == 0;

                    var hasLife = field[x, y];

                    if (hasLife)
                        AmountOfLivingCells++;
                }
            }
        }

        private int CountNeighbours(int x, int y)
        {
            int count = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + j + rows) % rows;

                    bool isSelfChecking = col == x && row == y;
                    var hasLife = field[col, row];

                    if (hasLife && !isSelfChecking)
                        count++;

                }
            }

            return count;
        }

        public void NextGeneration()
        {
            var newField = new Boolean[cols, rows];

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var neighboursCount = CountNeighbours(x, y);


                    var hasLife = field[x, y];


                    if (!hasLife && neighboursCount == 3)
                    {
                        newField[x, y] = true;
                        AmountOfLivingCells++;
                    }

                    else if (hasLife && (neighboursCount < 2 || neighboursCount > 3))
                    {
                        newField[x, y] = false;
                        AmountOfLivingCells--;
                    }
                    else
                    {
                        newField[x, y] = field[x, y];
                    }

                }
            }
            field = newField;
            CurrentGenaration++;
        }


        public bool[,] GetCurrentGeneration()
        {
            var result = new bool[cols, rows];
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    result[x, y] = field[x, y];
                }
            }
            return field;
        }

        private bool ValidateCellPosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }


        public void AddCell(int x, int y)
        {
            if (ValidateCellPosition(x, y))
            {
                if (!field[x, y])
                    AmountOfLivingCells++;
                field[x, y] = true;
            }

        }

        public void RemoveCell(int x, int y)
        {
            if (ValidateCellPosition(x, y))
            {
                if (field[x, y])
                    AmountOfLivingCells--;
                field[x, y] = false;
            }

        }
    }
}

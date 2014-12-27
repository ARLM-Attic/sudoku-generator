using System.Collections.Generic;
using System.Linq;

namespace TrueMagic.SudokuGenerator
{
    public class Sudoku
    {
        public int BlockSize { get; private set; }
        public int BoardSize { get; private set; }
        private readonly byte[] possibleValues;
        private readonly IDictionary<int, int> blockIndex = new Dictionary<int, int>();
        private readonly IDictionary<int, int> inBlockIndex = new Dictionary<int, int>();
        private byte[][] rows;
        private byte[][] columns;
        private byte[][] blocks;

        public Sudoku(int blockSize)
        {
            this.BlockSize = blockSize;
            this.BoardSize = blockSize * blockSize;
            this.possibleValues = Enumerable.Range(1, this.BoardSize).Select(value => (byte)value).ToArray();

            this.rows = new byte[this.BoardSize][];
            this.columns = new byte[this.BoardSize][];
            this.blocks = new byte[this.BoardSize][];
            for (var x = 0; x < this.BoardSize; x++)
            {
                this.rows[x] = new byte[this.BoardSize];
                this.columns[x] = new byte[this.BoardSize];
                this.blocks[x] = new byte[this.BoardSize];
            }

            for (var blockX = 0; blockX < this.BlockSize; blockX++)
            {
                for (var blockY = 0; blockY < this.BlockSize; blockY++)
                {
                    for (var x = 0; x < this.BlockSize; x++)
                    {
                        for (var y = 0; y < this.BlockSize; y++)
                        {
                            var itemX = blockX * this.BlockSize + x;
                            var itemY = blockY * this.BlockSize + y;
                            this.blockIndex[itemY * this.BoardSize + itemX] = blockY * this.BlockSize + blockX;
                            this.inBlockIndex[itemY * this.BoardSize + itemX] = y * this.BlockSize + x;
                        }
                    }
                }
            }
        }

        private Sudoku(Sudoku sudoku)
        {
            this.BlockSize = sudoku.BlockSize;
            this.BoardSize = sudoku.BoardSize;
            this.possibleValues = sudoku.possibleValues;
            this.blockIndex = sudoku.blockIndex;
            this.inBlockIndex = sudoku.inBlockIndex;
            this.rows = sudoku.rows.Select(row => row.ToArray()).ToArray();
            this.columns = sudoku.columns.Select(column => column.ToArray()).ToArray();
            this.blocks = sudoku.blocks.Select(block => block.ToArray()).ToArray();
        }

        public byte GetValue(int x, int y)
        {
            return this.rows[x][y];
        }

        public void SetValue(int x, int y, byte value)
        {
            this.rows[x][y] = value;
            this.columns[y][x] = value;
            this.blocks[this.blockIndex[y * this.BoardSize + x]][this.inBlockIndex[y * this.BoardSize + x]] = value;
        }

        public bool CanSetValue(int x, int y, byte value)
        {
            return !this.rows[x].Contains(value) && !this.columns[y].Contains(value) && !this.blocks[this.blockIndex[y * this.BoardSize + x]].Contains(value);
        }

        public byte[] GetPossibleValues(int x, int y)
        {
            return this.possibleValues.Where(value => CanSetValue(x, y, value)).ToArray();
        }

        public Sudoku Clone()
        {
            return new Sudoku(this);
        }
    }
}

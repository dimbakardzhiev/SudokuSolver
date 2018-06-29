using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class NakedPairStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public NakedPairStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for(int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    EliminateNakedPairFromOthersInRow(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInCol(sudokuBoard, row, col);
                    EliminateNakedPairFromOthersInBlock(sudokuBoard, row, col);

                }
            }

            return sudokuBoard;
        }

        private void EliminateNakedPairFromOthersInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasNakedPairInRow(sudokuBoard, givenRow, givenCol)) return;

            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
               if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] && sudokuBoard[givenRow, col].ToString().Length > 1)
                {
                    EliminateNakedPair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                } 
            }
        }

        private void EliminateNakedPair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
            var valuesToEliminateArray = valuesToEliminate.ToString().ToCharArray();
            foreach (var valueToEliminate in valuesToEliminateArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valueToEliminate.ToString(), string.Empty));
            }
        }

        private bool HasNakedPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol])) return true;
            }

            return false;
        }

        private void EliminateNakedPairFromOthersInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        { 
            throw new NotImplementedException();
        }

        private void EliminateNakedPairFromOthersInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            throw new NotImplementedException();
        }

        private bool IsNakedPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }
    }
}

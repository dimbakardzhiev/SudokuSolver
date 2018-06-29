using SudokuSolver.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Strategies
{
    class SudokuSolverEngine
    {
        private readonly SudokuBoardStateManager _sudokBoardStateManager;
        private readonly SudokuMapper _sudokuMapper;

        public SudokuSolverEngine(SudokuBoardStateManager sudokuBoardStateManager, SudokuMapper sudokuMapper)
        {
            _sudokBoardStateManager = sudokuBoardStateManager;
            _sudokuMapper = sudokuMapper;
        }

        public bool Solve(int[,] sudokuBoard)
        {
            List<ISudokuStrategy> strategies = new List<ISudokuStrategy>()
            {
                new SimpleMarkUpStrategy(_sudokuMapper),
                new NakedPairsStrategy(_sudokuMapper)
            };

            var currentState = _sudokBoardStateManager.GenerateState(sudokuBoard);
            var nextState = _sudokBoardStateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            while (!_sudokBoardStateManager.IsSolved(sudokuBoard) && currentState != nextState)
            {
                currentState = nextState;
                foreach (var strategy in strategies) nextState = _sudokBoardStateManager.GenerateState(strategy.Solve(sudokuBoard));                
            }

            return _sudokBoardStateManager.IsSolved(sudokuBoard);
        }
    }
}

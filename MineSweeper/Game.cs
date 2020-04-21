using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public class Game
    {
        GameBoard _gameBoard;
        public Form1 _gameForm;
        int _wins;
        int _losses;
        bool _running;
        bool _firstClick;

        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            set { _gameBoard = value; }
        }
        public int Wins
        {
            get { return _wins; }
            set { _wins = value; }
        }
        public int Losses
        {
            get { return _losses; }
            set { _losses = value; }
        }
        public bool Running
        {
            get { return _running; }
        }

        public Game(GameBoard GB, Form1 GF)
        {
            _gameBoard = GB;
            
            this.initGame();
            _running = true;
            _firstClick = true;
        }
        public void initGame()
        {
            _wins = 0;
            _losses = 0;
        }
        public void createBoard()
        {
            //Puts bombs in 10% of the squares at random
            Random random = new Random();
            for (int ii = 0; ii < GameBoard.Board.GetLength(0); ii++)
            {
                for (int jj = 0; jj < GameBoard.Board.GetLength(0); jj++)
                {
                    int num = random.Next(GameBoard.Board.GetLength(0));
                    if(num <= GameBoard.Board.GetLength(0) * .10)
                    {
                        GameBoard.Board[jj, ii].Value = -1;
                        this._gameBoard.NumBombs += 1;
                    }
                    else
                    {
                        GameBoard.Board[jj, ii].Value = 0;
                        
                    }
                    
                }
            }
        }
        public void GameLoss()
        {
            //Ran if the game has been lost
            this._running = false;
            this._losses += 1;
            this.RevealBoard();
            MessageBox.Show("You have lost");
            
        }
        public void GameWin()
        {
            //Ran if the game has been won
            this._running = false;
            this._wins += 1;
            MessageBox.Show("You have won");
        }
        public void FlagCell(int colum, int row)
        {
            if(this.Running)
            {
                if (!_gameBoard.Board[colum, row].Opened)
                {
                    //xor opperator, inverts bool
                    this.GameBoard.Board[colum, row].Flagged ^= true;
                }
            }
        }
        
        public void FindWinLoss()
        {
            if(this._gameBoard.UnOpenedCells == this._gameBoard.NumBombs)
            {
                this.GameWin();
            }
            else if(this._gameBoard.UnOpenedCells < this._gameBoard.NumBombs)
            {
                this.GameLoss();
            }
        }
        
        public void OpenVoid(int colum, int row)
        {
            //If the value of the cell is 0, it will open the space
             if(colum >= 0 && colum < GameBoard.Dimension && row >= 0 && row < GameBoard.Dimension && !GameBoard.Board[colum, row].Opened)
            {
                if(GameBoard.Board[colum, row].Value == 0)
                {
                    GameBoard.Board[colum, row].Opened = true;
                    _gameBoard.UnOpenedCells -= 1;
                    OpenVoid(colum + 1, row);
                    OpenVoid(colum - 1, row);
                    OpenVoid(colum, row + 1);
                    OpenVoid(colum, row - 1);
                }
            }
        }
        public void calcNeighboringBombs()
        {
            for (int ii = 0; ii < GameBoard.Board.GetLength(0); ii++)
            {
                for (int jj = 0; jj < GameBoard.Board.GetLength(0); jj++)
                {
                    //jj, ii
                    if(this._gameBoard.Board[jj, ii].Value != -1)
                    {
                        this._gameBoard.Board[jj, ii].Value = 0;
                        try
                        {
                            if (this._gameBoard.Board[jj + 1, ii].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj - 1, ii].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj, ii + 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj, ii - 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj + 1, ii + 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj + 1, ii - 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj - 1, ii + 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        try
                        {
                            if (this._gameBoard.Board[jj - 1, ii - 1].Value == -1)
                            {
                                this._gameBoard.Board[jj, ii].Value += 1;
                            }
                        }
                        catch { }
                        

                    }

                }
            }
        }
        public void RevealBoard()
        {
            for (int ii = 0; ii < GameBoard.Board.GetLength(0); ii++)
            {
                for (int jj = 0; jj < GameBoard.Board.GetLength(0); jj++)
                {
                    if(this._gameBoard.Board[jj, ii].Value == -1)
                    {
                        this._gameBoard.Board[jj, ii].Opened = true;
                    }
                    
                   

                }
            }
            
        }
        public void FirstClick(int colum, int row)
        {
            //Ran on the first click, clears the bomb in the clicked cell
            if(this._firstClick)
            {
                GameBoard.Board[colum + 1, row].Value = 0;
                GameBoard.Board[colum - 1, row].Value = 0;
                GameBoard.Board[colum, row + 1].Value = 0;
                GameBoard.Board[colum, row - 1].Value = 0;
                GameBoard.Board[colum + 1, row + 1].Value = 0;
                GameBoard.Board[colum + 1, row - 1].Value = 0;
                GameBoard.Board[colum - 1, row + 1].Value = 0;
                GameBoard.Board[colum - 1, row - 1].Value = 0;
                GameBoard.Board[colum, row].Value = 0;
                _firstClick ^= true;
                this.calcNeighboringBombs();
            }
            
        }
        public void Start()
        {
            this.GameBoard.initGrid();
            this.createBoard();
            this.calcNeighboringBombs();
        }
        public void Click(int colum, int row)
        {
            if(this.Running)
            {
                this.FirstClick(colum, row);
                if (!_gameBoard.Board[colum, row].Flagged && !_gameBoard.Board[colum, row].Opened)
                {
                    if(_gameBoard.Board[colum, row].Value == 0)
                    {
                        this.OpenVoid(colum, row);
                    }
                    else
                    {
                        _gameBoard.Board[colum, row].Opened = true;
                        _gameBoard.UnOpenedCells -= 1;
                    }
                    

                    if (_gameBoard.Board[colum, row].Value == -1 && _gameBoard.Board[colum, row].Opened)
                    {
                        this.GameLoss();
                    }
                    

                    FindWinLoss();
                }
            }
            
            
        }

        
    }
}

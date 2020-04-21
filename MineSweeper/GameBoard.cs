using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MineSweeper
{
    public class GameBoard
    {
        Cell[,] _board;
        PictureBox _canvas;
        int _unOpenedCells;
        int _numBombs;
        int _dimension;
        int _cellWidth;
        

        public GameBoard(PictureBox Canvas, int Dimension)
        {
            this._canvas = Canvas;
            this._dimension = Dimension;
            _board = new Cell[Dimension, Dimension];
            this._cellWidth = CellWidth;
            this.UnOpenedCells = Dimension * Dimension;
            
        }
        public int NumBombs
        {
            get { return _numBombs; }
            set { _numBombs = value; }
        }
        public int UnOpenedCells
        {
            get { return _unOpenedCells; }
            set { _unOpenedCells = value; }
        }
        public Cell[,] Board
        {
            get { return _board; }
            set { _board = value; }
        }
        public PictureBox Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }
        public int Dimension
        {
            get { return _dimension; }
            set { _dimension = value; }
        }
       
        public int CellWidth
        {
            get
            {
                int w = Canvas.Width;
                int dim = _board.GetLength(0);
                int width = w / dim;
                return width;
            }
        }
        public void initGrid()
        {
            for (int ii = 0; ii < _board.GetLength(0); ii++)
            {
                for (int jj = 0; jj < _board.GetLength(0); jj++)
                {
                    _board[jj, ii] = new Cell(false);
                }
            }
        }
        public void DrawGrid(PaintEventArgs e)
        {
            SolidBrush unCoveredDark = new SolidBrush(Color.FromArgb(222, 184, 146));
            SolidBrush unCoveredLight = new SolidBrush(Color.FromArgb(229, 194, 159));
            SolidBrush CoveredDark = new SolidBrush(Color.FromArgb(22,130,10));
            SolidBrush CoveredLight = new SolidBrush(Color.FromArgb(170, 215, 81));
            SolidBrush textColor = new SolidBrush(Color.FromArgb(25, 118, 240));
            SolidBrush flagBrush = new SolidBrush(Color.Red);
            SolidBrush currentBrush;
            
            int margin = 1;

            for(int ii = 0; ii < _board.GetLength(0); ii++)
            {
                for(int jj = 0; jj < _board.GetLength(0); jj++)
                {
                    int x = jj * CellWidth + margin;
                    int y = ii * CellWidth + margin;
                    int w = CellWidth - margin * 2;
                    if(_board[jj,ii].Opened == true)
                    {
                        if(jj % 2 == 0 && ii % 2 == 0 || jj % 2 == 1 && ii % 2 == 1)
                        {
                            currentBrush = unCoveredDark;
                        }
                        else
                        {
                            currentBrush = unCoveredLight;
                        }
                        
                    }
                    else
                    {
                        if(jj % 2 == 0 && ii % 2 == 0 || jj % 2 == 1 && ii % 2 == 1)
                        {
                            currentBrush = CoveredDark;
                        }
                        else
                        {
                            currentBrush = CoveredLight;
                        }
                        
                    }
                    //Font is used to print letters or numbers onto cells
                    System.Drawing.Font drawFont = new System.Drawing.Font("Arial Black", CellWidth / 2);

                    //Draws the actual rectangle
                    e.Graphics.FillRectangle(currentBrush, new Rectangle(x, y, w, w));

                    //Draws the flag over the rectangle
                    if(this._board[jj,ii].Flagged)
                    {
                        e.Graphics.FillEllipse(flagBrush, new Rectangle(x, y, w, w));
                    }
                    
                    //Draws the number on the square
                    if(this._board[jj,ii].Opened && this._board[jj, ii].Value > 0)
                    {
                        e.Graphics.DrawString(String.Format("{0}", this._board[jj, ii].Value), drawFont, textColor, x + CellWidth / 6 ,y);
                    }
                    
                    

                    //The 2 lines below are used for debugging
                    //e.Graphics.DrawString(String.Format("{0},{1}",jj, ii), drawFont , textColor, x ,y);
                    //e.Graphics.DrawString(String.Format("o:{0} \nv:{1} \nf:{2}", _board[jj,ii].Opened, _board[jj, ii].Value, _board[jj,ii].Flagged), drawFont, textColor, x , y + 30);
                }
            }
        }

        //Ran When the Game is lost
        public void DrawBombs(PaintEventArgs e)
        {
            
            SolidBrush bombBrush = new SolidBrush(Color.Black);
            for (int ii = 0; ii < _board.GetLength(0); ii++)
            {
                for (int jj = 0; jj < _board.GetLength(0); jj++)
                {
                    int margin = 1;
                    int x = jj * CellWidth + margin;
                    int y = ii * CellWidth + margin;
                    int w = CellWidth - margin * 2;
                    if (this._board[jj, ii].Value == -1)
                    {
                        e.Graphics.FillEllipse(bombBrush, new Rectangle(x, y, w, w));
                    }


                }
            }
        }
        
        public void UpdateGui()
        {
            _canvas.Refresh();
        }
        public int calcCellPosition(int loc)
        {
            return (int)Math.Floor((double)loc / _cellWidth);
        }
        public void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}

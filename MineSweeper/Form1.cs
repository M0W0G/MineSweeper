using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Brendon Carroll --- 4/11/2020
//IDEAS --- create some sort of pop function, uses a delegate and performs some action in a 3x3 radius around clicked spot

namespace MineSweeper
{
    public partial class Form1 : Form
    {
        
        GameBoard GB;
        Game MSGame;
        
        
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(74, 117, 44);
            GB = new GameBoard(pctCanvas, 10);
            GB.initGrid();
            GB.Board[1, 2].Opened = true;
            MSGame = new Game(GB, this);
            MSGame.Start();
           
        }

        

        private void pctCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            //Calculates row and colum position
            int r = MSGame.GameBoard.calcCellPosition(e.Location.Y);
            int c = MSGame.GameBoard.calcCellPosition(e.Location.X);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    //For left click
                    MSGame.Click(c, r);
                    break;

                case MouseButtons.Right:
                    //For right click

                    MSGame.FlagCell(c, r);
                    

                    break;
            }

            MSGame.GameBoard.UpdateGui();
            //GB.UpdateGui();
        }

        private void pctCanvas_Paint(object sender, PaintEventArgs e)
        {
            
            MSGame.GameBoard.DrawGrid(e);
            if(!MSGame.Running)
            {
                MSGame.GameBoard.DrawBombs(e);
            }

        }

        
    }
}

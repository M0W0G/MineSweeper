using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper
{
    public class Cell
    {
        bool _opened = true;
        bool _flagged = false;
        int _value;
        public Cell(bool opened)
        {
            this._opened = opened;
        }
        public bool Opened
        {
            
            get { return _opened; }
            set { _opened = value; }
        }
        public bool Flagged
        {
            get { return _flagged; }
            set { _flagged = value; }
        }
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }
}

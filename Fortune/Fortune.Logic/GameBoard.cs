using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    public class GameBoard
    {
        public List<Field> Fields { get; }

        public GameBoard(List<Field> fields)
        {
            this.Fields = fields;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic
{
    public class Continent
    {
        public string Name { get; private set; }
        public Color Color { get; private set; }

        public Continent(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

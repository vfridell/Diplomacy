using DiplomacyLib;
using DiplomacyLib.Models;
using DiplomacyLib.Visualize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphviz
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            renderer.Render(Maps.Full);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelGen.Cli
{
    public class Tool
    {
        public float diameter; // Tool diameter
        public float zStep; // Max z-step when doing multipass holes
        public float radius => diameter/2;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class Renk
    {
        public List<Color> colors = new List<Color>();
        public Renk()
        {
            colors.Add(Color.DarkRed);
            colors.Add(Color.DarkBlue);
            colors.Add(Color.DarkViolet);
            colors.Add(Color.DeepPink);
            colors.Add(Color.DarkTurquoise);
        }
    }
}

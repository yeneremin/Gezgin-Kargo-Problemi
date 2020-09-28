 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    public class Komsu
    {
        public string Komsuİsmi;
        public int KomsuPlaka;
        public double Mesafe;

        public Komsu(string Komsuİsmi, int KomsuPlaka, double Mesafe)
        {
            this.Komsuİsmi = Komsuİsmi;
            this.KomsuPlaka = KomsuPlaka;
            this.Mesafe = Mesafe;
        }
    }
}

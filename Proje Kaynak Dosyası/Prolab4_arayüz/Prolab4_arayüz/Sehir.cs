using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    public class Sehir
    {
        public string SehirAd { get; set; }
        public int KomsuSayisi { get; set; }
        public int Plaka { get; set; }

        private List<Komsu> _Komsular = new List<Komsu>();
        public List<Komsu> Komsular
        {
            get { return _Komsular; }
            set { _Komsular = value; }
        }
        public double[] Koordinatlar = new double[2];

    }
}

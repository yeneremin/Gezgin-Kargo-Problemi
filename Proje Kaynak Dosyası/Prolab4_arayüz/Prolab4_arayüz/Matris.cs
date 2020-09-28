using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class Matris
    {
        private string dosyaYolu = "komsuuzaklik.txt";
        private double[,] AgirlikMatrisi = new double[81, 81];
        public double[,] A_Matrisi
        {
            get { return AgirlikMatrisi; }
        }
        public Matris()
        {
            A_MatrisiOlustur();
        }

        public void A_MatrisiOlustur()
        {
            const char ayrac = ',';
            FileStream fs = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string satir = reader.ReadLine();
            int satirsayac = 0;
            while (satir != null)
            {
                string[] satirDizisi = satir.Split(ayrac);
                int sutunsayac = 0;
                foreach (var cumle in satirDizisi)
                {
                    if (sutunsayac > 0)
                    {
                        bool kelimedurum = true;
                        foreach (var kelime in cumle)
                        {
                            if (!Char.IsDigit(kelime))
                            {
                                kelimedurum = false;
                                break;
                            }
                        }
                        if (kelimedurum)
                        {
                            if (Convert.ToInt32(cumle) == 0)
                            {
                                AgirlikMatrisi[satirsayac, sutunsayac - 1] = 0;
                            }
                            if (Convert.ToInt32(cumle) != 0)
                            {
                                AgirlikMatrisi[satirsayac, sutunsayac - 1] = Convert.ToInt32(cumle);
                            }
                        }
                        else
                        {
                            AgirlikMatrisi[satirsayac, sutunsayac - 1] = 0;
                        }
                    }
                    sutunsayac++;
                }
                satirsayac++;
                satir = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
        }
    }
}

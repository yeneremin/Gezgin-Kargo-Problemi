using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class HaritaOlustur
    {
        private List<Sehir> Sehirler;
        private double[,] Piksel;

        public HaritaOlustur(ref List<Sehir> Sehirler)
        {
            this.Sehirler = Sehirler;
            Piksel = new double[81,2];
            PikselOku();
            pikselOlustur();
        }

        private void pikselOlustur()
        {
            for (int i = 0; i <=Piksel.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= Piksel.GetUpperBound(1); j++)
                {
                    Sehirler[i].Koordinatlar[j] = Piksel[i,j]*0.75;
                }
            }
        }

        private void PikselOku()
        {
            string dosyaYolu = "pikseller.txt";
            FileStream fileStream = new FileStream(dosyaYolu,FileMode.Open,FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            string oku = streamReader.ReadLine();
            int satirsayac = 0;
            while (oku != null)
            {
                string[] pikseller = oku.Split(',');
                int sutunsayac = 0;
                foreach (var piksel in pikseller)
                {
                    Piksel[satirsayac, sutunsayac] = Convert.ToInt32(piksel);
                    sutunsayac++;
                }
                satirsayac++;
                oku = streamReader.ReadLine();
            }
            streamReader.Close();
            fileStream.Close();
        }
    }
}

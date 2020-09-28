using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class BruteForce
    {
        public List<List<int>> GidilenTumSehirler { get; set; }
        public List<double> ToplamMesafe { get; set; }
        public List<List<int>> GidilenOptimumSehirler { get; set; }
        public List<double> ToplamOptimumMesafe { get; set; }
        public double[,] FloydWarshall { get; set; }
        public int Optimumincrease
        {
            get
            {
                if (ToplamMesafe.Count() >= 5) return 5;
                else return ToplamMesafe.Count();
            }
        }

        private Random random;

        public BruteForce(List<List<int>> GidilenTumSehirler, double[,] FloydWarshall)
        {
            random = new Random();
            ToplamMesafe = new List<double>();
            ToplamOptimumMesafe = new List<double>();
            GidilenOptimumSehirler = new List<List<int>>();

            this.GidilenTumSehirler = GidilenTumSehirler;
            this.FloydWarshall = FloydWarshall;
        }

        public void bruteForce()
        {
            List<int> Yol = new List<int>();

            for (int i = 0; i < GidilenTumSehirler.Count(); i++)
            {
                double KayıtUzunluk1 = 0, KayıtUzunluk2 = 0;

                for (int j = 0; j < 1000; j++)
                {
                    int random1 = random.Next(1, GidilenTumSehirler[i].Count() - 1);
                    int random2 = random.Next(1, GidilenTumSehirler[i].Count() - 1);
                    swap(GidilenTumSehirler[i], random1, random2);

                    if (j == 0)
                    {
                        KayıtUzunluk1 = yolHesapla(GidilenTumSehirler[i]);
                        Yol = GidilenTumSehirler[i];
                    }
                    else
                    {
                        KayıtUzunluk2 = yolHesapla(GidilenTumSehirler[i]);
                        if (KayıtUzunluk1 > KayıtUzunluk2)
                        {
                            KayıtUzunluk1 = KayıtUzunluk2;
                            Yol = GidilenTumSehirler[i].ToList();
                        }
                    }
                }

                ToplamMesafe.Add(KayıtUzunluk1);
                GidilenTumSehirler[i] = Yol.ToList();
            }
        }

        public void OptimumSonuclar()
        {
            List<int> Yol = new List<int>();

            for (int i = 0; i < GidilenTumSehirler.Count(); i++)
            {
                for (int j = i + 1; j < GidilenTumSehirler.Count(); j++)
                {
                    if (ToplamMesafe[i] > ToplamMesafe[j])
                    {
                        double temp = ToplamMesafe[i];
                        ToplamMesafe[i] = ToplamMesafe[j];
                        ToplamMesafe[j] = temp;

                        Yol = GidilenTumSehirler[i];
                        GidilenTumSehirler[i] = GidilenTumSehirler[j];
                        GidilenTumSehirler[j] = Yol;
                    }
                }
            }

            for (int i = ToplamMesafe.Count() - 1; i > 0; i--)
            {
                if (ToplamMesafe[i] == ToplamMesafe[i - 1])
                {
                    GidilenTumSehirler.RemoveAt(i - 1);
                    ToplamMesafe.RemoveAt(i - 1);
                    i = ToplamMesafe.Count() - 1;
                }
            }

            for (int i = 0; i < Optimumincrease; i++)
            {
                ToplamOptimumMesafe.Add(ToplamMesafe[i]);
                GidilenOptimumSehirler.Add(GidilenTumSehirler[i]);
            }
        }

        public void swap(List<int> yol, int i, int j)
        {
            int temp = yol[i];
            yol[i] = yol[j];
            yol[j] = temp;
        }

        public double yolHesapla(List<int> yol)
        {
            double mesafe = 0;

            for (int i = 0; i < yol.Count - 1; i++)
                mesafe += FloydWarshall[yol[i] - 1, yol[i + 1] - 1];

            return mesafe;
        }
    }
}
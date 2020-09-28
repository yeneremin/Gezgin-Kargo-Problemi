using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class SistemKayıt
    {
        private string dosyaYolu = "komsuuzaklik.txt";
        private (int plakaKodu, int komsusayisi, string sehirİsim)[] sehirler = new (int, int, string)[81];
        private List<Sehir> Tlist;
        private double[,] AMatrisi;
            
        public SistemKayıt(List<Sehir> Tlist, double[,] AMatrisi)
        {
            this.Tlist = Tlist;
            this.AMatrisi = AMatrisi;
        }

        public List<Sehir> SehirKayit()
        {
            DosyaOku();
            for (int i = 0; i < 81; i++)
            {
                Sehir sehir = new Sehir();
                sehir.SehirAd = sehirler[i].sehirİsim;
                sehir.KomsuSayisi = sehirler[i].komsusayisi;
                sehir.Plaka = sehirler[i].plakaKodu;
                for (int j = 0; j < 81; j++)
                {
                    if (AMatrisi[i, j] != 0)
                    {
                        string KomsuAd = sehirler[j].sehirİsim;
                        int KomsuPlaka = sehirler[j].plakaKodu;
                        double mesafe = AMatrisi[i, j];
                        Komsu komsu = new Komsu(KomsuAd, KomsuPlaka, mesafe);
                        sehir.Komsular.Add(komsu);
                    }
                }
                Tlist.Add(sehir);
            }
            return Tlist;
        }

        public void DosyaOku()
        {
            const char ayrac = ',';
            FileStream fs = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(fs);
            string satir = reader.ReadLine();
            int satirsayac = 0;
            while (satir != null)
            {
                int plakatut = 0, komsusayisi = 0;
                string[] satirDizisi = satir.Split(ayrac);
                int sutunsayac = 0;
                foreach (var cumle in satirDizisi)
                {
                    if (sutunsayac == 0) plakatut = Convert.ToInt32(cumle);
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
                            if (Convert.ToInt32(cumle) != 0)
                            {
                                komsusayisi++;
                            }
                        }
                        else
                        {
                            sehirler[satirsayac] = (plakatut, 0, cumle);
                        }
                    }
                    sutunsayac++;
                }
                sehirler[satirsayac].komsusayisi = komsusayisi;
                satirsayac++;
                satir = reader.ReadLine();
            }
            reader.Close();
            fs.Close();
        }
    }
}

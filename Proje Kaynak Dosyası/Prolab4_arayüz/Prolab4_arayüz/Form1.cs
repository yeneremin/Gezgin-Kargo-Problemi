using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;
using System.Threading;

namespace Prolab4_arayüz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Matris matris;
        private List<Sehir> Sehirler;
        private SistemKayıt sistemKayıt;
        private FloydWarshall floydWarshall;
        private HaritaOlustur harita;
        private const int size = 81;
        private List<string> AranacakSehirler;
        private Renk renk = new Renk();
        private List<int> PlakaTut;
        private List<int> GidilenSehirler;
        private List<List<int>> GidilenToplamSehirler;
        private const int AramaHavuzu = 10000;
        private List<List<int>> OptimumYollar;
        private List<double> OptimumMesafe;
        private int Optimumincrease;
        private int HaritaDegisimKatsayisi = 0;
        private Form2 form2;
        private int cizdir = -1;
        private Graphics graphics;

        private void Form1_Load(object sender, EventArgs e)
        {
            matris = new Matris();
            Sehirler = new List<Sehir>();
            sistemKayıt = new SistemKayıt(Sehirler, matris.A_Matrisi);
            Sehirler = sistemKayıt.SehirKayit();
            floydWarshall = new FloydWarshall(matris.A_Matrisi, 81);
            harita = new HaritaOlustur(ref Sehirler);
            AranacakSehirler = new List<string>();
            PlakaTut = new List<int>();
            GidilenSehirler = new List<int>();
            GidilenToplamSehirler = new List<List<int>>();
            form2 = new Form2();
            graphics = pictureBox1.CreateGraphics();

            MessageBox.Show("Sehir İsimleri Büyük Harfle Başlamalı Ve \n" +
                "Türkçe Karakter İçermemelidir. \n" +
                "Örn : Istanbul,Izmir...", "Bilgilendirme", MessageBoxButtons.OK,
            MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            yolCizdir(e.Graphics);
            pictureBox1.Update();
        }

        private void enkisayolCizdir(Graphics e, int j)
        {
            if (HaritaDegisimKatsayisi < Optimumincrease && HaritaDegisimKatsayisi != -1)
            {
                Pen pen = new Pen(renk.colors[j], 8F);
                for (int i = 0; i < OptimumYollar[j].Count - 1; i++)
                {
                    List<int> noktalar = new List<int>();
                    noktalar.Add(OptimumYollar[j][i]);
                    string[] floydNoktalar = floydWarshall.pathsLogs[OptimumYollar[j][i] - 1, OptimumYollar[j][i + 1] - 1].Split(',');

                    for (int h = 0; h < floydNoktalar.Length; h++)
                    {
                        noktalar.Add(Convert.ToInt32(floydNoktalar[h]));
                    }
                    noktalar.Add(OptimumYollar[j][i + 1]);

                    for (int k = 0; k < noktalar.Count - 1; k++)
                    {
                        double x1, y1, x2, y2;
                        x1 = Sehirler[noktalar[k] - 1].Koordinatlar[0];
                        y1 = Sehirler[noktalar[k] - 1].Koordinatlar[1];
                        x2 = Sehirler[noktalar[k + 1] - 1].Koordinatlar[0];
                        y2 = Sehirler[noktalar[k + 1] - 1].Koordinatlar[1];

                        Point point1 = new Point((int)x1, (int)y1);
                        Point point2 = new Point((int)x2, (int)y2);
                        e.DrawLine(pen, point1, point2);
                    }
                    noktalar.Clear();
                }
               pictureBox1.Update();
            }
        }

        private void yolCizdir(Graphics e)
        {
            Pen BlackPen = new Pen(Color.Black, 1.5F);
            for (int i = 0; i < size; i++)
            {
                double x = Sehirler[i].Koordinatlar[0];
                double y = Sehirler[i].Koordinatlar[1];
                Point point1 = new Point((int)x, (int)y);

                for (int j = 0; j < Sehirler[i].KomsuSayisi; j++)
                {
                    int komsuPlakatut = Sehirler[i].Komsular[j].KomsuPlaka;
                    double komsuX = Sehirler[komsuPlakatut - 1].Koordinatlar[0];
                    double komsuY = Sehirler[komsuPlakatut - 1].Koordinatlar[1];
                    Point point2 = new Point((int)komsuX, (int)komsuY);
                    e.DrawLine(BlackPen, point1, point2);
                }
            }
        }

        private void btnsehirismi_Click(object sender, EventArgs e)
        {
            int denetle = 0;
            for (int h = 0; h < AranacakSehirler.Count; h++)
            {
                if (txtsehiradi.Text == AranacakSehirler[h])
                {
                    MessageBox.Show("Sisteme Bu Şehri Eklemiştiniz!!", "uyarı", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    denetle = 3;
                    break;
                }
            }
            for (int i = 0; i < Sehirler.Count; i++)
            {
                if (denetle == 3) break;
                else if (txtsehiradi.Text == Sehirler[i].SehirAd)
                {
                    AranacakSehirler.Add(txtsehiradi.Text);

                    denetle = 1;
                    break;
                }
                else if (txtsehiradi.Text == "Kocaeli")
                {
                    MessageBox.Show("Kocaeli Şehirini Sisteme Ekleyemezsiniz!!", "uyarı", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                    denetle = 2;
                    break;
                }
            }

            if (denetle == 0)
                MessageBox.Show("Girdiğiniz Şehir Bulunamadı!!", "uyarı", MessageBoxButtons.OK,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);


            txtsehiradi.Clear();
            GirilenSehirSayisilbl_Degistir(AranacakSehirler);
        }

        private void GirilenSehirSayisilbl_Degistir(List<string> SistemdekiSehirler)
        {
            girilensehirlbl.Text = "";
            girilensehirlbl.Text = SistemdekiSehirler.Count().ToString();
        }

        private void AranacakSehirPlakasiTut(ref List<int> PlakaTut)
        {
            for (int i = 0; i < AranacakSehirler.Count; i++)

                for (int j = 0; j < Sehirler.Count; j++)

                    if (Sehirler[j].SehirAd == AranacakSehirler[i])
                        PlakaTut.Add(Sehirler[j].Plaka);
        }

        private void btnaramayap_Click(object sender, EventArgs e)
        {
            if (AranacakSehirler.Count < 3 || AranacakSehirler.Count > 10)
            {
                MessageBox.Show("En Az 3, En Fazla 10 Sehir Girmelisin!! \n\n Lütfen Tekrar Giriş Yapın!!", "uyarı", MessageBoxButtons.OK,
                 MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                degisim = 1;
                btnsistemisıfırla_Click(null, null);
            }
            else
            {
                AranacakSehirPlakasiTut(ref PlakaTut);
                Random random = new Random();
                List<int> sahtePlakaTut = new List<int>();

                int tur = PlakaTut.Count;

                for (int j = 0; j < AramaHavuzu; j++)
                {
                    for (int i = 0; i < PlakaTut.Count; i++)
                        sahtePlakaTut.Add(PlakaTut[i]);


                    GidilenSehirler.Add(41);
                    for (int i = 0; i < tur; i++)
                    {
                        int randomDeger = random.Next(0, sahtePlakaTut.Count);
                        GidilenSehirler.Add(sahtePlakaTut[randomDeger]);
                        sahtePlakaTut.RemoveAt(randomDeger);
                    }
                    GidilenSehirler.Add(41);

                    if (j == 0)
                        GidilenToplamSehirler.Add(GidilenSehirler.ToList());

                    else if (j > 0)
                    {
                        int r = 0;

                        for (r = 0; r < GidilenToplamSehirler.Count(); r++)
                        {
                            int benzerlikOrani = 0;

                            for (int i = 0; i < GidilenSehirler.Count(); i++)

                                if (GidilenToplamSehirler[r][i] == GidilenSehirler[i])
                                    benzerlikOrani++;

                            if (benzerlikOrani == GidilenSehirler.Count()) break;
                        }
                        if (r == GidilenToplamSehirler.Count()) GidilenToplamSehirler.Add(GidilenSehirler.ToList());
                    }
                    GidilenSehirler.Clear();
                }

                BruteForce bruteFroce = new BruteForce(GidilenToplamSehirler, floydWarshall.FloydMatrisi);
                bruteFroce.bruteForce();
                bruteFroce.OptimumSonuclar();

                OptimumMesafe = bruteFroce.ToplamOptimumMesafe.ToList();
                OptimumYollar = bruteFroce.GidilenOptimumSehirler.ToList();
                Optimumincrease = bruteFroce.Optimumincrease;
            }
        }

        int degisim = 0;
        private void btnsistemisıfırla_Click(object sender, EventArgs e)
        {


            if (degisim == 1)
            {
                GidilenSehirler.Clear();
                AranacakSehirler.Clear();
                PlakaTut.Clear();
                HaritaDegisimKatsayisi = -1;
                Optimumincrease = 0;
                degisim = 0;
            }
            else
            {
                int Uzunluk;
                if (PlakaTut.Count > 0)
                {
                    Uzunluk = OptimumYollar.Count();
                    for (int i = 0; i < Uzunluk; i++)
                        OptimumYollar[i].Clear();

                    OptimumMesafe.Clear();
                }
                Uzunluk = GidilenToplamSehirler.Count();
                for (int i = Uzunluk - 1; i >= 0; i--)
                    GidilenToplamSehirler.RemoveAt(i);
                GidilenSehirler.Clear();
                AranacakSehirler.Clear();
                PlakaTut.Clear();
                HaritaDegisimKatsayisi = -1;
                Optimumincrease = 0;
            }
            GirilenSehirSayisilbl_Degistir(AranacakSehirler);
            this.Refresh();
        }

        private void btnsonuclar_Click(object sender, EventArgs e)
        {
            form2.SonuclarTablosu(Sehirler, OptimumYollar, OptimumMesafe, Optimumincrease, floydWarshall.pathsLogs);
            form2.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HaritaDegisimKatsayisi = 0;
            enkisayolCizdir(graphics, HaritaDegisimKatsayisi);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HaritaDegisimKatsayisi = 1;
            enkisayolCizdir(graphics, HaritaDegisimKatsayisi);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HaritaDegisimKatsayisi = 2;
            enkisayolCizdir(graphics, HaritaDegisimKatsayisi);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HaritaDegisimKatsayisi = 3;
            enkisayolCizdir(graphics, HaritaDegisimKatsayisi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HaritaDegisimKatsayisi = 4;
            enkisayolCizdir(graphics, HaritaDegisimKatsayisi);
        }
    }
}

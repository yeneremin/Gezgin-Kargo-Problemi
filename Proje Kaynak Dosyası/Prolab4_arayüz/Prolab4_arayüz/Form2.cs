using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prolab4_arayüz
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            rchtxt.Clear();
            this.Visible = false;
        }
        public void SonuclarTablosu(List<Sehir> Sehirler, List<List<int>> OptimumYollar, List<double> OptimumMesafe, int Optimumincrease, string[,] PathLogs)
        {
            rchtxt.AppendText("Bulunan Sonuçlar: \n");

            for (int i = 0; i < Optimumincrease; i++)
            {
                List<int> noktalar = new List<int>();

                rchtxt.AppendText((i + 1) + ". Cözüm \n");
                rchtxt.AppendText("Toplam Mesafe : " + OptimumMesafe[i] + "\n");

                for (int j = 0; j < OptimumYollar[i].Count(); j++)
                {
                    rchtxt.AppendText(OptimumYollar[i][j].ToString());
                    if (j != OptimumYollar[i].Count - 1) rchtxt.AppendText(", ");
                }
                rchtxt.AppendText("\n");

                int tekrar = 0;
                for (int k = 0; k < OptimumYollar[i].Count() - 1; k++)
                {
                    string[] floydNoktalar = PathLogs[OptimumYollar[i][k] - 1, OptimumYollar[i][k + 1] - 1].Split(',');

                    for (int h = 0; h < floydNoktalar.Length; h++)
                    {
                        if (tekrar > 0 && h == 0)
                            continue;
                        noktalar.Add(Convert.ToInt32(floydNoktalar[h]));

                    }
                    tekrar++;
                }

                for (int h = 0; h < noktalar.Count(); h++)
                {
                    rchtxt.AppendText(Sehirler[noktalar[h] - 1].SehirAd);
                    if (h != noktalar.Count - 1)
                        rchtxt.AppendText(", ");
                }
                rchtxt.AppendText("\n");
            }
        }
    }
}


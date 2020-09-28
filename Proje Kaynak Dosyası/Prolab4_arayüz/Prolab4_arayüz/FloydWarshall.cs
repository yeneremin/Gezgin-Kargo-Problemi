using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab4_arayüz
{
    class FloydWarshall
    {
        public double[,] FloydMatrisi;
        public string[,] pathsLogs;
        private int[,] Paths;
        private int size;
        public FloydWarshall(double[,] A_Matrisi,int size)
        {
            this.size = size;
            this.pathsLogs = new string[size, size];
            this.Paths = new int[size, size];
            this.FloydMatrisi = matrisSıfırla(A_Matrisi);
            FloydMatrisi = floyd(FloydMatrisi, ref Paths);
            PathsLog(FloydMatrisi, Paths, Paths.GetUpperBound(0), ref pathsLogs);
        }
        private void PathsLog(double[,] FloydMatrisi, int[,] Paths, int v, ref string[,] pathsLogs)
        {
            for (int i = 0; i <= v; i++)
            {
                for (int j = 0; j <= v; j++)
                {
                    if (i != j && Paths[i, j] != -1)
                    {
                        pathsLogs[i, j] += ((i + 1) + ",");
                        yolBul(Paths, i, j, ref pathsLogs[i, j]);
                        pathsLogs[i, j] += (j + 1);
                    }
                    else if (i == j || Paths[i, j] == -1)
                    {
                        pathsLogs[i, j] = null;
                    }
                }
            }
        }

        private void yolBul(int[,] Paths, int i, int j, ref string pathsLog)
        {
            if (Paths[i, j] == i) return;

            yolBul(Paths, i, Paths[i, j], ref pathsLog);
            pathsLog += (Paths[i, j] + 1) + ",";
        }

        private double[,] floyd(double[,] FloydMatrisi, ref int[,] Paths)
        {
            int size = FloydMatrisi.GetUpperBound(0);

            for (int i = 0; i <= size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    if (i == j) Paths[i, j] = 0;

                    else if (FloydMatrisi[i, j] != double.PositiveInfinity) Paths[i, j] = i;

                    else Paths[i, j] = -1;
                }
            }

            for (int i = 0; i <= size; i++)
            {
                for (int j = 0; j <= size; j++)
                {
                    for (int k = 0; k <= size; k++)
                    {
                        if (FloydMatrisi[j, k] > FloydMatrisi[j, i] + FloydMatrisi[i, k])
                        {
                            FloydMatrisi[j, k] = FloydMatrisi[j, i] + FloydMatrisi[i, k];
                            Paths[j, k] = Paths[i, k];
                        }
                    }
                }
            }
            return FloydMatrisi;
        }

        private double[,] matrisSıfırla(double[,] matrix)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) continue;
                    if (matrix[i, j] == 0) matrix[i, j] = Double.PositiveInfinity;
                }
            }
            return matrix;
        }
    }
}

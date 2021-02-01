using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace labirintoSemArquivo
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader arquivo = new StreamReader("entrada-labirinto.txt");
            //string[] dimensaostring = arquivo.ReadLine().Split(' ');
            // int[] dimensaoint = Array.ConvertAll(dimensaostring, int.Parse);
            int[] dimensao = Array.ConvertAll(arquivo.ReadLine().Split(' '), int.Parse);
            int linha = dimensao[0];
            int coluna = dimensao[1];
            string[,] lab = new string[linha + 1, coluna + 1];

            lab[1, 1] = "1"; lab[1, 2] = "1"; lab[1, 3] = "1"; lab[1, 4] = "1"; lab[1, 5] = "1"; lab[1, 6] = "1"; lab[1, 7] = "1"; lab[1, 8] = "1";
            lab[2, 1] = "1"; lab[2, 2] = "1"; lab[2, 3] = "0"; lab[2, 4] = "1"; lab[2, 5] = "0"; lab[2, 6] = "1"; lab[2, 7] = "1"; lab[2, 8] = "1";
            lab[3, 1] = "1"; lab[3, 2] = "1"; lab[3, 3] = "0"; lab[3, 4] = "0"; lab[3, 5] = "0"; lab[3, 6] = "1"; lab[3, 7] = "1"; lab[3, 8] = "1";
            lab[4, 1] = "X"; lab[4, 2] = "0"; lab[4, 3] = "0"; lab[4, 4] = "1"; lab[4, 5] = "0"; lab[4, 6] = "0"; lab[4, 7] = "0"; lab[4, 8] = "0";
            lab[5, 1] = "1"; lab[5, 2] = "1"; lab[5, 3] = "1"; lab[5, 4] = "1"; lab[5, 5] = "1"; lab[5, 6] = "1"; lab[5, 7] = "1"; lab[5, 8] = "1";

            var origem2 = new List<int[]>();

            var origem = new Tuple<int, int>(0, 0);
            var livres = new List<Tuple<int, int>>();
            var parede = new List<Tuple<int, int>>();

            for (int i = 1; i <= linha; i++)
            {
                for (int j = 1; j <= coluna; j++)
                {
                    if (lab[i, j] == "X")
                    {
                        origem = new Tuple<int, int>(i, j);
                        //origem.Add(new Tuple<int, int>(i, j)); 
                        //int[] indices = new int[] { i, j };
                        //origem2.Add(indices);

                        if (i == 1)
                        {
                            parede.Add(new Tuple<int, int>(0, j));

                        }
                        if (j == 1)
                        {
                            parede.Add(new Tuple<int, int>(i, 0));
                        }
                    }
                    else if (lab[i, j] == "0")
                    {
                        livres.Add(new Tuple<int, int>(i, j));
                    }
                    else
                    {
                        parede.Add(new Tuple<int, int>(i, j));
                    }

                    //Console.Write(lab[i, j]);
                    //Console.Write(' ');
                }
                //Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(origem);
            origem = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
            Console.WriteLine(origem);
            origem = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
            Console.WriteLine(origem);
            origem = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
            Console.WriteLine(origem);
            origem = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
            Console.WriteLine(origem);


            //Console.WriteLine(parede.Count);
            //Console.WriteLine(lab[4, 1]);
            Console.ReadLine();
            arquivo.Close();
        }
    }
}

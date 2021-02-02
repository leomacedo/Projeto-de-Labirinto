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
            var percorridos = new List<Tuple<int, int>>();
            var caminho = new Stack<Tuple<int, int>>();

            for (int i = 1; i <= linha; i++)
            {
                for (int j = 1; j <= coluna; j++)
                {
                    if (lab[i, j] == "X")
                    {
                        origem = new Tuple<int, int>(i, j);

                        if (i == 1)
                        {
                            parede.Add(new Tuple<int, int>(0, j));

                        }
                        if (j == 1)
                        {
                            parede.Add(new Tuple<int, int>(i, 0));
                        }
                        if (i == linha)
                        {
                            parede.Add(new Tuple<int, int>(linha + 1, j));
                        }
                        if (j == coluna)
                        {
                            parede.Add(new Tuple<int, int>(i, coluna + 1));
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

                }

            }
            StreamWriter arqsaida = new StreamWriter("saida.txt");

            arqsaida.WriteLine("O [" + origem.Item1 + ", " + origem.Item2 + "]");
            //arqsaida.WriteLine(origem);
            //arqsaida.WriteLine("oi");

            var movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
            var movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
            var movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
            var movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);

            if (origem.Item1 == 1 || origem.Item2 == 1 || origem.Item1 == linha || origem.Item2 == coluna)
            {

                foreach (Tuple<int, int> parte in livres)
                {

                    if (movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movesquerda.Item1 == parte.Item1 && movesquerda.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movedireita.Item1 == parte.Item1 && movedireita.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        percorridos.Add(parte);
                        arqsaida.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movebaixo.Item1 == parte.Item1 && movebaixo.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }  
                };
            };

            bool saiu = false;

            while (saiu == false)
            {

                if (origem.Item1 == 1 || origem.Item2 == 1 || origem.Item1 == linha || origem.Item2 == coluna)
                {
                    saiu = true;
                }

                foreach (Tuple<int, int> parte in livres)
                {

                    if (movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movesquerda.Item1 == parte.Item1 && movesquerda.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movedireita.Item1 == parte.Item1 && movedireita.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        percorridos.Add(parte);
                        arqsaida.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (movebaixo.Item1 == parte.Item1 && movebaixo.Item2 == parte.Item2)
                    {
                        livres.Remove(parte);
                        arqsaida.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
                        percorridos.Add(parte);
                        caminho.Push(parte);
                        origem = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                        break;
                    }
                    else if (livres.ForEach(parte) != origem)
                        //movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2 ||
                        //    movesquerda.Item1 == parte.Item1 && movesquerda.Item2 == parte.Item2 ||
                        //    movedireita.Item1 == parte.Item1 && movedireita.Item2 == parte.Item2 ||
                        //    (movebaixo.Item1 == parte.Item1 && movebaixo.Item2 == parte.Item2))
                    {
                        livres.Remove(caminho.Peek());
                        percorridos.Remove(caminho.Peek());
                        caminho.Pop();
                        if (caminho.Peek().Item1 == origem.Item1 - 1 && caminho.Peek().Item2 == origem.Item2) { arqsaida.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]"); };
                        if (caminho.Peek().Item1 == origem.Item1  && caminho.Peek().Item2 -1 == origem.Item2) { arqsaida.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]"); };
                        if (caminho.Peek().Item1 == origem.Item1  && caminho.Peek().Item2 +1 == origem.Item2) { arqsaida.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]"); };        
                        if (caminho.Peek().Item1 == origem.Item1 + 1 && caminho.Peek().Item2 == origem.Item2) { arqsaida.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]"); };

                        origem = new Tuple<int, int>(caminho.Peek().Item1, caminho.Peek().Item2);
                        movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
                        movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
                        movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
                        movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
                    };
                    //Console.WriteLine(origem);
                }
                //Console.WriteLine(origem);
            }
            Console.WriteLine(origem);

            //Console.WriteLine(origem);
            //foreach (Tuple<int, int> parte in livres)
            //{
            //    Console.WriteLine(parte);
            //}
            //origem = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
            //Console.WriteLine(movedireita);
            //Console.WriteLine(origem);
            //if (origem.Item1 == movedireita.Item1 && origem.Item2 == movedireita.Item2)
            //{
            //    Console.WriteLine("Entrei no if");
            //}else
            //{
            //    Console.WriteLine("Não entrei no if");
            //}
            //Console.WriteLine(parede.Count);
            //Console.WriteLine(lab[4, 1]);
            Console.ReadLine();
            arquivo.Close();
            arqsaida.Close();
        }

    }
}

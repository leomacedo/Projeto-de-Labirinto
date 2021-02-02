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
        // Variaveis Globais que se usam tanto no programa principal como ña função ResetaMovimentos()
        static Tuple<int, int> origem = new Tuple<int, int>(0, 0);
        static Tuple<int, int> movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
        static Tuple<int, int> movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
        static Tuple<int, int> movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
        static Tuple<int, int> movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);

        // Função para resetar os movimentos percorrendo no labirinto
        static void ResetaMovimentos()
        {
            movecima = new Tuple<int, int>(origem.Item1 - 1, origem.Item2);
            movesquerda = new Tuple<int, int>(origem.Item1, origem.Item2 - 1);
            movedireita = new Tuple<int, int>(origem.Item1, origem.Item2 + 1);
            movebaixo = new Tuple<int, int>(origem.Item1 + 1, origem.Item2);
        }
        static void Main(string[] args)
        {
            /*
             * ERRO: Arquivo de entrada só está lendo as duas primeiras linhas. Armazenando a dimensão
             */
            StreamReader arquivo = new StreamReader("entrada-labirinto.txt");
            int[] dimensao = Array.ConvertAll(arquivo.ReadLine().Split(' '), int.Parse);
            int linha = dimensao[0];
            int coluna = dimensao[1];

            var livres = new List<Tuple<int, int>>(); // Lista de Tuplas de Caminhos Livres
            var percorridos = new List<Tuple<int, int>>(); // Lista de Tuplas de Caminhos Percorridos
            var caminho = new Stack<Tuple<int, int>>(); // Pilha de Tuplas da Posição da Origem

            /*
             * ERRO: Desenho do labirinto. Era pra ler a partir do arquivo. Caso consiga favor apagar essa parte de codigo
             */
            string[,] lab = new string[linha + 2, coluna + 2];
            lab[1, 1] = "1"; lab[1, 2] = "1"; lab[1, 3] = "1"; lab[1, 4] = "1"; lab[1, 5] = "1"; lab[1, 6] = "1"; lab[1, 7] = "1"; lab[1, 8] = "1";
            lab[2, 1] = "1"; lab[2, 2] = "1"; lab[2, 3] = "0"; lab[2, 4] = "1"; lab[2, 5] = "0"; lab[2, 6] = "1"; lab[2, 7] = "1"; lab[2, 8] = "1";
            lab[3, 1] = "1"; lab[3, 2] = "1"; lab[3, 3] = "0"; lab[3, 4] = "0"; lab[3, 5] = "0"; lab[3, 6] = "1"; lab[3, 7] = "1"; lab[3, 8] = "1";
            lab[4, 1] = "X"; lab[4, 2] = "0"; lab[4, 3] = "0"; lab[4, 4] = "1"; lab[4, 5] = "0"; lab[4, 6] = "0"; lab[4, 7] = "0"; lab[4, 8] = "0";
            lab[5, 1] = "1"; lab[5, 2] = "1"; lab[5, 3] = "1"; lab[5, 4] = "1"; lab[5, 5] = "1"; lab[5, 6] = "1"; lab[5, 7] = "1"; lab[5, 8] = "1";


            // Definindo em que parte está a origem e adicionando os caminhos livres na lista de Tuplas
            for (int i = 1; i <= linha; i++)
            {
                for (int j = 1; j <= coluna; j++)
                {
                    if (lab[i, j] == "X")
                    {
                        origem = new Tuple<int, int>(i, j);
                    }
                    else if (lab[i, j] == "0")
                    {
                        livres.Add(new Tuple<int, int>(i, j));
                    }

                }

            }
            arquivo.Close();

            // Vizualização no Console para testes. Pode ser Comentado.
            // Vão existir varias vizualizações no códgio caso não comente nenhum. o Console irá exibir todo o caminho que deveria aparecer no arquivo.
            Console.WriteLine(origem);


            // Armazenando o caminho do labirinto num arquivo texto.
            using (StreamWriter arquivo3 = new StreamWriter("saida3.txt", true))
            {
                arquivo3.WriteLine("O [" + origem.Item1 + ", " + origem.Item2 + "]");

                ResetaMovimentos(); // Atribuindo valores de movimentos 

                // Primeira condição é verificar se o X está em alguma extremidade. Se tiver executa o primeiro movimento
                if (origem.Item1 == 1 || origem.Item2 == 1 || origem.Item1 == linha || origem.Item2 == coluna)
                {

                    foreach (Tuple<int, int> parte in livres)
                    {

                        // Movimento para cima
                        if (movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para esquerda
                        else if (movesquerda.Item1 == parte.Item1 && movesquerda.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para direita
                        else if (movedireita.Item1 == parte.Item1 && movedireita.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para baixo
                        else if (movebaixo.Item1 == parte.Item1 && movebaixo.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                    };
                };
                // Vizualização no Console para testes. Pode ser Comentado.
                Console.WriteLine(origem);

                bool saiu = false; // Variavel booleana quando sair do labirinto
                while (saiu == false)
                {

                    if (origem.Item1 == 1 || origem.Item2 == 1 || origem.Item1 == linha || origem.Item2 == coluna)
                    {
                        saiu = true;
                    }

                    foreach (Tuple<int, int> parte in livres)
                    {
                        // Movimento para cima
                        if (movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para esquerda
                        else if (movesquerda.Item1 == parte.Item1 && movesquerda.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para direita
                        else if (movedireita.Item1 == parte.Item1 && movedireita.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        }
                        // Movimento para baixo
                        else if (movebaixo.Item1 == parte.Item1 && movebaixo.Item2 == parte.Item2)
                        {
                            livres.Remove(parte);
                            arquivo3.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
                            lab[parte.Item1, parte.Item2] = "P";
                            percorridos.Add(parte);
                            caminho.Push(parte);
                            origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                            ResetaMovimentos();
                            break;
                        };
                    };
                    // Condição para voltar espaços caso não encontre uma saída desempilhando a Pilha caminho
                    if (lab[movecima.Item1, movecima.Item2] != "0" &&
                        lab[movesquerda.Item1, movesquerda.Item2] != "0" &&
                        lab[movedireita.Item1, movedireita.Item2] != "0" &&
                        lab[movebaixo.Item1, movebaixo.Item2] != "0")
                    {
                        if (origem.Item1 != 1 && origem.Item2 != 1 && origem.Item1 != linha && origem.Item2 != coluna)
                        {
                            // Vizualização no Console para testes. Pode ser Comentado.
                            Console.WriteLine(origem);

                            livres.Remove(caminho.Peek());
                            percorridos.Remove(caminho.Peek());
                            caminho.Pop();
                            // Condições apenas para gravar a volta no arquivo.
                            if (caminho.Peek().Item1 == origem.Item1 - 1 && caminho.Peek().Item2 == origem.Item2) { arquivo3.WriteLine("C [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                            if (caminho.Peek().Item1 == origem.Item1 && caminho.Peek().Item2 - 1 == origem.Item2) { arquivo3.WriteLine("E [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                            if (caminho.Peek().Item1 == origem.Item1 && caminho.Peek().Item2 + 1 == origem.Item2) { arquivo3.WriteLine("D [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                            if (caminho.Peek().Item1 == origem.Item1 + 1 && caminho.Peek().Item2 == origem.Item2) { arquivo3.WriteLine("B [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };

                            origem = new Tuple<int, int>(caminho.Peek().Item1, caminho.Peek().Item2);
                            ResetaMovimentos();
                        }
                    };
                    // Vizualização no Console para testes. Pode ser Comentado.
                    Console.WriteLine(origem);
                }
            }
            Console.ReadLine();
            //arquivo2.Close();
        }

    }
}

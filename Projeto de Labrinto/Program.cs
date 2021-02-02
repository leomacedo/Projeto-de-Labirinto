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

            var livres = new List<Tuple<int, int>>(); // Lista de Tuplas de Caminhos Livres
            var percorridos = new List<Tuple<int, int>>(); // Lista de Tuplas de Caminhos Percorridos
            var caminho = new Stack<Tuple<int, int>>(); // Pilha de Tuplas da Posição da Origem
            
            // Leitura do Arquivo de entrada
            using (StreamReader arquivoEntrada = new StreamReader("entrada-labirinto3.txt"))
            {
                int[] dimensao = Array.ConvertAll(arquivoEntrada.ReadLine().Split(' '), int.Parse);
                int linha = dimensao[0];
                int coluna = dimensao[1];
                string linha3;
                List<string> linhas = new List<string>();
                string[,] lab = new string[linha + 2, coluna + 2];

                // Pegando os elementos do Labirinto e armazenando numa lista
                while ((linha3 = arquivoEntrada.ReadLine()) != null)
                {
                    var elements = linha3.Split(' ');
                    linhas.AddRange(elements);
                }

                // Convertendo a lista em uma matriz bidimensional
                int index = 0;
                for (int i = 1; i < linha + 1; i++)
                {
                    for (int j = 1; j < coluna + 1; j++)
                    {
                        lab[i, j] = linhas[index];
                        index++;

                        //Definindo em que parte está a origem e adicionando os caminhos livres na lista de Tuplas
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
                // Armazenando o caminho do labirinto num arquivo texto.
                using (StreamWriter arquivoSaida = new StreamWriter("saida3.txt"))
                {
                    arquivoSaida.WriteLine("O [" + origem.Item1 + ", " + origem.Item2 + "]");

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
                                arquivoSaida.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
                                lab[parte.Item1, parte.Item2] = "P";
                                percorridos.Add(parte);
                                caminho.Push(parte);
                                origem = new Tuple<int, int>(parte.Item1, parte.Item2);
                                ResetaMovimentos();
                                break;
                            }
                        };
                    };
                    

                    bool saiuDoLabirinto = false; // Variavel booleana quando sair do labirinto
                    while (saiuDoLabirinto == false)
                    {

                        if (origem.Item1 == 1 || origem.Item2 == 1 || origem.Item1 == linha || origem.Item2 == coluna)
                        {
                            saiuDoLabirinto = true;
                        }

                        foreach (Tuple<int, int> parte in livres)
                        {
                            // Movimento para cima
                            if (movecima.Item1 == parte.Item1 && movecima.Item2 == parte.Item2)
                            {
                                livres.Remove(parte);
                                arquivoSaida.WriteLine("C [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("E [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("D [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                arquivoSaida.WriteLine("B [" + parte.Item1 + ", " + parte.Item2 + "]");
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
                                livres.Remove(caminho.Peek());
                                percorridos.Remove(caminho.Peek());
                                caminho.Pop();
                                // Condições apenas para gravar a volta no arquivo.
                                if (caminho.Peek().Item1 == origem.Item1 - 1 && caminho.Peek().Item2 == origem.Item2) { arquivoSaida.WriteLine("C [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                                if (caminho.Peek().Item1 == origem.Item1 && caminho.Peek().Item2 - 1 == origem.Item2) { arquivoSaida.WriteLine("E [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                                if (caminho.Peek().Item1 == origem.Item1 && caminho.Peek().Item2 + 1 == origem.Item2) { arquivoSaida.WriteLine("D [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };
                                if (caminho.Peek().Item1 == origem.Item1 + 1 && caminho.Peek().Item2 == origem.Item2) { arquivoSaida.WriteLine("B [" + caminho.Peek().Item1 + ", " + caminho.Peek().Item2 + "]"); };

                                origem = new Tuple<int, int>(caminho.Peek().Item1, caminho.Peek().Item2);
                                ResetaMovimentos();
                            }
                        };

                    }
                    
                }
                
            }
            // Vizualização no Console para testes. Pode ser Comentado.
            // Console.WriteLine(origem);
            Console.ReadLine();
        }

    }
}

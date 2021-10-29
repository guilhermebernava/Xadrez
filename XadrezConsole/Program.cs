using System;
using tabuleiro;
using xadrez;

namespace XadrezConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

                while (!partidaDeXadrez.terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.tab);
                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    if (partidaDeXadrez.tab.existePeca(origem))
                    {
                        bool[,] posicoesPossiveis = partidaDeXadrez.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.executarMoviemtno(origem, destino);
                    }

                    

                    
                }
                
                

            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

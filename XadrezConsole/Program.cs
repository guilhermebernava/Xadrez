using System;
using System.Threading;
using System.Threading.Tasks;
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
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partidaDeXadrez);
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.ValidarPosicaoDeOrigem(origem);
                        bool[,] posicoesPossiveis = partidaDeXadrez.tab.peca(origem).movimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);
                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partidaDeXadrez.ValidarPosicaoDestino(origem,destino);
                        partidaDeXadrez.RealizaJogada(origem,destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine();
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
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

using System;
using System.Threading;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace XadrezConsole
{
    internal class Program
    {
        static  void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partidaDeXadrez = new PartidaDeXadrez();

                while (!partidaDeXadrez.terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidaDeXadrez.tab);
                    Console.WriteLine();
                    Console.WriteLine("Turno: "+ partidaDeXadrez.turno);
                    Console.WriteLine("Aguardando Jogada: " + partidaDeXadrez.jogadorAtual);
                    Console.Write("Origem: ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    if (partidaDeXadrez.jogadorAtual == partidaDeXadrez.tab.peca(origem).Cor)
                    {
                        Console.WriteLine("Cor Invalida");
                        if (partidaDeXadrez.tab.existePeca(origem))
                        {
                            bool[,] posicoesPossiveis = partidaDeXadrez.tab.peca(origem).movimentosPossiveis();
                            Console.Clear();
                            Tela.ImprimirTabuleiro(partidaDeXadrez.tab, posicoesPossiveis);
                            while (true)
                            {
                                Console.WriteLine();
                                Console.Write("Destino: ");
                                Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                                var ok = partidaDeXadrez.RealizaJogada(origem, destino);
                                if (ok)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.Write("Origem: ");
                                    origem = Tela.lerPosicaoXadrez().toPosicao();
                                }
                            }

                        }
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

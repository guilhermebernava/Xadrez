using System;
using tabuleiro;

namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("-");
                    }
                    Console.Write(tab.peca(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}

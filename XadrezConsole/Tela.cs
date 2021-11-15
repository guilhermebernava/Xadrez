using System;
using tabuleiro;
using xadrez;


namespace XadrezConsole
{
    internal class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    ImprimirPeca(tab.peca(i, j));

                }
                Console.WriteLine();

            }
            Console.WriteLine("  A B C D E F G H");

        }
        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posPossiveis)
        {
            ConsoleColor FundoOriginal = Console.BackgroundColor;
            ConsoleColor FundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                   
                        ImprimirPeca(tab.peca(i, j));
                        Console.BackgroundColor=FundoOriginal;
                     
                }
                Console.WriteLine();  
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine().ToLower();
            char coluna = s[0];
            int t = s.Length;
            int number;
            bool teste = int.TryParse(s[1] +"", out number);
            if(t > 2)
            {
                throw new TabuleiroException("Syntax Incorreta!");
            }
            if (!teste)
            {
                throw new TabuleiroException("Syntax Incorreta!");
            }
            int linha = int.Parse(s[1] + "");
            if(linha > 8)
            {
                throw new TabuleiroException("Syntax Incorreta!");
            }
            return new PosicaoXadrez(coluna, linha);
        }
    }
}

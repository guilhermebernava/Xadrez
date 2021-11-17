using System;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca() { }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            qteMovimentos = 0;
        }

        public bool ExisteMovimentoPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0;i <Tab.linhas;i++)
            {
                for(int j = 0; j < Tab.colunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public void IncrementarQteMoviemntos() { qteMovimentos++; }

        public void DecrementarQteMoviemntos() { qteMovimentos--; }

        public abstract bool[,] movimentosPossiveis();
    }
}

using System;
using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }
        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];
            bool[,] falso = { { false }, { false } };
            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(Posicao.linha -1 ,Posicao.coluna);
            if(Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                return mat;
            }
            return falso;
        }
        
        public override string ToString()
        {
            return "R";
        }
    }
}

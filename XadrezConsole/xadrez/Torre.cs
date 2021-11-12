using System;
using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];
            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(Posicao.linha - 1, Posicao.coluna);
            while(Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if(Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.linha = pos.linha - 1;
            }
            //abaixo
            pos.definirValores(Posicao.linha + 1, Posicao.coluna);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.linha = pos.linha +1;
            }
            //direita
            pos.definirValores(Posicao.linha, Posicao.coluna +1 );
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.coluna = pos.coluna + 1;
            }
            //Esquerda
            pos.definirValores(Posicao.linha, Posicao.coluna - 1);
            while (Tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).Cor != Cor)
                {
                    break;
                }
                pos.coluna = pos.coluna - 1;
            }


            return mat;
        }
    }
}

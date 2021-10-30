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
            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(Posicao.linha -1 ,Posicao.coluna);
            if(Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }
            //abaixo
            pos.definirValores(Posicao.linha + 1, Posicao.coluna);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            //esquerda
            pos.definirValores(Posicao.linha , Posicao.coluna -1);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            //diretia
            pos.definirValores(Posicao.linha, Posicao.coluna +1 );
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }
           
            //diagonal direita cima
            pos.definirValores(Posicao.linha - 1, Posicao.coluna +1);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            //diagonal esquerda  cima
            pos.definirValores(Posicao.linha - 1, Posicao.coluna - 1);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            //diagonal direita baixo
            pos.definirValores(Posicao.linha + 1, Posicao.coluna + 1);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            //diagonal esquerda  baixo
            pos.definirValores(Posicao.linha + 1, Posicao.coluna - 1);
            if (Tab.posicaoValida(pos))
            {
                mat[pos.linha, pos.coluna] = true;

            }

            return mat;
        }
        
        public override string ToString()
        {
            return "R";
        }
    }
}

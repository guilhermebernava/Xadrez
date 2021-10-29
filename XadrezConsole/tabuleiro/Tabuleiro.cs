using System;


namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }
        public Peca peca(Posicao pos)
        {
            if (pos == null)
            {
                throw new TabuleiroException("nao existe peca");
            }
            return pecas[pos.linha, pos.coluna];
        }
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void ColocarPecas(Peca p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Ja existe uma peca nessa posicao");
            }
            pecas[pos.linha, pos.coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPecas(Posicao pos)
        {
            if(peca(pos) == null)
            {
                return null;
                throw new TabuleiroException("Sem peca nesse lugar");
            }
            Peca aux = peca(pos);
            aux.Posicao = null;
            pecas[pos.linha, pos.coluna] = null;
            return aux;
        }
        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posicao Invalida");
            }
           
        }
    }
}

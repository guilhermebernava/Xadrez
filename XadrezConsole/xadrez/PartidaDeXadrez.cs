using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        public bool[,] posicoesPossiveis { get; set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            colocarPecas();
        }

        private void colocarPecas()
        {
            tab.ColocarPecas(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 8).toPosicao());
            tab.ColocarPecas(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 3).toPosicao());
            tab.ColocarPecas(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.ColocarPecas(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.ColocarPecas(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 6).toPosicao());
            tab.ColocarPecas(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 4).toPosicao());
            tab.ColocarPecas(new Rei(tab, Cor.Branca), new PosicaoXadrez('g', 2).toPosicao());
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca p = tab.peca(origem);
            p.Posicao = origem;
            ExecutarMovimentacao(origem, destino);
            turno++;
            MudaJogador();

        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Sem Peça!");
            }

            if (jogadorAtual != tab.peca(pos).Cor)
            {
                throw new TabuleiroException("Jogador Errado!");
            }

            if (!tab.peca(pos).ExisteMovimentoPossiveis())
            {
                throw new TabuleiroException("Sem Posições possíveis!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao pos)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    bool[,] possible = tab.peca(origem).movimentosPossiveis();
                    if (!possible[pos.linha, pos.coluna])
                    {
                        throw new TabuleiroException("Posição Inválida!");
                    }
                }
            }

        }


        public void MudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public void ExecutarMovimentacao(Posicao origem, Posicao destino)
        {
            Peca p = tab.peca(origem);
            p.IncrementarQteMoviemntos();
            p = tab.RetirarPecas(origem);
            Peca pecaCapturada = tab.RetirarPecas(destino);
            tab.ColocarPecas(p, destino);
        }
    }
}

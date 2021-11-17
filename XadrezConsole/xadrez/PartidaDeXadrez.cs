using System.Collections.Generic;
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
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.Cor == cor)
                {
                    aux.Add(x); 
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogos(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna,int linha,Peca peca)
        {
            tab.ColocarPecas(peca,new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            ColocarNovaPeca('c',1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));

            ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
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
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
    }
}

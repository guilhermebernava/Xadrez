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
        public bool Xeque { get; private set; }

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
            foreach (Peca x in pecas)
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

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if(R == null)
            {
                throw new TabuleiroException("Sem Rei em Jogo");
            }
            foreach(Peca x in PecasEmJogos(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.Posicao.linha, R.Posicao.coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogos(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca PecaCapturada = ExecutarMovimentacao(origem, destino);
                            bool TesteXequeMate = EstaEmXeque(cor);
                            DesFazMovimento(origem, destino, PecaCapturada);
                            if (!TesteXequeMate)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogos(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        private void colocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('h', 6, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('h', 7, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Branca));

            ColocarNovaPeca('a', 8, new Rei(tab, Cor.Preta));
            //ColocarNovaPeca('c',1, new Torre(tab, Cor.Branca));
            //ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            //ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            //ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            //ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            //ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));

            //ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            //ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            //ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            //ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            //ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            //ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
        }

        public void DesFazMovimento(Posicao origem, Posicao destino, Peca x)
        {
            Peca p = tab.RetirarPecas(destino);
            p.DecrementarQteMoviemntos();
            if(x != null)
            {
                tab.ColocarPecas(x,destino);
                capturadas.Remove(x);
            }
            tab.ColocarPecas(p,origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca p = tab.peca(origem);
            p.Posicao = origem;
            Peca x = ExecutarMovimentacao(origem, destino);

            if (EstaEmXeque(jogadorAtual))
            {
                DesFazMovimento(origem, destino, x);
                throw new TabuleiroException("Voce nao pode se colocar em xeque!");
            }
            if (EstaEmXeque(adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }
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

        public Peca ExecutarMovimentacao(Posicao origem, Posicao destino)
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
            return pecaCapturada;
        }
    }
}

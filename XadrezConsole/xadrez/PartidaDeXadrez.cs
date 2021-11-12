﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
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

        public bool ExecutarMovimentacao(Posicao origem, Posicao destino)
        {
            Posicao pos = destino;
            Peca p = tab.peca(origem);
            p.Posicao = origem;
            posicoesPossiveis = p.movimentosPossiveis();
            if (p != null)
            {
                if (posicoesPossiveis[pos.linha, pos.coluna] == true)
                {
                    p.IncrementarQteMoviemntos();
                    p = tab.RetirarPecas(origem);
                    Peca pecaCapturada = tab.RetirarPecas(destino);
                    tab.ColocarPecas(p, destino);
                    return true;
                }
                else
                {
                    Console.WriteLine("Posicao Invalida");
                    return false;   
                }

            }
            else
            {
                Console.WriteLine("Sem Peca selecionada");
                return false;
            }
            
        }
    } 
}

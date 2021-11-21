﻿using tabuleiro;
using System;

namespace xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            ushort codePoint = 0x2654;
            return $"{(char)codePoint}";
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.nLinhas,tab.nColunas];

            Posicao pos = new Posicao(0,0);

            //acima
            pos.definirValores(posicao.linha-1,posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //ne
            pos.definirValores(posicao.linha-1,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // direita
            pos.definirValores(posicao.linha,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //se
            pos.definirValores(posicao.linha+1,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //sul
            pos.definirValores(posicao.linha+1,posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            } 
            //so
            pos.definirValores(posicao.linha+1,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //esquerda
            pos.definirValores(posicao.linha,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            //no
            pos.definirValores(posicao.linha-1,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }

            return mat;
        }
    }
}
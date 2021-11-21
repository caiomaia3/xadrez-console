
using tabuleiro;
using System;

namespace xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            ushort codePoint = 0x2658;
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

            // norte - direita
            pos.definirValores(posicao.linha-2,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // norte - esquerda 
            pos.definirValores(posicao.linha-2,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // leste -  acima
            pos.definirValores(posicao.linha-1,posicao.coluna+2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // leste -  abaixo
            pos.definirValores(posicao.linha+1,posicao.coluna+2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // sul - direita
            pos.definirValores(posicao.linha+2,posicao.coluna+1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // sul - esquerda 
            pos.definirValores(posicao.linha+2,posicao.coluna-1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // oeste -  acima
            pos.definirValores(posicao.linha-1,posicao.coluna-2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // oeste -  abaixo
            pos.definirValores(posicao.linha+1,posicao.coluna-2);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            return mat;
        }
    }
}
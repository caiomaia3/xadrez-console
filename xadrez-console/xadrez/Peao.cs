using tabuleiro;
using System;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            ushort codePoint = 0x2659;
            return $"{(char)codePoint}";
        }

        private bool estaLivre(Posicao pos)
        {   

            return tab.peca(pos)==null;
        }
        private bool existeInimigo(Posicao pos) 
        {
            if(estaLivre(pos)) return false;

            if(tab.peca(pos).cor != cor)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.nLinhas,tab.nColunas];

            Posicao pos = new Posicao(0,0);
            int sentido=1;
            if(cor==Cor.Branca)
            {
                sentido = -1;
            }
            else
            {
                sentido = 1;
            }

            // Andar a frente 
            pos.definirValores(posicao.linha+sentido*1,posicao.coluna);
            if (tab.posicaoValida(pos) && estaLivre(pos))
            {
               mat[pos.linha,pos.coluna]=true;
            }
            // Andar a frente - primeiro movimento
            pos.definirValores(posicao.linha+sentido*2,posicao.coluna);
            if (tab.posicaoValida(pos) && estaLivre(pos) && qtdMovimentos == 0)
            {
               mat[pos.linha,pos.coluna]=true;
            }
            // capturar inimigo 1 
            pos.definirValores(posicao.linha+sentido*1,posicao.coluna+1);
            if (tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            // capturar inimigo 1 
            pos.definirValores(posicao.linha+sentido*1,posicao.coluna-1);
            if(tab.posicaoValida(pos) && existeInimigo(pos))
            {
                mat[pos.linha,pos.coluna] = true;
            }
            return mat;
        }
    }
}
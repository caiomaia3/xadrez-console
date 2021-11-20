using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }
        private bool podeMover(Posicao pos)
        { 
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;
            // Peca p = tab.peca(pos);
            // bool myOutput;
            // if(p == null)
            // {
            //     myOutput = true;
            // }
            // else
            // {
            //     myOutput= p.cor != cor;
            // }

            // return myOutput; 
        }

        public override string ToString()
        {
            return "T";
        }
       public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.nLinhas,tab.nColunas];

            Posicao pos = new Posicao(0,0);

            //acima
            pos.definirValores(posicao.linha-1,posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                } 
                pos.linha = pos.linha - 1;
            }

            //abaixo
            pos.definirValores(posicao.linha+1,posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.linha++;
            }

            //direita 
            pos.definirValores(posicao.linha,posicao.coluna+1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.coluna++;
            }

            //esquerda 
            pos.definirValores(posicao.linha,posicao.coluna-1);
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha,pos.coluna] = true;
                if(tab.peca(pos) != null && tab.peca(pos).cor != cor)
                {
                   break; 
                }
                pos.coluna--;
            }
            return mat;

        } 


    }
}
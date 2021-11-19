

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qtdMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab,Cor cor)
        {
            this.posicao = null;
            this.cor = cor;
            this.qtdMovimentos = 0;
            this.tab = tab;
        }
        public abstract bool[,] movimentosPossiveis();

        public bool existeMovimentosPossiveis()
        {
            bool [,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.nLinhas; i++)
            {
                for (int j = 0; j < tab.nColunas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;   
                    }
                }
            }
            return false;
        }


        public bool podeMoverPara(Posicao p)
        {
            return movimentosPossiveis()[p.linha,p.coluna];
        }
        public void incrementarQtdMovimentos()
        {
            qtdMovimentos++;
        }
    }
}

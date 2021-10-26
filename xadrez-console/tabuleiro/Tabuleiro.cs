namespace tabuleiro
{
    public class Tabuleiro
    {
        public int nLinhas { get; set; }
        public int nColunas { get; set; }
        private Peca[,] grid;

        public Tabuleiro(int nLinhas, int nColunas)
        {
            this.nLinhas = nLinhas;
            this.nColunas = nColunas;
            this.grid = new Peca[nLinhas, nColunas];
        }
    }
}
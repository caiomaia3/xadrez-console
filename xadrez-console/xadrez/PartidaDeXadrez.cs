using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; set; }
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

        public void executaMovimento(Posicao pOrigem, Posicao pDestino)
        {
            Peca p = tab.retirarPeca(pOrigem);
            p.incrementarQtdMovimentos();
            Peca pCapturada = tab.retirarPeca(pDestino);
            tab.colocarPeca(p, pDestino);
            if(pCapturada != null)
            {
                capturadas.Add(pCapturada);
            }
        }

        public void realizaJogada(Posicao pOrigem, Posicao pDestino)
        {
            executaMovimento(pOrigem,pDestino);
            turno++;
            mudarJogador();
        }   

        public void validarPosicaoOrigem(Posicao p)
        {
            if (tab.peca(p) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (tab.peca(p).cor != jogadorAtual)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(p).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimento possíveis para a peça escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao pOrigem, Posicao pDestino)
        {
            if (!tab.peca(pOrigem).podeMoverPara(pDestino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        public void mudarJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> pecaNaCor = new HashSet<Peca>();
            foreach(Peca p in capturadas)
            {
                if(p.cor == cor) pecaNaCor.Add(p);
            }
            return pecaNaCor;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca p in pecas)
            {   
                if(p.cor == cor) aux.Add(p);
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
        }
        public void colocarPecas()
        {
            colocarNovaPeca('c',1,new Torre(tab,Cor.Branca));
            colocarNovaPeca('c',2,new Torre(tab,Cor.Branca));
            colocarNovaPeca('d',2,new Torre(tab,Cor.Branca));
            colocarNovaPeca('e',1,new Torre(tab,Cor.Branca));
            colocarNovaPeca('e',2,new Torre(tab,Cor.Branca));
            colocarNovaPeca('d',1,new Rei(tab,Cor.Branca));

            colocarNovaPeca('c',8,new Torre(tab,Cor.Preta));
            colocarNovaPeca('c',7,new Torre(tab,Cor.Preta));
            colocarNovaPeca('d',7,new Torre(tab,Cor.Preta));
            colocarNovaPeca('e',8,new Torre(tab,Cor.Preta));
            colocarNovaPeca('e',7,new Torre(tab,Cor.Preta));
            colocarNovaPeca('d',8,new Rei(tab,Cor.Preta));


        }
    }
}

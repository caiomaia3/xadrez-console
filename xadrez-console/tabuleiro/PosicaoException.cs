using System;

namespace tabuleiro
{
    class PosicaoException : Exception
    {
        public PosicaoException(string msg) : base(msg)
        {
        }
    }
}

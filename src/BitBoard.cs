using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  class BitBoard
  {
    //WhitePieces
    public UInt64 WhiteKing;
    public UInt64 WhiteQueens;
    public UInt64 WhiteRooks;
    public UInt64 WhiteBishops;
    public UInt64 WhiteKnights;
    public UInt64 WhitePawns;
    //BlackPieces
    public UInt64 BlackKing;
    public UInt64 BlackQueens;
    public UInt64 BlackRooks;
    public UInt64 BlackBishops;
    public UInt64 BlackKnights;
    public UInt64 BlackPawns;
    public UInt64 BlackPieces;

    public UInt64 AllWhitePieces
    {
      get{
        return WhiteKing | WhiteQueens | WhiteRooks | WhiteBishops | WhiteKnights | WhitePawns;
      }
    }

    public UInt64 AllBlackPieces
    {
      get
      {
        return BlackKing | BlackQueens | BlackRooks | BlackBishops | BlackKnights | BlackPawns;
      }
    }

    public int FirstPiecesPosition(UInt64 p_pieces)
    {
      if (p_pieces == 0) return 0;
      UInt64 mask = 1;
      int position = 1;

      while ((p_pieces & mask) == 0)
      {
        mask <<= 1;
        position += + 1;
      }
      return position;
    }
  }
}

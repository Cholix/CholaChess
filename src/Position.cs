using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public class Position
  {
    public int colorToMove;
    
    public ulong[,] pieces = new ulong[2, 6];
    public ulong[] piecesByColor = new ulong[2];
    public ulong enPassant;

    public static Position GetStartPosition()
    {
      Position startPosition = new Position();
      startPosition.colorToMove = BitBoard.COLOR_WHITE;

      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_KING] = BitBoard.Square[4];
      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_QUEEN] = BitBoard.Square[3];
      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_ROOK] = BitBoard.Square[0] | BitBoard.Square[7];
      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_BISHOP] = BitBoard.Square[2] | BitBoard.Square[5];
      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_KNIGHT] = BitBoard.Square[1] | BitBoard.Square[6];
      startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_PAWN] = BitBoard.Rank[1];

      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_KING] = BitBoard.Square[60];
      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_QUEEN] = BitBoard.Square[59];
      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_ROOK] = BitBoard.Square[56] | BitBoard.Square[63];
      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_BISHOP] = BitBoard.Square[58] | BitBoard.Square[61];
      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_KNIGHT] = BitBoard.Square[57] | BitBoard.Square[62];
      startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_PAWN] = BitBoard.Rank[6];


      for (int color = 0; color < 2; color++)
      {
        for (int pieceType = 0; pieceType < 6; pieceType++)
        {
          startPosition.piecesByColor[color] |= startPosition.pieces[color, pieceType];
        }
      }

      return startPosition;
    }
  }
}

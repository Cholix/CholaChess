using CholaChess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace CholaChessTest
{
  [TestClass]
  public class Position_Test
  {
    [TestMethod]
    public void GetStartPosition()
    {
      Position startPosition = Position.GetStartPosition();
      string s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_KING]);
      Debug.Print("[BitBoard.WHITE, BitBoard.KING]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_QUEEN]);
      Debug.Print("[BitBoard.WHITE, BitBoard.QUEEN]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_ROOK]);
      Debug.Print("[BitBoard.WHITE, BitBoard.ROOK]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_BISHOP]);
      Debug.Print("[BitBoard.WHITE, BitBoard.BISHOP]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_KNIGHT]);
      Debug.Print("[BitBoard.WHITE, BitBoard.KNIGHT]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_WHITE, BitBoard.PIECE_TYPE_PAWN]);
      Debug.Print("[BitBoard.WHITE, BitBoard.PAWN]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_KING]);
      Debug.Print("[BitBoard.BLACK, BitBoard.KING]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_QUEEN]);
      Debug.Print("[BitBoard.BLACK, BitBoard.QUEEN]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_ROOK]);
      Debug.Print("[BitBoard.BLACK, BitBoard.ROOK]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_BISHOP]);
      Debug.Print("[BitBoard.BLACK, BitBoard.BISHOP]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_KNIGHT]);
      Debug.Print("[BitBoard.BLACK, BitBoard.KNIGHT]");
      Debug.Print(s);

      s = TestUtilities.UlongToString(startPosition.pieces[BitBoard.COLOR_BLACK, BitBoard.PIECE_TYPE_PAWN]);
      Debug.Print("[BitBoard.BLACK, BitBoard.PAWN]");
      Debug.Print(s);    
    }
  }
}

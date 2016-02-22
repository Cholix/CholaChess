using System.Collections.Generic;
namespace CholaChess
{
  public class Move
  {
    int fromSquareIndex;
    int toSquareIndex;
    int enPassantIndex;
    int promoteTo;

    public Move(int p_formSquareIndex, int p_toSquareIndex, int p_enPassantIndex, int p_promoteTo)
    {
      fromSquareIndex = p_formSquareIndex;
      toSquareIndex = p_toSquareIndex;
      enPassantIndex = p_enPassantIndex;
      promoteTo = p_promoteTo;
    }

    public override string ToString()
    {
      return
        fromSquareIndex + "-" + toSquareIndex +
        (enPassantIndex > 0 ? " EnPassant:" + enPassantIndex.ToString() : "") +
        (promoteTo > 0? " Promote to:" + promoteTo.ToString() : "");
    }
  }
}
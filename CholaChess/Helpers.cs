using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public static class Helpers
  {
    public static int GetRankIndexFromSquareIndex(int p_squareIndex)
    {
      return p_squareIndex / 8;
    }

    public static int GetFileIndexFromSquareIndex(int p_squareIndex)
    {
      return p_squareIndex % 8;
    }

    public static int GetSquareIndexFormAlgebraicNotation(string p_algebraicNotation)
    {
      return (int)(p_algebraicNotation[0] - 'a' + (p_algebraicNotation[1] - '1') * 8);
    }

    public static string GetAlgebraicNotationFromSquareIndex(int p_squareIndex)
    {
      int rankIndex = GetRankIndexFromSquareIndex(p_squareIndex);
      int fileIndex = GetFileIndexFromSquareIndex(p_squareIndex);
      return char.ConvertFromUtf32('a' + fileIndex) + char.ConvertFromUtf32('1' + rankIndex);
    }
  }
}

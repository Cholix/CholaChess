using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public static class Helpers
  {
    public static int GetSquareIndexFormAlgebraicNotation(string p_algebraicNotation)
    {
      return (int)(p_algebraicNotation[0] - 'a' + (p_algebraicNotation[1] - '1') * 8);
    }

    public static string GetAlgebraicNotationFromSquareIndex(int p_squareIndex)
    {
      int rank = p_squareIndex / 8;
      int file = p_squareIndex % 8;
      return char.ConvertFromUtf32('a' + file) + char.ConvertFromUtf32('1' + rank);
    }
  }
}

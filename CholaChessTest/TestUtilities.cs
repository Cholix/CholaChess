using System;

namespace CholaChessTest
{
  class TestUtilities
  {
    public static string UlongToString(ulong p_uint64, int p_square = -1)
    {
      Console.WriteLine();
      int square = 0;
      Console.WriteLine();
      string retval = "";
      for (int i = 0; i < 8; i++)
      {
        string row = "";
        for (int j = 0; j < 8; j++)
        {
          if (square++ == p_square)
          {
            row += "O";
          }
          else
          {
            row += ((p_uint64 & ((ulong)1) << j) == 0) ? "°" : "X";
          }
        }
        retval = row + Environment.NewLine + retval;
        p_uint64 >>= 8;
      }
      return retval;
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChessTest
{
  class TestUtilities
  {
    public static string UlongToString(ulong p_uint64)
    {
      Console.WriteLine();

      Console.WriteLine();
      string retval = "";
      for (int i = 0; i < 8; i++)
      {
        string row = "";
        for (int j = 0; j < 8; j++)
        {
          row += ((p_uint64 & ((ulong)1) << j) == 0) ? "°" : "X";
        }
        retval = row + Environment.NewLine + retval;
        p_uint64 >>= 8;
      }
      return retval;
    }
  }
}

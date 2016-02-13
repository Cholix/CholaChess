using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CholaChess;
using System.Diagnostics;

namespace CholaChessTest
{
  [TestClass]
  public class BitBoardConstantsTest
  {
    public string UlongToString(ulong p_uint64)
    {
      Console.WriteLine();

      Console.WriteLine();
      string retval = "";
      for (int i = 0; i < 8; i++)
      {
        string row = "";
        for (int j = 0; j < 8; j++)
        {
          row += ((p_uint64 & ((ulong)1) << j) == 0) ? "0" : "1";
        }
        retval = row + Environment.NewLine + retval;
        p_uint64 >>= 8;
      }
      return retval;
    }

    [TestMethod]
    public void BitBoardPosition()
    {
      for (int i = 0; i < 64; i++)
      {
        string s = UlongToString(BitBoardConstants.BitBoardPositions[i]);
      }
    }

    [TestMethod]
    public void KnightAttacks()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong[] knightAttacks = BitBoardConstants.KnightAttacks[i];
        ulong from = BitBoardConstants.BitBoardPositions[i];
        foreach (ulong ka in knightAttacks)
        {
          from |= ka;         
        }
        string s = UlongToString(from);
        Debug.Print(s);
      }
    }

    [TestMethod]
    public void Files()
    {
      Debug.Print("FileA\n" + UlongToString(BitBoardConstants.FileA));//0101010101010101
      Debug.Print("FileB\n" + UlongToString(BitBoardConstants.FileB));//0202020202020202
      Debug.Print("FileG\n" + UlongToString(BitBoardConstants.FileG));//4040404040404040
      Debug.Print("FileH\n" + UlongToString(BitBoardConstants.FileH));//8080808080808080
      
      ulong ul = ulong.Parse("4040404040404040", System.Globalization.NumberStyles.AllowHexSpecifier);
      string s = UlongToString(ul);

      Debug.Print(s);
    }
  }
}

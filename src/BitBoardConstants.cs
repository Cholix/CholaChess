using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public static class BitBoardConstants
  {
    public static ulong FileA = 72340172838076673;
    public static ulong FileB = 144680345676153346;
    public static ulong FileG = 4629771061636907072;
    public static ulong FileH = 9259542123273814144;


    public static ulong[] BitBoardPositions;

    public static ulong[][] KnightAttacks;

    static BitBoardConstants()
    {
      BitBoardPositions = new ulong[64];
      BitBoardPositions[0] = 1;
      for (int i = 1; i < 64; i++)
      {
        BitBoardPositions[i] = BitBoardPositions[0] << i;
      }

      KnightAttacks = new ulong[64][];
      for (int i = 0; i < 64; i++)
      {
        List<ulong> attackList = new List<ulong>();
        ulong p;
        p = BitBoardPositions[i] << 17 & ~FileA;
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] << 10 & ~(FileA | FileB);
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] >> 6 & ~(FileA | FileB);
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] >> 15 & ~FileA;
        if (p != 0) attackList.Add(p);

        p = BitBoardPositions[i] << 15 & ~FileH;
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] << 6 & ~(FileG | FileH);
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] >> 10 & ~(FileG | FileH);
        if (p != 0) attackList.Add(p);
        p = BitBoardPositions[i] >> 17 & ~FileH;
        if (p != 0) attackList.Add(p);
        /*
        U64 soWeWe(U64 b) {return (b >> 10) & notGHFile;}
        U64 soSoWe(U64 b) {return (b >> 17) & notHFile ;}
         */
        KnightAttacks[i] = attackList.ToArray();
      }
    }
  }
}

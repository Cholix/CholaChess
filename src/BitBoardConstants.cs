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


    public static ulong[] BitBoardPosition;

    public static ulong[][] KnightAttack;
    public static ulong[] KnightAttacks;

    static BitBoardConstants()
    {
      BitBoardPosition = new ulong[64];
      BitBoardPosition[0] = 1;
      for (int i = 1; i < 64; i++)
      {
        BitBoardPosition[i] = BitBoardPosition[0] << i;
      }

      KnightAttack = new ulong[64][];
      KnightAttacks = new ulong[64];
      for (int i = 0; i < 64; i++)
      {
        List<ulong> attackList = new List<ulong>();
        ulong p;
        p = BitBoardPosition[i] << 17 & ~FileA;
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] << 10 & ~(FileA | FileB);
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] >> 6 & ~(FileA | FileB);
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] >> 15 & ~FileA;
        if (p != 0) attackList.Add(p);

        p = BitBoardPosition[i] << 15 & ~FileH;
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] << 6 & ~(FileG | FileH);
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] >> 10 & ~(FileG | FileH);
        if (p != 0) attackList.Add(p);
        p = BitBoardPosition[i] >> 17 & ~FileH;
        if (p != 0) attackList.Add(p);

        KnightAttack[i] = attackList.ToArray();
        foreach (ulong knightAttack in KnightAttack[i])
        {
          KnightAttacks[i] |= knightAttack;
        }
      }
    }
  }
}

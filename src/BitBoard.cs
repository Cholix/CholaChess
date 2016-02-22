using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public class BitBoard
  {
    public const int COLOR_WHITE = 0;
    public const int COLOR_BLACK = 1;

    public const int PIECE_TYPE_KING = 0;
    public const int PIECE_TYPE_QUEEN = 1;
    public const int PIECE_TYPE_ROOK = 2;
    public const int PIECE_TYPE_BISHOP = 3;
    public const int PIECE_TYPE_KNIGHT = 4;
    public const int PIECE_TYPE_PAWN = 5;

    public static ulong[] Square = new ulong[64];
    public static ulong[] NegationOfSquare= new ulong[64];

    public static ulong[] File = new ulong[8];
    public static ulong[] Rank = new ulong[8];
    public static ulong[] NotFile = new ulong[8];
    public static ulong[] NotRank = new ulong[8];

    public static ulong[] KingAttack = new ulong[64];
    public static ulong[] KnightAttack = new ulong[64];
    public static ulong[,] PawnAttack = new ulong[2,64];



    static BitBoard()
    {
      #region Squares and negation of squares

      for (int i = 0; i < 64; i++)
      {
        Square[i] = 1UL << i;
        NegationOfSquare[i] = ~Square[i];
      }

      #endregion Squares and negation of squares

      #region Files, ranks, not files and not ranks

      for (int i = 0; i < 8; i++)
      {
        File[i] = 0x0101010101010101UL << i;
        Rank[i] = 0x00000000000000FFUL << (8 * i);

        NotFile[i] = ~File[i];
        NotRank[i] = ~Rank[i];
      }

      #endregion Files, ranks, not files and not ranks

      #region King attacks

      for (int i = 0; i < 64; i++)
      {
        ulong m = 1UL << i;
        ulong mask = (((m >> 1) | (m << 7) | (m >> 9)) & ~File[7]) |
                    (((m << 1) | (m << 9) | (m >> 7)) & ~File[0]) |
                    (m << 8) | (m >> 8);
        KingAttack[i] = mask;
      }

      #endregion King attacks

      #region Knight attacks

      for (int i = 0; i < 64; i++)
      {
        List<ulong> attackList = new List<ulong>();
        ulong p;
        p = Square[i] << 17 & ~File[0];
        if (p != 0) attackList.Add(p);
        p = Square[i] << 10 & ~(File[0] | File[1]);
        if (p != 0) attackList.Add(p);
        p = Square[i] >> 6 & ~(File[0] | File[1]);
        if (p != 0) attackList.Add(p);
        p = Square[i] >> 15 & ~File[0];
        if (p != 0) attackList.Add(p);

        p = Square[i] << 15 & ~File[7];
        if (p != 0) attackList.Add(p);
        p = Square[i] << 6 & ~(File[6] | File[7]);
        if (p != 0) attackList.Add(p);
        p = Square[i] >> 10 & ~(File[6] | File[7]);
        if (p != 0) attackList.Add(p);
        p = Square[i] >> 17 & ~File[7];
        if (p != 0) attackList.Add(p);

        foreach (ulong knightAttack in attackList)
        {
          KnightAttack[i] |= knightAttack;
        }
      }

      #endregion Knight attacks

      #region Pawn attacks

      for(int i = 0; i < 64; i++)
      {
        ulong m = 1UL << i;
        ulong mask = ((m << 7) & ~File[7]) | ((m << 9) & ~File[0]);
        PawnAttack[COLOR_WHITE,i] = mask;

        mask = ((m >> 9) & ~File[7]) | ((m >> 7) & ~File[0]);
        PawnAttack[COLOR_BLACK, i] = mask;
      }

      #endregion Pawn attacks
    }

    static int[] firstBitTable = {
      63, 30, 3, 32, 25, 41, 22, 33, 15, 50, 42, 13, 11, 53, 19, 34, 61, 29, 2,
      51, 21, 43, 45, 10, 18, 47, 1, 54, 9, 57, 0, 35, 62, 31, 40, 4, 49, 5, 52,
      26, 60, 6, 23, 44, 46, 27, 56, 16, 7, 39, 48, 24, 59, 14, 12, 55, 38, 28,
      58, 20, 37, 17, 36, 8
    };

    public static int GetIndexOfFirstBitAndRemoveIt(ref ulong p_number)
    {
      ulong b = p_number ^ (p_number - 1);
      uint fold = (uint)((b & 0xffffffff) ^ (b >> 32));
      p_number &= p_number - 1;
      return firstBitTable[(fold * 0x783a9b23) >> 26];
    }

    public int CountBits(ulong p_number)
    {
      int r;
      for (r = 0; p_number != 0; r++, p_number &= p_number - 1) ;
      return r;
    }
  }
}

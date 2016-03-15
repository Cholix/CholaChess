using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public class BitBoard
  {
    public const int COLOR_MASK = COLOR_BLACK;

    public const int COLOR_WHITE = 0;

    public const int COLOR_BLACK = 1;

    public const int PIECE_TYPE_PAWN = 2;

    public const int PIECE_TYPE_KNIGHT = 4;

    public const int PIECE_TYPE_BISHOP = 6;

    public const int PIECE_TYPE_ROOK = 8;

    public const int PIECE_TYPE_QUEEN = 10;

    public const int PIECE_TYPE_KING = 12;

    public static ulong[] Square = new ulong[64];

    public static ulong[] NegationOfSquare = new ulong[64];

    public static ulong[] File = new ulong[8];

    public static ulong[] Rank = new ulong[8];

    public static ulong[] NotFile = new ulong[8];

    public static ulong[] NotRank = new ulong[8];

    public static ulong[] KingAttack = new ulong[64];

    public static ulong[] KnightAttack = new ulong[64];

    public static ulong[,] PawnAttack = new ulong[2, 64];

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

      for (int i = 0; i < 64; i++)
      {
        ulong m = 1UL << i;
        ulong mask;
        mask = ((m << 7) & ~File[7]) | ((m << 9) & ~File[0]);
        PawnAttack[COLOR_WHITE, i] = mask;
        mask = ((m >> 9) & ~File[7]) | ((m >> 7) & ~File[0]);
        PawnAttack[COLOR_BLACK, i] = mask;
      }

      #endregion Pawn attacks

      #region Rook, bishop and queen occupancy mask

      for (int sq = 0; sq < 64; sq++)
      {
        ulong mask = 0;
        for (int i = sq + 8; i <= 55; i += 8)
        {
          mask |= (1ul << i);
        }
        for (int i = sq - 8; i >= 8; i -= 8)
        {
          mask |= (1ul << i);
        }
        for (int i = sq + 1; i % 8 != 7 && i % 8 != 0; i++)
        {
          mask |= (1ul << i);
        }
        for (int i = sq - 1; i % 8 != 7 && i % 8 != 0 && i >= 0; i--)
        {
          mask |= (1ul << i);
        }
        occupancyMaskRook[sq] = mask;

        mask = 0;
        for (int i = sq + 9; i % 8 != 7 && i % 8 != 0 && i <= 55; i += 9)
        {
          mask |= (1ul << i);
        }
        for (int i = sq - 9; i % 8 != 7 && i % 8 != 0 && i >= 8; i -= 9)
        {
          mask |= (1ul << i);
        }
        for (int i = sq + 7; i % 8 != 7 && i % 8 != 0 && i <= 55; i += 7)
        {
          mask |= (1ul << i);
        }
        for (int i = sq - 7; i % 8 != 7 && i % 8 != 0 && i >= 8; i -= 7)
        {
          mask |= (1ul << i);
        }
        occupancyMaskBishop[sq] = mask;
      }

      #endregion Rook, bishop and queen occupancy mask

      #region MagicMovesRook

      for (int sq = 0; sq < 64; sq++)
      {
        ulong mask = occupancyMaskRook[sq];
        int n = CountBits(mask);
        for (int index = 0; index < (1 << n); index++)
        {
          ulong occupancyVariation = Index2Ulong(index, n, mask);
          int magicMovesIndex = (int)((occupancyVariation * magicNumberRook[sq]) >> magicNumberShiftsRook[sq]);
          ulong mm = 0;
          for (int j = sq + 8; j <= 63; j += 8) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; 
          }
          for (int j = sq - 8; j >= 0; j -= 8) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; 
          }
          for (int j = sq + 1; j % 8 != 0; j++) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; 
          }
          for (int j = sq - 1; j % 8 != 7 && j >= 0; j--) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; 
          }
          magicMovesRook[sq, magicMovesIndex] = mm;
        }
      }

      #endregion MagicMovesRook

      #region MagicMovesBishop

      for (int sq = 0; sq < 64; sq++)
      {
        ulong mask = occupancyMaskBishop[sq];
        int n = CountBits(mask);
        for (int index = 0; index < (1 << n); index++)
        {
          ulong occupancyVariation = Index2Ulong(index, n, mask);
          int magicMovesIndex = (int)((occupancyVariation * magicNumberBishop[sq]) >> magicNumberShiftsBishop[sq]);
          ulong mm = 0;
          for (int j = sq + 9; j % 8 != 0 && j <= 63; j += 9) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; 
          }
          for (int j = sq - 9; j % 8 != 7 && j >= 0; j -= 9) 
          { 
            mm |= (1ul << j); 
            if ((occupancyVariation & (1ul << j)) != 0) 
              break; }
          for (int j = sq + 7; j % 8 != 7 && j <= 63; j += 7)
          {
            mm |= (1ul << j);
            if ((occupancyVariation & (1ul << j)) != 0)
              break;
          }
          for (int j = sq - 7; j % 8 != 0 && j >= 0; j -= 7)
          {
            mm |= (1ul << j);
            if ((occupancyVariation & (1ul << j)) != 0)
              break;
          }
          
          magicMovesBishop[sq, magicMovesIndex] = mm;
        }
      }

      #endregion MagicMovesBishop
    }

    static ulong Index2Ulong(int p_index, int p_bits, ulong p_mask)
    {
      int i, j;
      ulong result = 0;
      for (i = 0; i < p_bits; i++)
      {
        j = GetIndexOfFirstBitAndRemoveIt(ref p_mask);
        if ((p_index & (1 << i)) != 0) result |= (1UL << j);
      }
      return result;
    }

    #region GetRookAttacks

    static ulong[] occupancyMaskRook = new ulong[64];

    static ulong[] magicNumberRook = 
    {
      36029347315843088,    10124110379159126656, 72092778545348672,    72066411607887872, 
      432363165009847320,   216192590503542792,   36030996058996864,    144115755013701706, 
      9147937817895040,     70369282097216,       1594556018228994050,  2392571662828160, 
      583356906437871618,   562958543622152,      1747678207715590400,  9800395741326229762, 
      36029071898968132,    1157425380186923008,  4846014486563266562,  4616190717868965920, 
      4611829504829031425,  1153625741871546881,  18018796691292418,    6917830293894234209, 
      2305913380105388064,  4512710326886400,     36064264859976197,    1134698148398208, 
      8798240768130,        4756364474285295660,  865554812019155536,   144124267638882444, 
      141021056868608,      72092780565905409,    9007272285971520,     422246866751489, 
      1156303604529760256,  565151132550144,      9333763133671440,     17871392475268, 
      288266111353389064,   9227876873437069312,  3458799699283214352,  1166432342145826832, 
      435169110154838144,   5066618367508488,     901282909804167172,   576461031753252884, 
      1765437597917675776,  4629718009659801920,  13835128497042882816, 9147971108078208, 
      36312488333021440,    289356826384794752,   4936226675164971264,  277310874112, 
      1152940196854661121,  722863208041349146,   1153211921714032706,  162138400411881473, 
      844433789026309,      145522571683823617,   2305915612147777796,  1152921711906717826
     };

    static int[] magicNumberShiftsRook = 
    {
      52,53,53,53,53,53,53,52,
      53,54,54,54,54,54,54,53,
      53,54,54,54,54,54,54,53,
      53,54,54,54,54,54,54,53,
      53,54,54,54,54,54,54,53,
      53,54,54,54,54,54,54,53,
      53,54,54,54,54,54,54,53,
      52,53,53,53,53,53,53,52
    };

    static ulong[,] magicMovesRook = new ulong[64, 4096];    

    public static ulong GetRookAttacks(int p_fromSquare, ulong p_bbAllPieces)
    {
      int magicMovesIndex = (int)(((p_bbAllPieces & occupancyMaskRook[p_fromSquare]) * magicNumberRook[p_fromSquare]) >> magicNumberShiftsRook[p_fromSquare]);
      return magicMovesRook[p_fromSquare, magicMovesIndex];
    }

    #endregion GetRookAttacks

    #region GetBishopAttacks

    static ulong[] occupancyMaskBishop = new ulong[64];

    static ulong[] magicNumberBishop = 
    {
      1197961904598025232,  1147892623544385,     1157461396710096896,  38326776858151944, 
      5635014273269814,     5764750597521948928,  577587923555254912,   9228440828425536000, 
      9414585614602,        35768504483881,       5188432662017212428,  74600781763858946, 
      90718591821287940,    4611827314800492544,  146253067322440,      37440853518655496, 
      9296555668372858880,  9016065174669376,     73183528343048704,    1126005404205056, 
      5067150977990658,     35197267480608,       9512742057916108800,  5350421536346230784, 
      5634173550203904,     571746314967048,      9227954835695993858,  2306124763430404608, 
      142936578786304,      2341966368531877888,  18577623349465184,    110410760265441796, 
      289391494796714624,   9264062780394185888,  585511936321913346,   597292101751275650, 
      666532882828886144,   9009986696938048,     649083512516789304,   9152421901403656, 
      9226204450010437632,  2533861120565248,     2818065582539008,     5332825192236324876, 
      578748020158382208,   9224718391074374144,  586253037270991361,   76654721972179720, 
      565157621137700,      18300273714266144,    576611531644469506,   79165944627201, 
      144397489917722752,   2379035436828884992,  162710489520947202,   9232524375975010312, 
      4630406337904509952,  2355401301241758216,  5908723816057811232,  1297637026044184096, 
      2314956586353697794,  422050859520,         9223407362978679040,  594479586004451584
    };

    static int[] magicNumberShiftsBishop = 
    {
      58,59,59,59,59,59,59,58,
      59,59,59,59,59,59,59,59,
      59,59,57,57,57,57,59,59,
      59,59,57,55,55,57,59,59,
      59,59,57,55,55,57,59,59,
      59,59,57,57,57,57,59,59,
      59,59,59,59,59,59,59,59,
      58,59,59,59,59,59,59,58
    };

    static ulong[,] magicMovesBishop = new ulong[64, 4096];

    public static ulong GetBishopAttacks(int p_fromSquare, ulong p_bbAllPieces)
    {
      int magicMovesIndex = (int)(((p_bbAllPieces & occupancyMaskBishop[p_fromSquare]) * magicNumberBishop[p_fromSquare]) >> magicNumberShiftsBishop[p_fromSquare]);
      return magicMovesBishop[p_fromSquare, magicMovesIndex];
    }

    #endregion GetBishopAttacks

   
    #region GetIndexOfFirstBit

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

    public static int GetIndexOfFirstBit(ulong p_number)
    {
      ulong b = p_number ^ (p_number - 1);
      uint fold = (uint)((b & 0xffffffff) ^ (b >> 32));
      return firstBitTable[(fold * 0x783a9b23) >> 26];
    }

    #endregion GetIndexOfFirstBit

    #region CountBits

    public static int CountBits(ulong p_number)
    {
      int r;
      for (r = 0; p_number != 0; r++, p_number &= p_number - 1) ;
      return r;
    }

    #endregion CountBits
  }
}

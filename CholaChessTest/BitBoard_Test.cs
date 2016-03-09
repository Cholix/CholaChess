using CholaChess;
using System.Diagnostics;
using Xunit;

namespace CholaChessTest
{
  public class BitBoard_Test
  {
    [Fact]
    public void BitBoardPosition()
    {
      for (int i = 0; i < 64; i++)
      {
        Debug.Print(i.ToString());
        string s = TestUtilities.UlongToString(BitBoard.Square[i]);
        Debug.Print(s);
      }
    }

    [Fact]
    public void FilesAndRanks()
    {
      for (int i = 0; i < 8; i++)
      {
        string s = TestUtilities.UlongToString(BitBoard.File[i]);
        Debug.Print("File " + ((char)('A' + i)));
        Debug.Print(s);
        s = TestUtilities.UlongToString(BitBoard.Rank[i]);
        Debug.Print("Rank " + (1 + i).ToString());
        Debug.Print(s);
      }
    }

    [Fact]
    public void KingAttacks()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong attacks = BitBoard.KingAttack[i];
        Debug.Print("King Attacks from: " + i.ToString());
        string s = TestUtilities.UlongToString(attacks, i);
        Debug.Print(s);
      }
    }

    [Fact]
    public void KnightAttacks()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong attacks = BitBoard.KnightAttack[i];
        Debug.Print("Knight Attacks from: " + i.ToString());
        string s = TestUtilities.UlongToString(attacks, i);
        Debug.Print(s);
      }
    }

    [Fact]
    public void PownAttacks()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong attacks = BitBoard.PawnAttack[BitBoard.COLOR_WHITE, i];
        Debug.Print("White Pawn Attacks from: " + i.ToString());
        string s = TestUtilities.UlongToString(attacks, i);
        Debug.Print(s);
      }
      for (int i = 0; i < 64; i++)
      {
        ulong attacks = BitBoard.PawnAttack[BitBoard.COLOR_BLACK,i];
        Debug.Print("Black Pawn Attacks from: " + i.ToString());
        string s = TestUtilities.UlongToString(attacks, i);
        Debug.Print(s);
      }
    }

    [Fact]
    public void OccupancyRockMask()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong om = BitBoard.OccupancyMaskRock[i];
        Debug.Print("OccupancyRockMask[" + i.ToString() + "]");
        string s = TestUtilities.UlongToString(om, i);
        Debug.Print(s);
      }
    }

    [Fact]
    public void OccupancyBishopMask()
    {
      for (int i = 0; i < 64; i++)
      {
        ulong om = BitBoard.OccupancyMaskBishop[i];
        Debug.Print("OccupancyBishopMask[" + i.ToString() + "]");
        string s = TestUtilities.UlongToString(om, i);
        Debug.Print(s);
      }
    }
  }
}

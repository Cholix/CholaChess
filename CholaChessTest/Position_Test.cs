using CholaChess;
using System.Diagnostics;
using Xunit;

namespace CholaChessTest
{
  public class Position_Test
  {
    [Theory]
    [InlineData("starting position", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
    [InlineData("1. e4", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1")]
    [InlineData("1. ... c5", "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2")]
    [InlineData("2. Nf3", "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2")]
    public void PositionFromFen(string p_test, string p_fen)
    {
      Position positionFromFen = new Position(p_fen);
      Debug.Print(p_test + " from FEN " + p_fen + "\n");
      Debug.Print(positionFromFen.ToString());
    }

    [Theory]
    [InlineData("No knight attack", "8/8/8/4k3/8/1K2N3/8/8 w - - 0 1", false)]
    [InlineData("Knight attack", "8/8/8/4k3/8/1K1N4/8/8 b - - 0 1", true)]
    [InlineData("No pawn attack", "8/8/8/3Pk3/8/1K2N3/8/8 b - - 0 1", false)]
    [InlineData("Pawn attack", "8/8/8/4k3/3P4/1K2N3/8/8 b - - 0 1", true)]
    [InlineData("No king attack", "8/8/8/4k3/8/4K3/8/8 b - - 0 1", false)]
    [InlineData("King attack", "8/8/8/4k3/3K4/8/8/8 b - - 0 1", true)]
    public void IsColorInCheck(string p_test, string p_fen, bool p_result)
    {
      Position position = new Position(p_fen);
      Debug.Print(p_test + " from FEN " + p_fen + "\n");
      Debug.Print(position.ToString());
      Assert.Equal(position.IsColorInCheck(BitBoard.COLOR_BLACK), p_result);
    }

    [Theory]
    [InlineData("starting position", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 20)]
    [InlineData("1. e4", "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", 20)]
    [InlineData("1. ... c5", "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2", 30)]
    [InlineData("2. Nf3", "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2", 0)]
    public void GetLegalMoves(string p_test, string p_fen, int p_expectedCountOfMove)
    {
      Position positionFromFen = new Position(p_fen);
      Debug.Print(p_test + " from FEN " + p_fen + "\n");
      Debug.Print(positionFromFen.ToString());
      MoveList moveList = positionFromFen.GetLegalMoves();
      Debug.Print("MoveList:\n");
      Debug.Print(moveList.ToString());
      Assert.Equal(p_expectedCountOfMove, moveList.CountMoves);
    }
  }
}

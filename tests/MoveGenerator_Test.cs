using CholaChess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChessTest
{
  [TestClass]
  public class MoveGenerator_Test
  {
    [TestMethod]
    public void PseudoLegalMoves_StartPosition()
     {
       Position startPosition = Position.GetStartPosition();
       MoveList moveList = MoveGenerator.PseudoLegalMoves(startPosition);
     }

  }
}

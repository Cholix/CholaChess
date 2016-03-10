using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public class MoveList
  {
    //TODO privremena implementacija
    List<Move> moves = new List<Move>();

    public void AddMove(int p_formSquare, int p_toSquare)
    {
      moves.Add(new Move(p_formSquare, p_toSquare, 0, 0));
    }

    public void AddMovePromotion(int p_formSquare, int p_toSquare, int p_promoteTo)
    {
      moves.Add(new Move(p_formSquare, p_toSquare, 0, p_promoteTo));
    }

    public void AddMoveEnPassant(int p_formSquare, int p_toSquare, int p_enPassant)
    {
      moves.Add(new Move(p_formSquare, p_toSquare, p_enPassant, 0));
    }

    public int CountMoves
    {
      get
      { 
        return moves.Count;
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < moves.Count; i++)
      {
        sb.Append(i + 1).Append(". ").AppendLine(moves[i].ToString());
      }
      return sb.ToString();
    }
  }
}

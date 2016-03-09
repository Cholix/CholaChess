using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CholaChess
{
  public class Position
  {
    public int ColorToMove;
    
    public ulong[] Pieces = new ulong[16];

    public ulong EnPassant;
    public bool[] CastleKingside = new bool[2];
    public bool[] CastleQueenside = new bool[2];

    public int FiftyMovesClock;
    public int HalfMoves;

    /// <summary>
    /// The FEN string of the starting chess position. 
    /// </summary>
    public const string FEN_STARTPOS = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    public Position(string p_fen)
    {
      int rank = 7;
      int file = 0;
      string[] fenParts = p_fen.Split(' ');
      for(int i = 0; i < fenParts[0].Length; i++)
      {
        char c = fenParts[0][i];
        char cUpper = char.ToUpper(c);
        int color = BitBoard.COLOR_BLACK;

        if (cUpper == c)
        {
          color = BitBoard.COLOR_WHITE;
        }
        switch (cUpper)
        { 
          case '/':
            file = 0;
            rank--;
            break;
          case 'K':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_KING] = BitBoard.Square[rank * 8 + file++];
            break;
          case 'Q':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_QUEEN] |= BitBoard.Square[rank * 8 + file++];
            break;
          case 'R':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_ROOK] |= BitBoard.Square[rank * 8 + file++];
            break;
          case 'B':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_BISHOP] |= BitBoard.Square[rank * 8 + file++];
            break;
          case 'N':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_KNIGHT] |= BitBoard.Square[rank * 8 + file++];
            break;
          case 'P':
            Pieces[color] |= BitBoard.Square[rank * 8 + file];
            Pieces[color | BitBoard.PIECE_TYPE_PAWN] |= BitBoard.Square[rank * 8 + file++];
            break;
          case '8':
          case '7':
          case '6':
          case '5':
          case '4':
          case '3':
          case '2':
          case '1':
            file += cUpper - '0';
            break;
          default:
            throw new Exception();
        }
      }

      ColorToMove = fenParts[1] == "w" ? BitBoard.COLOR_WHITE : BitBoard.COLOR_BLACK;

      if (fenParts.Length > 2)
      {
        if (fenParts[2].Contains("Q"))
          CastleQueenside[BitBoard.COLOR_WHITE] = true;
        if (fenParts[2].Contains("K"))
          CastleKingside[BitBoard.COLOR_WHITE] = true;
        if (fenParts[2].Contains("q"))
          CastleQueenside[BitBoard.COLOR_BLACK] = true;
        if (fenParts[2].Contains("k"))
          CastleKingside[BitBoard.COLOR_BLACK] = true;
      }

      if (fenParts.Length > 3)
      {
        if (fenParts[3] != "-")
        {
          EnPassant = BitBoard.Square[Helpers.GetSquareIndexFormAlgebraicNotation(fenParts[3])];
        }
      }

      if (fenParts.Length > 5)
      {
        FiftyMovesClock = Int32.Parse(fenParts[4]);
        Int32 moveNumber = Int32.Parse(fenParts[5]);
        HalfMoves = 2 * (moveNumber - 1) + ColorToMove;
      }
    }

    public MoveList GetLegalMoves()
    {
      MoveList moveList = new MoveList();

      int fromSquareIndex;
      int toSquareIndex;
      ulong toSquares;

      ulong myPieces = Pieces[ColorToMove];
      ulong enemyPieces = Pieces[1 - ColorToMove];
      ulong notMyPieces = ~myPieces;
      ulong allPieces = myPieces | enemyPieces;
      ulong emptySquare = ~allPieces;
      int myKingSquareIndex = BitBoard.GetIndexOfFirstBit(Pieces[ColorToMove | BitBoard.PIECE_TYPE_KING]);
      
      #region GenerateKingMoves

      fromSquareIndex = myKingSquareIndex;
      toSquares = BitBoard.KingAttack[myKingSquareIndex] & notMyPieces;
      while (toSquares != 0)
      {
        toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
        moveList.AddMove(fromSquareIndex, toSquareIndex);
      }

      #endregion GenerateKingMoves
      //GenerateCastleMoves();
      //GenerateQueenMoves();
      //GenerateRookMoves();
      //GenerateBishopMoves();
      #region GenerateKnightMoves

      ulong knights = Pieces[ColorToMove | BitBoard.PIECE_TYPE_KNIGHT];

      while (knights != 0)
      {
        fromSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref knights);
        toSquares = BitBoard.KnightAttack[fromSquareIndex] & notMyPieces;
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(fromSquareIndex, toSquareIndex);
        }
      }

      #endregion GenerateKnightMoves

      #region GeneratePawnMoves

      ulong pawns = Pieces[ColorToMove | BitBoard.PIECE_TYPE_PAWN];

      if (ColorToMove == BitBoard.COLOR_WHITE)
      {
        #region White pawn

        toSquares = (pawns << 8) & emptySquare;
        ulong toSquares2 = ((toSquares & BitBoard.Rank[2]) << 8) & emptySquare;
        ulong toSquareForPromotion = toSquares & BitBoard.Rank[7];
        toSquares &= BitBoard.NotRank[7];
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex - 8, toSquareIndex);
        }
        while (toSquares2 != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares2);
          moveList.AddMoveEnPassant(toSquareIndex - 16, toSquareIndex, toSquareIndex - 8);
        }
        while (toSquareForPromotion != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquareForPromotion);
          moveList.AddMovePromotion(toSquareIndex - 8, toSquareIndex, BitBoard.PIECE_TYPE_QUEEN);
          moveList.AddMovePromotion(toSquareIndex - 8, toSquareIndex, BitBoard.PIECE_TYPE_ROOK);
          moveList.AddMovePromotion(toSquareIndex - 8, toSquareIndex, BitBoard.PIECE_TYPE_BISHOP);
          moveList.AddMovePromotion(toSquareIndex - 8, toSquareIndex, BitBoard.PIECE_TYPE_KNIGHT);
        }
        //attacks
        toSquares = (pawns << 7) & BitBoard.NotFile[7] & (enemyPieces | EnPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex - 7, toSquareIndex);
        }
        toSquares = (pawns << 9) & BitBoard.NotFile[0] & (enemyPieces | EnPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex - 9, toSquareIndex);
        }

        #endregion White pawn
      }
      else
      {
        #region Black pawn

        toSquares = (pawns >> 8) & emptySquare;
        ulong toSquares2 = ((toSquares & BitBoard.Rank[5]) >> 8) & emptySquare;
        ulong toSquareForPromotion = toSquares & BitBoard.Rank[0];
        toSquares &= BitBoard.NotRank[0];
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex + 8, toSquareIndex);
        }
        while (toSquares2 != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares2);
          moveList.AddMoveEnPassant(toSquareIndex + 16, toSquareIndex, toSquareIndex + 8);
        }
        while (toSquareForPromotion != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquareForPromotion);
          moveList.AddMovePromotion(toSquareIndex + 8, toSquareIndex, BitBoard.PIECE_TYPE_QUEEN);
          moveList.AddMovePromotion(toSquareIndex + 8, toSquareIndex, BitBoard.PIECE_TYPE_ROOK);
          moveList.AddMovePromotion(toSquareIndex + 8, toSquareIndex, BitBoard.PIECE_TYPE_BISHOP);
          moveList.AddMovePromotion(toSquareIndex + 8, toSquareIndex, BitBoard.PIECE_TYPE_KNIGHT);
        }
        //attacks
        toSquares = (pawns >> 7) & BitBoard.NotFile[0] & (enemyPieces | EnPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex + 7, toSquareIndex);
        }
        toSquares = (pawns >> 9) & BitBoard.NotFile[7] & (enemyPieces | EnPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex + 9, toSquareIndex);
        }

        #endregion Black pawn
      }

      #endregion GeneratePawnMoves

      return moveList;
    }

    //da li je strana koja nije na potezu u šahu
    public bool IsColorInCheck(int p_color)
    {
      int opposingColor = 1 - p_color;
      ulong colorKing = Pieces[p_color | BitBoard.PIECE_TYPE_KING];
      int colorKingIndex = BitBoard.GetIndexOfFirstBit(colorKing);
      //Capture by knight
      if ((Pieces[opposingColor | BitBoard.PIECE_TYPE_KNIGHT] & BitBoard.KnightAttack[colorKingIndex]) != 0)
      {
        return true;
      }
      if ((Pieces[opposingColor | BitBoard.PIECE_TYPE_PAWN] & BitBoard.PawnAttack[p_color, colorKingIndex]) != 0)
      {
        return true;
      }
      if ((Pieces[opposingColor | BitBoard.PIECE_TYPE_KING] & BitBoard.KingAttack[colorKingIndex]) != 0)
      {
        return true;
      }

      return false;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      for (int rank = 7; rank >= 0; rank--)
      {
        sb.AppendLine(" -- -- -- -- -- -- -- --");
        sb.Append("|");
        for (int file = 0; file < 8; file++)
        {
          ulong square = BitBoard.Square[rank * 8 + file];
          if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_KING] & square) != 0)
          { 
            sb.Append("WK");
          }
          else if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_QUEEN] & square) != 0)
          {
            sb.Append("WQ");
          }
          else if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_ROOK] & square) != 0)
          {
            sb.Append("WR");
          }
          else if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_BISHOP] & square) != 0)
          {
            sb.Append("WB");
          }
          else if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_KNIGHT] & square) != 0)
          {
            sb.Append("WN");
          }
          else if ((Pieces[BitBoard.COLOR_WHITE | BitBoard.PIECE_TYPE_PAWN] & square) != 0)
          {
            sb.Append("WP");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_KING] & square) != 0)
          {
            sb.Append("bk");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_QUEEN] & square) != 0)
          {
            sb.Append("bq");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_ROOK] & square) != 0)
          {
            sb.Append("br");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_BISHOP] & square) != 0)
          {
            sb.Append("bb");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_KNIGHT] & square) != 0)
          {
            sb.Append("bn");
          }
          else if ((Pieces[BitBoard.COLOR_BLACK | BitBoard.PIECE_TYPE_PAWN] & square) != 0)
          {
            sb.Append("bp");
          }
          else
          {
            if ((file + rank % 2)%2 == 0)
            {
              sb.Append("°°");
            }
            else
            {
              sb.Append("  ");
            }
          }
          sb.Append("|");
        }
        sb.AppendLine();
      }
      sb.AppendLine(" -- -- -- -- -- -- -- --");
      return sb.ToString();
    }
  }
}

using System;

namespace CholaChess
{
  public class MoveGenerator
  {
    public static MoveList PseudoLegalMoves(Position p_position)
    {
      int fromSquareIndex;
      int toSquareIndex;
      ulong toSquares;
      MoveList moveList = new MoveList();
      ulong myPieces = p_position.piecesByColor[p_position.colorToMove];
      ulong enemyPieces = p_position.piecesByColor[1 - p_position.colorToMove];
      ulong notMyPieces = ~myPieces;
      ulong allPieces = myPieces | enemyPieces;
      ulong emptySquare = ~allPieces;
      
      //GenerateKingMoves();
      //GenerateCastleMoves();
      //GenerateQueenMoves();
      //GenerateRookMoves();
      //GenerateBishopMoves();
      #region GenerateKnightMoves

      ulong knights = p_position.pieces[p_position.colorToMove, BitBoard.PIECE_TYPE_KNIGHT];

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

      ulong pawns = p_position.pieces[p_position.colorToMove, BitBoard.PIECE_TYPE_PAWN];

      if (p_position.colorToMove == BitBoard.COLOR_WHITE)
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
        toSquares = (pawns << 7) & BitBoard.NotFile[7] & (enemyPieces | p_position.enPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex - 7, toSquareIndex);
        }
        toSquares = (pawns << 9) & BitBoard.NotFile[0] & (enemyPieces | p_position.enPassant);
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
        toSquares = (pawns >> 7) & BitBoard.NotFile[0] & (enemyPieces | p_position.enPassant);
        while (toSquares != 0)
        {
          toSquareIndex = BitBoard.GetIndexOfFirstBitAndRemoveIt(ref toSquares);
          moveList.AddMove(toSquareIndex + 7, toSquareIndex);
        }
        toSquares = (pawns >> 9) & BitBoard.NotFile[7] & (enemyPieces | p_position.enPassant);
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

   

  }
}
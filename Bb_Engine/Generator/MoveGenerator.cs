using System.Collections.Generic;
using System.Numerics;

// Test push
namespace Bb_Engine.Generator
{
    internal static class MoveGenerator
    {
        internal static ulong position { get; set; } = 0UL;

        internal static List<MoveObject> GenerateMoves(List<ulong> boards, GameState gameState, bool legalMoves)
        {
            List<MoveObject> allMoves = new();

            position = 0UL;
            foreach (ulong board in boards)
            {
                position |= board;
            }

            int turn = gameState.Turn;
            if (turn == 0)
            {
                // Generate moves for White pieces
                allMoves.AddRange(Kings.GenerateWhiteKingMoves(boards, position));
                allMoves.AddRange(Pawns.GetWhitePawns(boards, gameState));
                // Add move generation for other White pieces
            }
            else
            {
                // Generate moves for Black pieces
                allMoves.AddRange(Kings.GenerateBlackKingMoves(boards, position));
                allMoves.AddRange(Pawns.GetBlackPawns(boards, gameState));
                // Add move generation for other Black pieces
            }

            if (legalMoves)
            {
                allMoves = allMoves.FindAll(move => IsLegal(move, boards, gameState));
            }

            return allMoves;
        }

        internal static bool IsLegal(MoveObject move, List<ulong> boards, GameState gameState)
        {
            MoveHandlers.MakeMove(boards, move, gameState);
            bool isKingInCheck = IsKingInCheck(boards, gameState);
            MoveHandlers.UndoMove(boards, gameState);
            return !isKingInCheck;
        }

        private static bool IsKingInCheck(List<ulong> boards, GameState gameState)
        {
            int turn = gameState.Turn;
            ulong kingBoard = turn == 0 ? boards[5] : boards[11];
            int kingSquare = BitOperations.TrailingZeroCount(kingBoard);

            ulong enemyAttacks = CalculateAllEnemyAttacks(boards, turn);

            return (enemyAttacks & (1UL << kingSquare)) != 0;
        }

        private static ulong CalculateAllEnemyAttacks(List<ulong> boards, int turn)
        {
            ulong enemyAttacks = 0UL;

            if (turn == 0) // White's turn, so check black's pieces
            {
                // Implement enemy attack generation for Black pieces
                enemyAttacks |= Kings.GetKingAttacks(boards[11]);
                // Add attack generation for other Black pieces
            }
            else // Black's turn, so check white's pieces
            {
                // Implement enemy attack generation for White pieces
                enemyAttacks |= Kings.GetKingAttacks(boards[5]);
                // Add attack generation for other White pieces
            }

            return enemyAttacks;
        }
    }
}

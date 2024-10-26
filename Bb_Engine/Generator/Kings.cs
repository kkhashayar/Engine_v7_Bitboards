using System.Collections.Generic;
using System.Numerics;

namespace Bb_Engine.Generator
{
    internal static class Kings
    {
        // Precomputed king attack patterns for each square
        private static readonly ulong[] KingAttacks = new ulong[64];

        static Kings()
        {
            InitializeKingAttacks();
        }

        private static void InitializeKingAttacks()
        {
            for (int square = 0; square < 64; square++)
            {
                ulong bitboard = 1UL << square;
                ulong attacks = 0UL;

                int rank = square / 8;
                int file = square % 8;

                // Iterate over adjacent squares
                for (int deltaRank = -1; deltaRank <= 1; deltaRank++)
                {
                    for (int deltaFile = -1; deltaFile <= 1; deltaFile++)
                    {
                        if (deltaRank == 0 && deltaFile == 0)
                            continue;

                        int targetRank = rank + deltaRank;
                        int targetFile = file + deltaFile;

                        if (targetRank >= 0 && targetRank <= 7 && targetFile >= 0 && targetFile <= 7)
                        {
                            int targetSquare = targetRank * 8 + targetFile;
                            attacks |= 1UL << targetSquare;
                        }
                    }
                }

                KingAttacks[square] = attacks;
            }
        }

        public static List<MoveObject> GenerateWhiteKingMoves(List<ulong> boards, ulong position)
        {
            List<MoveObject> allMoves = new();
            ulong whiteKing = boards[5];

            if (whiteKing == 0) return allMoves;

            int fromSquare = BitOperations.TrailingZeroCount(whiteKing);

            ulong ownPieces = boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5];
            ulong enemyPieces = boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11];

            ulong attacks = KingAttacks[fromSquare] & ~ownPieces;

            while (attacks != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(attacks);
                ulong toSquareMask = 1UL << toSquare;
                bool isCapture = (enemyPieces & toSquareMask) != 0;

                MoveObject newMove = new MoveObject
                {
                    startPosition = fromSquare,
                    EndSquare = toSquare,
                    IsCapture = isCapture,
                    PieceType = PieceType.King,
                    MoveType = isCapture ? MoveType.Capture : MoveType.Quiet
                };

                allMoves.Add(newMove);

                attacks &= attacks - 1;
            }

            // Add castling moves if applicable
            // Implement castling rights checks based on GameState

            return allMoves;
        }

        public static List<MoveObject> GenerateBlackKingMoves(List<ulong> boards, ulong position)
        {
            List<MoveObject> allMoves = new();
            ulong blackKing = boards[11];

            if (blackKing == 0) return allMoves;

            int fromSquare = BitOperations.TrailingZeroCount(blackKing);

            ulong ownPieces = boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11];
            ulong enemyPieces = boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5];

            ulong attacks = KingAttacks[fromSquare] & ~ownPieces;

            while (attacks != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(attacks);
                ulong toSquareMask = 1UL << toSquare;
                bool isCapture = (enemyPieces & toSquareMask) != 0;

                MoveObject newMove = new MoveObject
                {
                    startPosition = fromSquare,
                    EndSquare = toSquare,
                    IsCapture = isCapture,
                    PieceType = PieceType.King,
                    MoveType = isCapture ? MoveType.Capture : MoveType.Quiet
                };

                allMoves.Add(newMove);

                attacks &= attacks - 1;
            }

            // Add castling moves if applicable
            // Implement castling rights checks based on GameState

            return allMoves;
        }

        public static ulong GetKingAttacks(ulong kingBoard)
        {
            if (kingBoard == 0) return 0UL;

            int kingSquare = BitOperations.TrailingZeroCount(kingBoard);
            return KingAttacks[kingSquare];
        }
    }
}

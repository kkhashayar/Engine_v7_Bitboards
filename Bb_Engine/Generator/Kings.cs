using System.Numerics;
using static Bb_Engine.Boards;

namespace Bb_Engine.Generator
{
    internal static class Kings
    {
        private static readonly int[] kingOffsets = { 1, -1, 8, -8, 9, -9, 7, -7 }; // Possible king moves

        private const ulong notAFile = 0xfefefefefefefefeUL; // Mask to block A-file wrapping
        private const ulong notHFile = 0x7f7f7f7f7f7f7f7fUL; // Mask to block H-file wrapping

        public static List<MoveObject> GetWhiteKingMoves(List<ulong> boards, ulong position)
        {
            List<MoveObject> allMoves = new();
            ulong whiteKingBoard = boards[5];  // White King
            ulong blackPieces = boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11]; // All black pieces

            while (whiteKingBoard != 0)
            {
                int fromSquare = BitOperations.TrailingZeroCount(whiteKingBoard);
                ulong fromSquareMask = 1UL << fromSquare;

                BoardSquares fromSquareEnum = (BoardSquares)fromSquare;

                foreach (int offset in kingOffsets)
                {
                    int toSquare = fromSquare + offset;
                    if (toSquare >= 0 && toSquare < 64)
                    {
                        ulong toSquareMask = 1UL << toSquare;

                        BoardSquares toSquareEnum = (BoardSquares)toSquare;

                        // Prevent wrapping on A and H files
                        if ((offset == -1 || offset == -9 || offset == 7) && (fromSquareMask & notAFile) == 0) continue;
                        if ((offset == 1 || offset == 9 || offset == -7) && (fromSquareMask & notHFile) == 0) continue;

                        // If destination is empty or has black pieces
                        if ((toSquareMask & ~position) != 0 || (toSquareMask & blackPieces) != 0)
                        {
                            var startPosition = (int)fromSquareEnum;
                            var endSquare = (int)toSquareEnum;
                            MoveObject newMove = new MoveObject
                            {
                                startPosition = startPosition,
                                EndSquare = endSquare,
                            };
                            allMoves.Add(newMove);
                        }
                    }
                }

                whiteKingBoard &= whiteKingBoard - 1; // Remove the last set bit
            }

            return allMoves;
        }
        public static List<MoveObject> GetBlackKingMoves(List<ulong> boards, ulong position)
        {
            List<MoveObject> allMoves = new();
            ulong blackKingBoard = boards[11];  // Black King
            ulong whitePieces = boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5]; // All white pieces

            while (blackKingBoard != 0)
            {
                int fromSquare = BitOperations.TrailingZeroCount(blackKingBoard);
                ulong fromSquareMask = 1UL << fromSquare;

                BoardSquares fromSquareEnum = (BoardSquares)fromSquare;

                foreach (int offset in kingOffsets)
                {
                    int toSquare = fromSquare + offset;
                    if (toSquare >= 0 && toSquare < 64)
                    {
                        ulong toSquareMask = 1UL << toSquare;

                        BoardSquares toSquareEnum = (BoardSquares)toSquare;

                        // Prevent wrapping on A and H files
                        if ((offset == -1 || offset == -9 || offset == 7) && (fromSquareMask & notAFile) == 0) continue;
                        if ((offset == 1 || offset == 9 || offset == -7) && (fromSquareMask & notHFile) == 0) continue;

                        // If destination is empty or has white pieces
                        if ((toSquareMask & ~position) != 0 || (toSquareMask & whitePieces) != 0)
                        {
                            MoveObject newMove = new MoveObject
                            {
                                startPosition = (int)fromSquareEnum,
                                EndSquare = (int)toSquareEnum
                            };
                            allMoves.Add(newMove);
                        }
                    }
                }

                blackKingBoard &= blackKingBoard - 1; // Remove the last set bit
            }

            return allMoves;
        }



        public static ulong GetBlackKingAttacks(ulong blackKingBoard)
        {
            return GenerateKingAttacks(blackKingBoard);
        }

        public static ulong GetWhiteKingAttacks(ulong whiteKingBoard)
        {
            return GenerateKingAttacks(whiteKingBoard);
        }

        private static ulong GenerateKingAttacks(ulong kingBoard)
        {
            ulong attacks = 0UL;
            int kingSquare = BitOperations.TrailingZeroCount(kingBoard);

            foreach (int offset in kingOffsets)
            {
                int targetSquare = kingSquare + offset;
                if (targetSquare >= 0 && targetSquare < 64)
                {
                    // Prevent wrapping on A and H files
                    ulong kingSquareMask = 1UL << kingSquare;
                    if ((offset == -1 || offset == -9 || offset == 7) && (kingSquareMask & notAFile) == 0) continue;
                    if ((offset == 1 || offset == 9 || offset == -7) && (kingSquareMask & notHFile) == 0) continue;

                    attacks |= 1UL << targetSquare;
                }
            }

            return attacks;
        }
    }
}

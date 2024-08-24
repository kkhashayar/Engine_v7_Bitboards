using System.Numerics;

namespace Bb_Engine.Generator;

internal static class Kings
{

    private static readonly int[] kingOffsets = { 1, -1, 8, -8, 9, -9, 7, -7 }; // Possible king moves

    public static List<MoveObject> GetWhiteKing(List<ulong> boards, ulong position)
    {
        return GenerateKingMoves(boards[5], position, boards, false);
    }

    public static List<MoveObject> GetBlackKing(List<ulong> boards, ulong position)
    {
        return GenerateKingMoves(boards[11], position, boards, true);
    }

    private static List<MoveObject> GenerateKingMoves(ulong kingBoard, ulong position, List<ulong> boards, bool isBlack)
    {
        List<MoveObject> allMoves = new();
        ulong enemyPieces = isBlack ? (boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5])
                                    : (boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11]);

        while (kingBoard != 0)
        {
            int fromSquare = BitOperations.TrailingZeroCount(kingBoard);
            ulong fromSquareMask = 1UL << fromSquare;

            foreach (int offset in kingOffsets)
            {
                int toSquare = fromSquare + offset;
                if (toSquare >= 0 && toSquare < 64)
                {
                    ulong toSquareMask = 1UL << toSquare;
                    if ((toSquareMask & ~position) != 0 || (toSquareMask & enemyPieces) != 0)
                    {
                        MoveObject newMove = new MoveObject
                        {
                            startPosition = fromSquare,
                            EndSquare = toSquare
                        };
                        allMoves.Add(newMove);
                    }
                }
            }

            kingBoard &= kingBoard - 1; // Remove the last set bit
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
                attacks |= 1UL << targetSquare;
            }
        }

        return attacks;
    }
}

using System.Numerics;
using System.Collections.Generic;

namespace Bb_Engine.Generator;

internal static class Kings
{
    private static readonly ulong[] KingAttacks = new ulong[64];

    static Kings()
    {
        InitializeKingAttacks();
    }

    private static void InitializeKingAttacks()
    {
        for (int square = 0; square < 64; square++)
        {
            KingAttacks[square] = ComputeKingAttacks(square);
        }
    }

    private static ulong ComputeKingAttacks(int square)
    {
        ulong attacks = 0UL;
        ulong bitboard = 1UL << square;

        int rank = square / 8;
        int file = square % 8;

        // Up
        if (rank < 7) attacks |= bitboard << 8;
        // Down
        if (rank > 0) attacks |= bitboard >> 8;
        // Left
        if (file > 0) attacks |= bitboard >> 1;
        // Right
        if (file < 7) attacks |= bitboard << 1;
        // Up-Left
        if (rank < 7 && file > 0) attacks |= bitboard << 7;
        // Up-Right
        if (rank < 7 && file < 7) attacks |= bitboard << 9;
        // Down-Left
        if (rank > 0 && file > 0) attacks |= bitboard >> 9;
        // Down-Right
        if (rank > 0 && file < 7) attacks |= bitboard >> 7;

        return attacks;
    }

    public static List<MoveObject> GetWhiteKing(List<ulong> boards)
    {
        return GenerateKingMoves(boards[5], boards, false);
    }

    public static List<MoveObject> GetBlackKing(List<ulong> boards)
    {
        return GenerateKingMoves(boards[11], boards, true);
    }

    private static List<MoveObject> GenerateKingMoves(ulong kingBoard, List<ulong> boards, bool isBlack)
    {
        List<MoveObject> allMoves = new();

        if (kingBoard == 0) return allMoves;

        int fromSquare = BitOperations.TrailingZeroCount(kingBoard);

        ulong ownPieces = isBlack ? (boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11])
                                  : (boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5]);

        ulong enemyPieces = isBlack ? (boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5])
                                    : (boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11]);

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
                IsCapture = isCapture
            };

            allMoves.Add(newMove);

            attacks &= attacks - 1;
        }

            return allMoves;
        }

    public static ulong GetKingAttacks(ulong kingBoard)
    {
        if (kingBoard == 0) return 0UL;

        int kingSquare = BitOperations.TrailingZeroCount(kingBoard);
        return KingAttacks[kingSquare];
    }
}

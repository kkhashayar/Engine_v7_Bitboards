using System.Numerics;

namespace Bb_Engine.Generator;

internal static class Pawns
{
    public static List<MoveObject> GetWhitePawns(List<ulong> boards, ulong position)
    {
     
        List<MoveObject> allMoves = new();
        ulong whitePawns = boards[0];
        ulong blackPieces = boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11];

        // Single Pushes
        ulong singlePushes = whitePawns << 8 & ~position;
        while (singlePushes != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(singlePushes);
            int fromSquare = toSquare - 8;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            singlePushes &= singlePushes - 1;
        }

        // Double Pushes
        ulong secondRank = whitePawns & 0x000000000000FF00UL;
        ulong doublePushes = secondRank << 16 & ~(position | position << 8);
        while (doublePushes != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(doublePushes);
            int fromSquare = toSquare - 16;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            doublePushes &= doublePushes - 1;
        }

        // Left Captures
        ulong leftCaptures = whitePawns << 7 & blackPieces & ~0x8080808080808080UL;
        while (leftCaptures != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(leftCaptures);
            int fromSquare = toSquare - 7;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            leftCaptures &= leftCaptures - 1;
        }

        // Right Captures
        ulong rightCaptures = whitePawns << 9 & blackPieces & ~0x0101010101010101UL;
        while (rightCaptures != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(rightCaptures);
            int fromSquare = toSquare - 9;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            rightCaptures &= rightCaptures - 1;
        }

        return allMoves;
    }

    public static ulong GetWhitePawnAttacks(List<ulong> boards)
    {
        ulong whitePawns = boards[0];
        ulong attacks = 0UL;

        // Calculate left and right attacks
        attacks |= (whitePawns >> 9) & ~0x8080808080808080UL; // Left attacks
        attacks |= (whitePawns >> 7) & ~0x0101010101010101UL; // Right attacks

        return attacks;
    }

    /***************************************************************************/
 
    /***************************************************************************/


    public static List<MoveObject> GetBlackPawns(List<ulong> boards, ulong position)
    {
        List<MoveObject> allMoves = new();
        ulong blackPawns = boards[6];
        ulong whitePieces = boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5];

        // Single Pushes
        ulong singlePushes = blackPawns >> 8 & ~position;
        while (singlePushes != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(singlePushes);
            int fromSquare = toSquare + 8;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            singlePushes &= singlePushes - 1;
        }

        // Double Pushes
        ulong seventhRank = blackPawns & 0x00FF000000000000UL;
        ulong doublePushes = seventhRank >> 16 & ~(position | position >> 8);
        while (doublePushes != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(doublePushes);
            int fromSquare = toSquare + 16;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            doublePushes &= doublePushes - 1;
        }

        // Left Captures
        ulong leftCaptures = blackPawns >> 9 & whitePieces & ~0x0101010101010101UL;
        while (leftCaptures != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(leftCaptures);
            int fromSquare = toSquare + 9;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            leftCaptures &= leftCaptures - 1;
        }

        // Right Captures
        ulong rightCaptures = blackPawns >> 7 & whitePieces & ~0x8080808080808080UL;
        while (rightCaptures != 0)
        {
            int toSquare = BitOperations.TrailingZeroCount(rightCaptures);
            int fromSquare = toSquare + 7;
            MoveObject newMove = new MoveObject
            {
                startPosition = fromSquare,
                EndSquare = toSquare
            };
            allMoves.Add(newMove);
            rightCaptures &= rightCaptures - 1;
        }

        return allMoves;
    }


    public static ulong GetBlackPawnAttacks(List<ulong> boards)
    {
        ulong blackPawns = boards[6];
        ulong attacks = 0UL;

        // Calculate left and right attacks
        attacks |= (blackPawns << 7) & ~0x8080808080808080UL; // Left attacks
        attacks |= (blackPawns << 9) & ~0x0101010101010101UL; // Right attacks

        return attacks;
    }

    
}

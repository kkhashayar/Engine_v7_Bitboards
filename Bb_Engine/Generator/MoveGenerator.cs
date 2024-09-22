using System.Numerics;

namespace Bb_Engine.Generator;


internal static class MoveGenerator
{
    internal static ulong position { get; set; } = 0UL;


    internal static List<MoveObject> GenerateWhiteMoves(List<ulong> boards)
    {
        List<MoveObject> moves = new();

        foreach (ulong board in boards) position |= board;

        for (int i = 0; i < boards.Count; i++)
        {
            
            if (i == 5) moves.AddRange(Generator.Kings.GetWhiteKingMoves(boards, position));

            //else if (i == 0) moves.AddRange(Generator.Pawns.GetWhitePawns(boards, position));

        }
        foreach (var move in moves)
        {

            moves = moves.Where(move => IsLegal(move, boards)).ToList(); // Filter for legal moves
        }
        return moves;
    }

    internal static List<MoveObject> GenerateBlackMoves(List<ulong> boards)
    {
        List<MoveObject> moves = new();
        foreach (ulong board in boards) position |= board;

        for (int i = 0; i < boards.Count; i++)
        {
            if (i == 6) moves.AddRange(Generator.Pawns.GetBlackPawns(boards, position));
            if (i == 11) moves.AddRange(Generator.Kings.GetBlackKingMoves(boards, position));
        }
        foreach (var move in moves)
        {

            moves = moves.Where(move => IsLegal(move, boards)).ToList(); // Filter for legal moves
        }
        return moves;
      
    }

    internal static bool IsLegal(MoveObject move, List<ulong> boards)
    {
        // Make the move temporarily
        MoveHandlers.MakeMove(boards, move);

        // Determine if the king is in check after the move
        bool isKingInCheck = IsKingInCheck(boards, GameState.Turn);

        // Undo the move
        MoveHandlers.UndoMove(boards);

        // The move is legal if the king is not in check after making it
        return !isKingInCheck;
    }

    private static bool IsKingInCheck(List<ulong> boards, int turn)
    {
        // Identify the king's position
        ulong kingBoard = (turn == 0) ? boards[5] : boards[11]; // White King or Black King
        int kingSquare = BitOperations.TrailingZeroCount(kingBoard);

        // Check for attacks on the king
        ulong enemyAttacks = CalculateAllEnemyAttacks(boards, turn);

        // If the king's square is under attack, the king is in check
        return (enemyAttacks & (1UL << kingSquare)) != 0;
    }

    private static ulong CalculateAllEnemyAttacks(List<ulong> boards, int turn)
    {
        ulong enemyAttacks = 0UL;

        // Depending on the turn, calculate all possible attacks from the opposing side
        if (turn == 0) // White's turn, so check black's pieces
        {
            enemyAttacks |= Generator.Pawns.GetBlackPawnAttacks(boards);

            enemyAttacks |= Generator.Kings.GetBlackKingAttacks(boards[11]);
        }
        else // Black's turn, so check white's pieces
        {
            enemyAttacks |= Generator.Pawns.GetWhitePawnAttacks(boards);

            enemyAttacks |= Generator.Kings.GetWhiteKingAttacks(boards[5]);
        }

        return enemyAttacks;
    }

}
/*
        ulong a = 0b0101; // 5 in binary
        ulong b = 0b0011; // 3 in binary

        a |= b;  // a becomes 0b0111 (7 in binary)

        
        // a = 0101
        // b = 0011
        // a |= b  results in a = 0111  (7 in decimal)



        BitOperations.TrailingZeroCount(moveBoard) 
        is a method that counts the number of consecutive 0 bits starting from the least significant bit (rightmost) 
        until it hits the first 1.
        
        Purpose:
            Identify the Lowest Set Bit: It tells you the position of the first 1 bit in moveBoard, 
            which corresponds to the lowest destination square for a move.

        Example:
        If moveBoard = 0b00001000 (8 in binary), TrailingZeroCount would return 3 because there are three 0 bits before the first 1.
        Usage:

        In move generation, this helps identify which square a pawn can move to next.

        ### Bitwise Operators - Definitions and Examples

        1. **AND (`&`)**: Compares each bit of two numbers. The result bit is `1` only if both corresponding bits are `1`.
           - **Example**: 
             ```csharp
             int result = 0b1100 & 0b1010;  // result = 0b1000 (8 in decimal)
             ```

        2. **OR (`|`)**: Compares each bit of two numbers. The result bit is `1` if either corresponding bit is `1`.
           - **Example**: 
             ```csharp
             int result = 0b1100 | 0b1010;  // result = 0b1110 (14 in decimal)
             ```

        3. **XOR (`^`)**: Compares each bit of two numbers. The result bit is `1` only if the corresponding bits are different.
           - **Example**: 
             ```csharp
             int result = 0b1100 ^ 0b1010;  // result = 0b0110 (6 in decimal)
             ```

        4. **NOT (`~`)**: Inverts all bits of the number (flips `0` to `1` and `1` to `0`).
           - **Example**: 
             ```csharp
             int result = ~0b1100;  // result = 0b...11110011 (-13 in decimal, two's complement)
             ```

        5. **Left Shift (`<<`)**: Shifts bits to the left by a specified number of positions, adding `0`s on the right. This is equivalent to multiplying the number by `2` for each shift.
           - **Example**: 
             ```csharp
             int result = 0b0011 << 2;  // result = 0b1100 (12 in decimal)
             ```

        6. **Right Shift (`>>`)**: Shifts bits to the right by a specified number of positions. For unsigned types, `0`s are added on the left; for signed types, the sign bit is extended.
           - **Example**: 
             ```csharp
             int result = 0b1100 >> 2;  // result = 0b0011 (3 in decimal)
             ```
 */
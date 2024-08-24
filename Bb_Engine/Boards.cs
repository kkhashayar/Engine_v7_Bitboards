namespace Bb_Engine;

public static class Boards
{
    internal static ulong WhitePawns { get; set; } = 0;
    internal static ulong WhiteRooks { get; set; } = 0;
    internal static ulong WhiteKnights { get; set; } = 0;
    internal static ulong WhiteBishops { get; set; } = 0;
    internal static ulong WhiteQueen { get; set; } = 0;
    internal static ulong WhiteKing { get; set; } = 0;

    internal static ulong BlackPawns { get; set; } = 0;
    internal static ulong BlackRooks { get; set; } = 0;
    internal static ulong BlackKnights { get; set; } = 0;
    internal static ulong BlackBishops { get; set; } = 0;
    internal static ulong BlackQueen { get; set; } = 0;
    internal static ulong BlackKing { get; set; } = 0;

    public static int InitialTurn { get; set; }
    public static int Turn { get; set; }


    public static List<ulong> GetBoards()
    {
        return new List<ulong>()
        { WhitePawns,
          WhiteRooks,
          WhiteKnights,
          WhiteBishops,
          WhiteQueen,
          WhiteKing,
          BlackPawns,
          BlackRooks,
          BlackKnights,
          BlackBishops,
          BlackQueen,
          BlackKing,
        };
    }


    internal static void SetBitOnSquare(ulong bitboard, int square)
    {
        // The |= operator performs a bitwise OR operation, updating the bitboard to include the bit set at the specified square.
        bitboard |= 1UL << square;
    }

    internal static void RemoveBitFromSquare(int square, ulong bitboard)
    {
        // bitboard &= ~(1UL << square); uses a bitwise AND to clear the bit at the specified square.
        bitboard &= ~(1UL << square);
    }

    internal static bool IsSquareEmpty(int square, ulong bitboard)
    {
        // bitboard & (1UL << square) checks if the bit at the square is set (1) or not (0).
        return (bitboard & (1UL << square)) == 0;
    }

    
}



/*
 * 0 for A1, 63 for H8
 * Set Bit: Place a piece on a specific square.
 * Clear Bit: Remove a piece from a specific square.
 * Toggle Bit: Flip the bit for a square (useful for move generation).
 * Check Bit: Determine if a piece is on a specific square.
 * Bit Scan: Find the index of the first/last set bit (LSB/MSB).
 * Count Bits: Count the number of set bits (population count).
 * Shift: Shift the bitboard left, right, up, or down (for move generation).
 */
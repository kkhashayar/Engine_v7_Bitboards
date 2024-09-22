namespace Bb_Engine;

public static class Boards
{
    internal static ulong WhitePawns   { get; set; } = 0;
    internal static ulong WhiteRooks   { get; set; } = 0;
    internal static ulong WhiteKnights { get; set; } = 0;
    internal static ulong WhiteBishops { get; set; } = 0;
    internal static ulong WhiteQueen   { get; set; } = 0;
    internal static ulong WhiteKing    { get; set; } = 0;

    internal static ulong BlackPawns   { get; set; } = 0;
    internal static ulong BlackRooks   { get; set; } = 0;
    internal static ulong BlackKnights { get; set; } = 0;
    internal static ulong BlackBishops { get; set; } = 0;
    internal static ulong BlackQueen   { get; set; } = 0;
    internal static ulong BlackKing    { get; set; } = 0;

    public static int InitialTurn { get; set; }
    public static int Turn { get; set; }

    public enum BoardSquares
    {
        a8, b8, c8, d8, e8, f8, g8, h8,
        a7, b7, c7, d7, e7, f7, g7, h7,
        a6, b6, c6, d6, e6, f6, g6, h6,
        a5, b5, c5, d5, e5, f5, g5, h5,
        a4, b4, c4, d4, e4, f4, g4, h4,
        a3, b3, c3, d3, e3, f3, g3, h3,
        a2, b2, c2, d2, e2, f2, g2, h2,
        a1, b1, c1, d1, e1, f1, g1, h1
    }
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
        // Shift the number 1 left by 'square' positions.
        // This creates a mask with a 1 at the position corresponding to 'square'
        // and 0s everywhere else.
        ulong mask = 1UL << square;

        // Use the bitwise OR operator to set the bit at 'square' to 1.
        // If the bit was already 1, it remains 1. If it was 0, it changes to 1.
        bitboard |= mask;

        // Note: This method changes the local variable 'bitboard' but does not
        // affect the original bitboard outside this method. To modify the original
        // bitboard, you would need to return the new value or pass by reference.
    }


    internal static void RemoveBitFromSquare(int square, ulong bitboard)
    {
        // Shift the number 1 left by 'square' positions to create a mask.
        // This mask has a 1 at the position corresponding to 'square' and 0s everywhere else.
        ulong mask = 1UL << square;

        // Invert the mask using the bitwise NOT (~) operator.
        // Now, the mask has 0 at the position corresponding to 'square' and 1s everywhere else.
        mask = ~mask;

        // Use the bitwise AND (&=) operator to clear the bit at the 'square' position.
        // If the bit was 1, it changes to 0; if it was 0, it remains 0.
        bitboard &= mask;

        // Note: Like the previous method, this changes a local copy of 'bitboard'.
        // To modify the original bitboard, you would need to return the new value or pass by reference.
    }


    internal static bool IsSquareEmpty(int square, ulong bitboard)
    {
        // Shift the number 1 left by 'square' positions to create a mask.
        // This mask has a 1 at the position corresponding to 'square' and 0s everywhere else.
        ulong mask = 1UL << square;

        // Use the bitwise AND (&) operator to check if the bit at 'square' is set (1) or not (0).
        // The result will be non-zero if the bit is set (meaning the square is occupied).
        // The result will be zero if the bit is not set (meaning the square is empty).
        bool isEmpty = (bitboard & mask) == 0;

        // Return true if the square is empty (bit is 0), false if it is occupied (bit is 1).
        return isEmpty;
    }


}

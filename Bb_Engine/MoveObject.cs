public class MoveObject
{
    public int startPosition { get; set; }
    public int EndSquare { get; set; }
    public int? capturedPiece { get; set; }
    public bool IsCastling { get; set; }

    // Convert the move to standard chess notation
    public override string ToString()
    {
        return $"{SquareToNotation(startPosition)}{SquareToNotation(EndSquare)}";
    }

    private string SquareToNotation(int square)
    {
        int file = square % 8;  // This is correct.
        int rank = square / 8;  // This gives the correct rank, but for top-down notation.

        // To flip the rank so that 0 corresponds to '1' and 7 corresponds to '8':
        return $"{(char)('a' + file)}{(char)('8' - rank)}";
    }
}

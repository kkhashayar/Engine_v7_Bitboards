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
        int file = square % 8;
        int rank = square / 8;
        return $"{(char)('a' + file)}{(char)('1' + rank)}";
    }
}

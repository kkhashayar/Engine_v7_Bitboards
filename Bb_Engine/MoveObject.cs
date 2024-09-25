public class MoveObject
{
    public int startPosition { get; set; }
    public int EndSquare { get; set; }
    public int? capturedPiece { get; set; }
    public bool IsCastling { get; set; }
    public bool IsCapture { get; set; }
    public PieceType PieceType { get; set; }
    public MoveType MoveType { get; set; }
    public PieceType? PromotionPiece { get; set; }

    // Convert the move to standard chess notation
    public override string ToString()
    {
        string moveString = $"{SquareToNotation(startPosition)}{SquareToNotation(EndSquare)}";

        if (PromotionPiece.HasValue)
        {
            moveString += PromotionPieceToChar(PromotionPiece.Value);
        }

        return moveString;
    }

    private string SquareToNotation(int square)
    {
        int file = square % 8;
        int rank = 8 - (square / 8);
        return $"{(char)('a' + file)}{rank}";
    }

    private char PromotionPieceToChar(PieceType pieceType)
    {
        return pieceType switch
        {
            PieceType.Queen => 'q',
            PieceType.Rook => 'r',
            PieceType.Bishop => 'b',
            PieceType.Knight => 'n',
            _ => ' '
        };
    }
}

public enum PieceType
{
    Pawn,
    Knight,
    Bishop,
    Rook,
    Queen,
    King
}

public enum MoveType
{
    Quiet,
    Capture,
    DoublePawnPush,
    EnPassant,
    Promotion,
    CapturePromotion,
    Castling
}

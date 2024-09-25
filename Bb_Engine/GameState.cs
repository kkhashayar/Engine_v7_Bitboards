public class GameState
{
    public int Turn { get; set; }
    public bool WhiteCastleKingSide { get; set; }
    public bool WhiteCastleQueenSide { get; set; }
    public bool BlackCastleKingSide { get; set; }
    public bool BlackCastleQueenSide { get; set; }
    public int EnPassantSquare { get; set; }

    public void RestoreFromSnapshot(GameStateSnapshot snapshot)
    {
        Turn = snapshot.Turn;
        WhiteCastleKingSide = snapshot.WhiteCastleKingSide;
        WhiteCastleQueenSide = snapshot.WhiteCastleQueenSide;
        BlackCastleKingSide = snapshot.BlackCastleKingSide;
        BlackCastleQueenSide = snapshot.BlackCastleQueenSide;
        EnPassantSquare = snapshot.EnPassantSquare;
    }
}

public class GameStateSnapshot
{
    public int Turn { get; set; }
    public bool WhiteCastleKingSide { get; set; }
    public bool WhiteCastleQueenSide { get; set; }
    public bool BlackCastleKingSide { get; set; }
    public bool BlackCastleQueenSide { get; set; }
    public int EnPassantSquare { get; set; }

    public GameStateSnapshot()
    {
    }

    public GameStateSnapshot(GameState gameState)
    {
        Turn = gameState.Turn;
        WhiteCastleKingSide = gameState.WhiteCastleKingSide;
        WhiteCastleQueenSide = gameState.WhiteCastleQueenSide;
        BlackCastleKingSide = gameState.BlackCastleKingSide;
        BlackCastleQueenSide = gameState.BlackCastleQueenSide;
        EnPassantSquare = gameState.EnPassantSquare;
    }
}

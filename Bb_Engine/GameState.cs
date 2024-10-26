public class GameState
{
    public int Turn { get; set; }
    public bool WhiteCastleKingSide { get; set; }
    public bool WhiteCastleQueenSide { get; set; }
    public bool BlackCastleKingSide { get; set; }
    public bool BlackCastleQueenSide { get; set; }
    public int EnPassantSquare { get; set; }

    public GameState()
    {
        // Initialize default values 
        Turn = 0; // Default to White's turn
        WhiteCastleKingSide = true;
        WhiteCastleQueenSide = true;
        BlackCastleKingSide = true;
        BlackCastleQueenSide = true;
        EnPassantSquare = -1; // -1 indicates no en passant square
    }
    // Copy constructor
    public GameState(GameState other)
    {
        this.Turn = other.Turn;
        this.WhiteCastleKingSide = other.WhiteCastleKingSide;
        this.WhiteCastleQueenSide = other.WhiteCastleQueenSide;
        this.BlackCastleKingSide = other.BlackCastleKingSide;
        this.BlackCastleQueenSide = other.BlackCastleQueenSide;
        this.EnPassantSquare = other.EnPassantSquare;
    }

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

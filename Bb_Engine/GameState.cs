namespace Bb_Engine
{
    public static class GameState
    {
        public static int InitialTurn { get; set; }
        public static int Turn { get; set; }

        public static bool WhiteCastleKingSide { get; set; } = true;
        public static bool WhiteCastleQueenSide { get; set; } = true;
        public static bool BlackCastleKingSide { get; set; } = true;
        public static bool BlackCastleQueenSide { get; set; } = true;
    }

    public class GameStateSnapshot
    {
        public int Turn { get; set; }
        public bool WhiteCastleKingSide { get; set; }
        public bool WhiteCastleQueenSide { get; set; }
        public bool BlackCastleKingSide { get; set; }
        public bool BlackCastleQueenSide { get; set; }

        public GameStateSnapshot Clone()
        {
            return (GameStateSnapshot)this.MemberwiseClone();
        }
    }
}

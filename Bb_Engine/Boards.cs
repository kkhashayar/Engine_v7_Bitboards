namespace Bb_Engine
{
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

        public enum BoardSquares
        {
            a8, b8, c8, d8, e8, f8, g8, h8,   // Indices 0-7
            a7, b7, c7, d7, e7, f7, g7, h7,   // Indices 8-15
            a6, b6, c6, d6, e6, f6, g6, h6,   // Indices 16-23
            a5, b5, c5, d5, e5, f5, g5, h5,   // Indices 24-31
            a4, b4, c4, d4, e4, f4, g4, h4,   // Indices 32-39
            a3, b3, c3, d3, e3, f3, g3, h3,   // Indices 40-47
            a2, b2, c2, d2, e2, f2, g2, h2,   // Indices 48-55
            a1, b1, c1, d1, e1, f1, g1, h1    // Indices 56-63
        }

        public static List<ulong> GetBoards()
        {
            return new List<ulong>()
            {
                WhitePawns,
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
                BlackKing
            };
        }
    }
}


        /*
          ******** Board layout ********

            00  01  02  03  04  05  06  07  // Rank 8
            08  09  10  11  12  13  14  15  // Rank 7
            16  17  18  19  20  21  22  23  // Rank 6
            24  25  26  27  28  29  30  31  // Rank 5
            32  33  34  35  36  37  38  39  // Rank 4
            40  41  42  43  44  45  46  47  // Rank 3
            48  49  50  51  52  53  54  55  // Rank 2
            56  57  58  59  60  61  62  63  // Rank 1
            a   b   c   d   e   f   g   h
         */
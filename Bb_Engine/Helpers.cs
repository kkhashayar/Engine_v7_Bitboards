namespace Bb_Engine
{
    public static class Helpers
    {
        public static class PieceMapper
        {
            public static readonly Dictionary<char, char> pieceToUnicode = new Dictionary<char, char>
            {
                {'P', '\u2659'}, // white pawn
                {'N', '\u2658'}, // white knight
                {'B', '\u2657'}, // white bishop
                {'R', '\u2656'}, // white rook
                {'Q', '\u2655'}, // white queen
                {'K', '\u2654'}, // white king
                {'p', '\u265F'}, // black pawn
                {'n', '\u265E'}, // black knight
                {'b', '\u265D'}, // black bishop
                {'r', '\u265C'}, // black rook
                {'q', '\u265B'}, // black queen
                {'k', '\u265A'}  // black king
            };
        }

        public static readonly Dictionary<string, ulong> CoordinateToBitBoard = new Dictionary<string, ulong>
        {
            { "a8", 1UL << 0 },   { "b8", 1UL << 1 },   { "c8", 1UL << 2 },   { "d8", 1UL << 3 },
            { "e8", 1UL << 4 },   { "f8", 1UL << 5 },   { "g8", 1UL << 6 },   { "h8", 1UL << 7 },
            { "a7", 1UL << 8 },   { "b7", 1UL << 9 },   { "c7", 1UL << 10 },  { "d7", 1UL << 11 },
            { "e7", 1UL << 12 },  { "f7", 1UL << 13 },  { "g7", 1UL << 14 },  { "h7", 1UL << 15 },
            { "a6", 1UL << 16 },  { "b6", 1UL << 17 },  { "c6", 1UL << 18 },  { "d6", 1UL << 19 },
            { "e6", 1UL << 20 },  { "f6", 1UL << 21 },  { "g6", 1UL << 22 },  { "h6", 1UL << 23 },
            { "a5", 1UL << 24 },  { "b5", 1UL << 25 },  { "c5", 1UL << 26 },  { "d5", 1UL << 27 },
            { "e5", 1UL << 28 },  { "f5", 1UL << 29 },  { "g5", 1UL << 30 },  { "h5", 1UL << 31 },
            { "a4", 1UL << 32 },  { "b4", 1UL << 33 },  { "c4", 1UL << 34 },  { "d4", 1UL << 35 },
            { "e4", 1UL << 36 },  { "f4", 1UL << 37 },  { "g4", 1UL << 38 },  { "h4", 1UL << 39 },
            { "a3", 1UL << 40 },  { "b3", 1UL << 41 },  { "c3", 1UL << 42 },  { "d3", 1UL << 43 },
            { "e3", 1UL << 44 },  { "f3", 1UL << 45 },  { "g3", 1UL << 46 },  { "h3", 1UL << 47 },
            { "a2", 1UL << 48 },  { "b2", 1UL << 49 },  { "c2", 1UL << 50 },  { "d2", 1UL << 51 },
            { "e2", 1UL << 52 },  { "f2", 1UL << 53 },  { "g2", 1UL << 54 },  { "h2", 1UL << 55 },
            { "a1", 1UL << 56 },  { "b1", 1UL << 57 },  { "c1", 1UL << 58 },  { "d1", 1UL << 59 },
            { "e1", 1UL << 60 },  { "f1", 1UL << 61 },  { "g1", 1UL << 62 },  { "h1", 1UL << 63 },
        };

        public static readonly Dictionary<ulong, string> BitBoardToCoordinate = new Dictionary<ulong, string>
        {
            { 1UL << 0,  "a8" },  { 1UL << 1,  "b8" },  { 1UL << 2,  "c8" },  { 1UL << 3,  "d8" },
            { 1UL << 4,  "e8" },  { 1UL << 5,  "f8" },  { 1UL << 6,  "g8" },  { 1UL << 7,  "h8" },
            { 1UL << 8,  "a7" },  { 1UL << 9,  "b7" },  { 1UL << 10, "c7" },  { 1UL << 11, "d7" },
            { 1UL << 12, "e7" },  { 1UL << 13, "f7" },  { 1UL << 14, "g7" },  { 1UL << 15, "h7" },
            { 1UL << 16, "a6" },  { 1UL << 17, "b6" },  { 1UL << 18, "c6" },  { 1UL << 19, "d6" },
            { 1UL << 20, "e6" },  { 1UL << 21, "f6" },  { 1UL << 22, "g6" },  { 1UL << 23, "h6" },
            { 1UL << 24, "a5" },  { 1UL << 25, "b5" },  { 1UL << 26, "c5" },  { 1UL << 27, "d5" },
            { 1UL << 28, "e5" },  { 1UL << 29, "f5" },  { 1UL << 30, "g5" },  { 1UL << 31, "h5" },
            { 1UL << 32, "a4" },  { 1UL << 33, "b4" },  { 1UL << 34, "c4" },  { 1UL << 35, "d4" },
            { 1UL << 36, "e4" },  { 1UL << 37, "f4" },  { 1UL << 38, "g4" },  { 1UL << 39, "h4" },
            { 1UL << 40, "a3" },  { 1UL << 41, "b3" },  { 1UL << 42, "c3" },  { 1UL << 43, "d3" },
            { 1UL << 44, "e3" },  { 1UL << 45, "f3" },  { 1UL << 46, "g3" },  { 1UL << 47, "h3" },
            { 1UL << 48, "a2" },  { 1UL << 49, "b2" },  { 1UL << 50, "c2" },  { 1UL << 51, "d2" },
            { 1UL << 52, "e2" },  { 1UL << 53, "f2" },  { 1UL << 54, "g2" },  { 1UL << 55, "h2" },
            { 1UL << 56, "a1" },  { 1UL << 57, "b1" },  { 1UL << 58, "c1" },  { 1UL << 59, "d1" },
            { 1UL << 60, "e1" },  { 1UL << 61, "f1" },  { 1UL << 62, "g1" },  { 1UL << 63, "h1" },
        };
    }
}

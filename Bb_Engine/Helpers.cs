namespace Bb_Engine;

public static class Helpers
{
    public static class PieceMapper
    {
        public static Dictionary<char, char> pieceToUnicode = new Dictionary<char, char>
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


    public static Dictionary<string, ulong> CoordinateToBitBoard = new Dictionary<string, ulong>
    {
        {"h8", 0x8000000000000000UL}, {"g8", 0x4000000000000000UL}, {"f8", 0x2000000000000000UL}, {"e8", 0x1000000000000000UL},
        {"d8", 0x800000000000000UL},  {"c8", 0x400000000000000UL},  {"b8", 0x200000000000000UL},  {"a8", 0x100000000000000UL},
        {"h7", 0x80000000000000UL},   {"g7", 0x40000000000000UL},   {"f7", 0x20000000000000UL},   {"e7", 0x10000000000000UL},
        {"d7", 0x8000000000000UL},    {"c7", 0x4000000000000UL},    {"b7", 0x2000000000000UL},    {"a7", 0x1000000000000UL},
        {"h6", 0x800000000000UL},     {"g6", 0x400000000000UL},     {"f6", 0x200000000000UL},     {"e6", 0x100000000000UL},
        {"d6", 0x80000000000UL},      {"c6", 0x40000000000UL},      {"b6", 0x20000000000UL},      {"a6", 0x10000000000UL},
        {"h5", 0x8000000000UL},       {"g5", 0x4000000000UL},       {"f5", 0x2000000000UL},       {"e5", 0x1000000000UL},
        {"d5", 0x800000000UL},        {"c5", 0x400000000UL},        {"b5", 0x200000000UL},        {"a5", 0x100000000UL},
        {"h4", 0x80000000UL},         {"g4", 0x40000000UL},         {"f4", 0x20000000UL},         {"e4", 0x10000000UL},
        {"d4", 0x8000000UL},          {"c4", 0x4000000UL},          {"b4", 0x2000000UL},          {"a4", 0x1000000UL},
        {"h3", 0x800000UL},           {"g3", 0x400000UL},           {"f3", 0x200000UL},           {"e3", 0x100000UL},
        {"d3", 0x80000UL},            {"c3", 0x40000UL},            {"b3", 0x20000UL},            {"a3", 0x10000UL},
        {"h2", 0x8000UL},             {"g2", 0x4000UL},             {"f2", 0x2000UL},             {"e2", 0x1000UL},
        {"d2", 0x800UL},              {"c2", 0x400UL},              {"b2", 0x200UL},              {"a2", 0x100UL},
        {"h1", 0x80UL},               {"g1", 0x40UL},               {"f1", 0x20UL},               {"e1", 0x10UL},
        {"d1", 0x8UL},                {"c1", 0x4UL},                {"b1", 0x2UL},                {"a1", 0x1UL}
    };

    public static Dictionary<ulong, string> BitBoardToCoordinate = new Dictionary<ulong, string>
    {
        {0x8000000000000000UL, "h8"}, {0x4000000000000000UL, "g8"}, {0x2000000000000000UL, "f8"}, {0x1000000000000000UL, "e8"},
        {0x800000000000000UL, "d8"},  {0x400000000000000UL, "c8"},  {0x200000000000000UL, "b8"},  {0x100000000000000UL, "a8"},
        {0x80000000000000UL, "h7"},   {0x40000000000000UL, "g7"},   {0x20000000000000UL, "f7"},   {0x10000000000000UL, "e7"},
        {0x8000000000000UL, "d7"},    {0x4000000000000UL, "c7"},    {0x2000000000000UL, "b7"},    {0x1000000000000UL, "a7"},
        {0x800000000000UL, "h6"},     {0x400000000000UL, "g6"},     {0x200000000000UL, "f6"},     {0x100000000000UL, "e6"},
        {0x80000000000UL, "d6"},      {0x40000000000UL, "c6"},      {0x20000000000UL, "b6"},      {0x10000000000UL, "a6"},
        {0x8000000000UL, "h5"},       {0x4000000000UL, "g5"},       {0x2000000000UL, "f5"},       {0x1000000000UL, "e5"},
        {0x800000000UL, "d5"},        {0x400000000UL, "c5"},        {0x200000000UL, "b5"},        {0x100000000UL, "a5"},
        {0x80000000UL, "h4"},         {0x40000000UL, "g4"},         {0x20000000UL, "f4"},         {0x10000000UL, "e4"},
        {0x8000000UL, "d4"},          {0x4000000UL, "c4"},          {0x2000000UL, "b4"},          {0x1000000UL, "a4"},
        {0x800000UL, "h3"},           {0x400000UL, "g3"},           {0x200000UL, "f3"},           {0x100000UL, "e3"},
        {0x80000UL, "d3"},            {0x40000UL, "c3"},            {0x20000UL, "b3"},            {0x10000UL, "a3"},
        {0x8000UL, "h2"},             {0x4000UL, "g2"},             {0x2000UL, "f2"},             {0x1000UL, "e2"},
        {0x800UL, "d2"},              {0x400UL, "c2"},              {0x200UL, "b2"},              {0x100UL, "a2"},
        {0x80UL, "h1"},               {0x40UL, "g1"},               {0x20UL, "f1"},               {0x10UL, "e1"},
        {0x8UL, "d1"},                {0x4UL, "c1"},                {0x2UL, "b1"},                {0x1UL, "a1"}

    };
}

/*
          
{
    public static class Helpers
    {
        public static class PieceMapper
        {
            public static Dictionary<char, char> pieceToUnicode = new Dictionary<char, char>
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

        public static Dictionary<string, ulong> CoordinateToBitBoard = new Dictionary<string, ulong>();
        public static Dictionary<ulong, string> BitBoardToCoordinate = new Dictionary<ulong, string>();

        static Helpers()
        {
            InitializeCoordinateMappings();
        }

        private static void InitializeCoordinateMappings()
        {
            for (int square = 0; square < 64; square++)
            {
                int file = square % 8;
                int rank = square / 8;

                string coordinate = $"{(char)('a' + file)}{rank + 1}";
                ulong bitboard = 1UL << square;

                CoordinateToBitBoard[coordinate] = bitboard;
                BitBoardToCoordinate[bitboard] = coordinate;
            }
        }
    }
}

 */

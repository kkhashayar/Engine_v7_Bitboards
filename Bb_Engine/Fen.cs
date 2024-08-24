namespace Bb_Engine
{
    public static class Fen
    {
        public static void Read(string fen)
        {
            if (string.IsNullOrEmpty(fen)) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            string[] fenParts = fen.Split(" ");
            string piecePosition = fenParts[0];
            // starts from top left
            int rank = 7;
            int file = 0;
            foreach (char symbol in piecePosition)
            {
                if (char.IsDigit(symbol))
                {
                    int skipSquares = int.Parse(symbol.ToString());
                    file += skipSquares;
                }
                else if (symbol == '/')
                {
                    rank--;
                    file = 0;
                }
                else
                {
                    int bitPosition = rank * 8 + file;

                    switch (symbol)
                    {
                        case 'P':
                            Boards.WhitePawns |= 1UL << bitPosition;
                            break;
                        case 'p':
                            Boards.BlackPawns |= 1UL << bitPosition;
                            break;
                        case 'R':
                            Boards.WhiteRooks |= 1UL << bitPosition;
                            break;
                        case 'r':
                            Boards.BlackRooks |= 1UL << bitPosition;
                            break;
                        case 'B':
                            Boards.WhiteBishops |= 1UL << bitPosition;
                            break;
                        case 'b':
                            Boards.BlackBishops |= 1UL << bitPosition;
                            break;
                        case 'N':
                            Boards.WhiteKnights |= 1UL << bitPosition;
                            break;
                        case 'n':
                            Boards.BlackKnights |= 1UL << bitPosition;
                            break;
                        case 'Q':
                            Boards.WhiteQueen |= 1UL << bitPosition;
                            break;
                        case 'q':
                            Boards.BlackQueen |= 1UL << bitPosition;
                            break;
                        case 'K':
                            Boards.WhiteKing |= 1UL << bitPosition;
                            break;
                        case 'k':
                            Boards.BlackKing |= 1UL << bitPosition;
                            break;
                    }

                    file++;
                }
            }
            if (fenParts[1] == "w") GameState.InitialTurn = 0;
            else if (fenParts[1] == "b") GameState.InitialTurn = 1; 
        }
    }
}
/*
    
    Board layout
    A1 Bottom left is at index 0 
    H8 top right   is at index 63

    56, 57, 58, 59, 60, 61, 62, 63,
    48, 49, 50, 51, 52, 53, 54, 55,
    40, 41, 42, 43, 44, 45, 46, 47,
    32, 33, 34, 35, 36, 37, 38, 39,
    24, 25, 26, 27, 28, 29, 30, 31,
    16, 17, 18, 19, 20, 21, 22, 23,
    8,  9,  10, 11, 12, 13, 14, 15,
    0,  1,  2,  3,  4,  5,  6,  7



    if (string.IsNullOrEmpty(fen)) fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
 */
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
    ******** Board layout ********
    
    A1 Bottom left  is at index 56
    A8 Top left     is at index 00

    H1 Bottom right is at index 63
    H8 top right    is at index 07

        00 01 02 03 04 05 06 07
        08 09 10 11 12 13 14 15
        16 17 18 19 20 21 22 23
        24 25 26 27 28 29 30 31
        32 33 34 35 36 37 38 39
        40 41 42 43 44 45 46 47
        48 49 50 51 52 53 54 55
        56 57 58 59 60 61 62 63

 */
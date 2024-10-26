namespace Bb_Engine
{
    public static class Fen
    {
        public static void Read(string fen)
        {
            if (string.IsNullOrEmpty(fen))
                fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

            string[] fenParts = fen.Split(' ');
            string piecePosition = fenParts[0];

            // Reset all boards
            Boards.WhitePawns = Boards.WhiteRooks = Boards.WhiteKnights = Boards.WhiteBishops = 0UL;
            Boards.WhiteQueen = Boards.WhiteKing = 0UL;
            Boards.BlackPawns = Boards.BlackRooks = Boards.BlackKnights = Boards.BlackBishops = 0UL;
            Boards.BlackQueen = Boards.BlackKing = 0UL;

            // Start from Rank 8 (rank = 0)
            int rank = 0;
            int file = 0;

            foreach (char symbol in piecePosition)
            {
                if (char.IsDigit(symbol))
                {
                    file += symbol - '0';
                }
                else if (symbol == '/')
                {
                    rank++;
                    file = 0;
                }
                else
                {
                    int bitPosition = rank * 8 + file;

                    switch (symbol)
                    {
                        case 'P': Boards.WhitePawns |= 1UL << bitPosition; break;
                        case 'p': Boards.BlackPawns |= 1UL << bitPosition; break;
                        case 'R': Boards.WhiteRooks |= 1UL << bitPosition; break;
                        case 'r': Boards.BlackRooks |= 1UL << bitPosition; break;
                        case 'N': Boards.WhiteKnights |= 1UL << bitPosition; break;
                        case 'n': Boards.BlackKnights |= 1UL << bitPosition; break;
                        case 'B': Boards.WhiteBishops |= 1UL << bitPosition; break;
                        case 'b': Boards.BlackBishops |= 1UL << bitPosition; break;
                        case 'Q': Boards.WhiteQueen |= 1UL << bitPosition; break;
                        case 'q': Boards.BlackQueen |= 1UL << bitPosition; break;
                        case 'K': Boards.WhiteKing |= 1UL << bitPosition; break;
                        case 'k': Boards.BlackKing |= 1UL << bitPosition; break;
                    }
                    file++;
                }
            }

            Boards.InitialTurn = (fenParts.Length > 1 && fenParts[1] == "b") ? 1 : 0;
            // Parse castling rights and en passant square if needed
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
}

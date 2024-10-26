using Bb_Engine;
using Bb_Engine.ExternalResources;


//string fen = "8/5p2/7k/8/8/7K/5P2/8 w - - 0 1";
string fen = "8/8/7k/8/8/7K/8/8 w - - 0 1";

Fen.Read(fen);

// Initialize GameState
GameState gameState = new GameState
{
    Turn = Boards.InitialTurn,
    WhiteCastleKingSide = true,
    WhiteCastleQueenSide = true,
    BlackCastleKingSide = true,
    BlackCastleQueenSide = true,
    EnPassantSquare = -1
};


int perftDepth = 7;

RunPerftWithVerification(fen, perftDepth, gameState);

PrintBoard(Boards.GetBoards(), gameState);

// PrintBoardSquareNumbers();

////////////////////////////////////////////////////////////// Functions //////////////////////////////////////////////////////////////

void RunPerftWithVerification(string fen, int perftDepth, GameState gameState)
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("******* Engine 7 BB *******  \n");
    Console.WriteLine($"Perft test at depth: {perftDepth} on: \n");
    Console.WriteLine($"{fen} \n");

    // Run the perft test
    Perft.TestPerft(fen, perftDepth, gameState);

    Console.WriteLine();
    Console.Beep(2000, 50);
    Console.WriteLine("Press 'V' to verify with Stockfish or any key to exit \n");

    char input = Console.ReadKey().KeyChar;
    if (input == 'v' || input == 'V')
    {
        VerifyWithStockfish(fen, perftDepth);
        Console.WriteLine("Press any key to continue\n");
        Console.ReadKey();
    }

    Environment.Exit(0);
}

void VerifyWithStockfish(string fen, int depth)
{
    string stockfishPath = "\"D:\\DATA\\stockfish_15.1_win_x64_avx2\\stockfish-windows-2022-x86-64-avx2.exe\"";
    StockfishIntegration stockfish = new StockfishIntegration(stockfishPath);
    stockfish.StartStockfish();

    // Send the FEN to Stockfish
    stockfish.SendCommand($"position fen {fen}");
    stockfish.SendCommand($"go perft {depth}");

    // Read the output
    string output;

    while ((output = stockfish.ReadOutput()) != null)
    {
        Console.WriteLine(output);
        if (output.StartsWith("Nodes searched:")) break;
    }
}

void PrintBoard(List<ulong> boards, GameState gameState)
{
    for (int rank = 7; rank >= 0; rank--) // Start from rank 8 down to rank 1
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write((rank + 1) + " "); // Print rank number

        for (int file = 0; file < 8; file++)
        {
            int square = rank * 8 + file;
            char pieceSymbol = '.';

            if ((boards[0] & (1UL << square)) != 0) pieceSymbol = 'P';
            else if ((boards[6] & (1UL << square)) != 0) pieceSymbol = 'p';
            else if ((boards[1] & (1UL << square)) != 0) pieceSymbol = 'R';
            else if ((boards[7] & (1UL << square)) != 0) pieceSymbol = 'r';
            else if ((boards[2] & (1UL << square)) != 0) pieceSymbol = 'N';
            else if ((boards[8] & (1UL << square)) != 0) pieceSymbol = 'n';
            else if ((boards[3] & (1UL << square)) != 0) pieceSymbol = 'B';
            else if ((boards[9] & (1UL << square)) != 0) pieceSymbol = 'b';
            else if ((boards[4] & (1UL << square)) != 0) pieceSymbol = 'Q';
            else if ((boards[10] & (1UL << square)) != 0) pieceSymbol = 'q';
            else if ((boards[5] & (1UL << square)) != 0) pieceSymbol = 'K';
            else if ((boards[11] & (1UL << square)) != 0) pieceSymbol = 'k';

            if (pieceSymbol != '.')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.OutputEncoding = System.Text.Encoding.Unicode;

                // Map piece symbols to Unicode characters
                Console.Write(Helpers.PieceMapper.pieceToUnicode[pieceSymbol] + " ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(". ");
            }
        }

        Console.WriteLine();
    }

    Console.ForegroundColor = ConsoleColor.DarkBlue;
    Console.WriteLine("  A B C D E F G H");
    Console.WriteLine();
    Console.WriteLine($"Turn: {(gameState.Turn == 0 ? "White" : "Black")}");
    Thread.Sleep(100);
}

void PrintBoardSquareNumbers()
{
    Console.WriteLine();
    Console.WriteLine("Board Square Numbers");
    Console.WriteLine("********************");

    // Loop over board ranks
    for (int rank = 7; rank >= 0; rank--)
    {
        // Loop over board files
        for (int file = 0; file < 8; file++)
        {
            // Convert file and rank to square index
            int square = rank * 8 + file;
            Console.Write(square.ToString("D2") + " "); // Print square index with formatting
        }
        Console.WriteLine(); // New line after each rank
    }
}

using Bb_Engine;
using Bb_Engine.ExternalResources;

string fen = "8/8/8/7k/8/7K/8/8 w - - 0 1";
Fen.Read(fen);

RunPerftWIthVerification(fen, 2);

PrintBoard(Boards.GetBoards());

PrintBoardSquareNumbers();

//////////////////////////////////////////////////////////////// Functions ////////////////////////////////////////////////////////////////
void RunPerftWIthVerification(string fen, int perftDepth)
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.WriteLine("******* Engine 7 BitBoards *******  \n");
    Console.WriteLine($"Perft test in depth: {perftDepth} on: \n");
    Console.WriteLine($"{fen} \n");
    Perft.TestPerft(fen, perftDepth);
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
        if (output.StartsWith("Stockfish result:  ")) break;
    }

}




void PrintBoard(List<ulong> boards)
{
    for (int rank = 7; rank >= 0; rank--) // start from the 8th rank and move down to the 1st
    {
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.Write((rank + 1) + " "); // Print rank number

        for (int file = 0; file < 8; file++)
        {
            int shift = rank * 8 + file;
            char pieceSymbol = '.';
           
            if      ((boards[0]  & (1UL << shift))  != 0) pieceSymbol = 'P';
            else if ((boards[6]  & (1UL << shift))  != 0) pieceSymbol = 'p';
            else if ((boards[1]  & (1UL << shift))  != 0) pieceSymbol = 'R';
            else if ((boards[7]  & (1UL << shift))  != 0) pieceSymbol = 'r';
            else if ((boards[2]  & (1UL << shift))  != 0) pieceSymbol = 'N';
            else if ((boards[8]  & (1UL << shift))  != 0) pieceSymbol = 'n';
            else if ((boards[3]  & (1UL << shift))  != 0) pieceSymbol = 'B';
            else if ((boards[9]  & (1UL << shift))  != 0) pieceSymbol = 'b';
            else if ((boards[4]  & (1UL << shift))  != 0) pieceSymbol = 'Q';
            else if ((boards[10] & (1UL << shift))  != 0) pieceSymbol = 'q';
            else if ((boards[5]  & (1UL << shift))  != 0) pieceSymbol = 'K';
            else if ((boards[11] & (1UL << shift))  != 0) pieceSymbol = 'k';


            if(pieceSymbol != '.')
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.OutputEncoding = System.Text.Encoding.Unicode;
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
    Console.WriteLine($"Turn: {GameState.Turn}");
    Thread.Sleep(100);
}

void PrintBoardSquareNumbers()
{
    Console.WriteLine();
    Console.WriteLine("Board Square numbers");
    Console.WriteLine("********************");
   
    // Loop over board ranks
    for (int rank = 0; rank < 8; rank++)
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
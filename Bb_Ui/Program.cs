using Bb_Engine;

string fen = "4k3/7p/8/8/8/8/7P/4K3 w - - 0 1";
Fen.Read(fen);


Perft.TestPerft(fen, 3);
Console.ReadKey();  



PrintBoard(Boards.GetBoards());

//PrintBoardSquareNumbers();

//////////////////////////////////////////////////////////////// Functions //////////////////////////////////////////////////////////////// 
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
           
            if ((boards[0] & (1UL << shift)) != 0)      pieceSymbol = 'P';
            else if ((boards[6] & (1UL << shift)) != 0) pieceSymbol = 'p';
            else if ((boards[1] & (1UL << shift)) != 0) pieceSymbol = 'R';
            else if ((boards[7] & (1UL << shift)) != 0) pieceSymbol = 'r';
            else if ((boards[2] & (1UL << shift)) != 0) pieceSymbol = 'N';
            else if ((boards[8] & (1UL << shift)) != 0) pieceSymbol = 'n';
            else if ((boards[3] & (1UL << shift)) != 0) pieceSymbol = 'B';
            else if ((boards[9] & (1UL << shift)) != 0) pieceSymbol = 'b';
            else if ((boards[4] & (1UL << shift)) != 0) pieceSymbol = 'Q';
            else if ((boards[10] & (1UL << shift)) != 0) pieceSymbol = 'q';
            else if ((boards[5] & (1UL << shift)) != 0) pieceSymbol = 'K';
            else if ((boards[11] & (1UL << shift)) != 0) pieceSymbol = 'k';


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
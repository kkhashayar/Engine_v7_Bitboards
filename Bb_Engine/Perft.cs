using Bb_Engine.Generator;
using Bb_Engine;

public static class Perft
{
    public static ulong RunPerft(List<ulong> boards, int depth, int turn, bool legalMovesOnly = true)
    {
        List<MoveObject> moves = new(); 
        if (depth == 0)
        {
            return 1; // Base case: one leaf node reached
        }
        
        if(turn == 0) moves = MoveGenerator.GenerateWhiteMoves(boards);
        else if(turn  == 1) moves = MoveGenerator.GenerateBlackMoves(boards);   


        ulong nodes = 0;

        foreach (var move in moves)
        {
            // Clone the current board state before making the move
            List<ulong> clonedBoards = new List<ulong>(boards);  // Deep copy the bitboards

            MoveHandlers.MakeMove(clonedBoards, move);
            nodes += RunPerft(clonedBoards, depth - 1, turn ^ 1, legalMovesOnly); // FIXED: pass the flipped turn without modifying `turn` in place
        }

        return nodes;
    }



    public static void TestPerft(string fen, int depth)
    {
        // Parse the FEN to initialize the board
        Fen.Read(fen);

        List<ulong> boards = Boards.GetBoards();
        int turn = GameState.InitialTurn;

        ulong nodes = 0;

        if (depth == 0)
        {
            nodes = RunPerft(boards, depth, turn);
            Console.WriteLine($"Nodes searched: {nodes}");
        }
        else
        {
            List<MoveObject> moves = new();
            if (turn == 0) moves = MoveGenerator.GenerateWhiteMoves(boards);
            else if(turn == 1) moves  = MoveGenerator.GenerateBlackMoves(boards);   
            

            foreach (var move in moves)
            {
                MoveHandlers.MakeMove(boards, move);
                ulong childNodes = RunPerft(boards, depth - 1, turn ^ 1); // FIXED: pass the flipped turn without modifying `turn` in place
                MoveHandlers.UndoMove(boards);
                // NO NEED to flip the turn again here, since it's already handled properly by UndoMove
                Console.WriteLine($"{move.ToString()}: {childNodes}");
                nodes += childNodes;
            }

            Console.WriteLine($"\nTotal nodes: {nodes}");
        }
    }

}

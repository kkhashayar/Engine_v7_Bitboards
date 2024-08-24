using Bb_Engine.Generator;
using Bb_Engine;

public static class Perft
{
    public static ulong RunPerft(List<ulong> boards, int depth, int turn, bool legalMovesOnly = true)
    {
        if (depth == 0)
        {
            return 1; // Base case: one leaf node reached
        }

        List<MoveObject> moves = MoveGenerator.GenerateMoves(boards, turn, legalMovesOnly);
        ulong nodes = 0;

        foreach (var move in moves)
        {
            MoveHandlers.MakeMove(boards, move);
            nodes += RunPerft(boards, depth - 1, 1 - turn, legalMovesOnly); // Switch turn
            MoveHandlers.UndoMove(boards);
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

        if (depth == 1)
        {
            nodes = RunPerft(boards, depth, turn);
            Console.WriteLine($"Nodes searched: {nodes}");
        }
        else
        {
            List<MoveObject> moves = MoveGenerator.GenerateMoves(boards, turn, true);

            foreach (var move in moves)
            {
                MoveHandlers.MakeMove(boards, move);
                ulong childNodes = RunPerft(boards, depth - 1, 1 - turn);
                MoveHandlers.UndoMove(boards);

                Console.WriteLine($"{move.ToString()}: {childNodes}"); // Assuming MoveObject has a proper ToString override
                nodes += childNodes;
            }

            Console.WriteLine($"\nTotal nodes: {nodes}");
        }
    }
}

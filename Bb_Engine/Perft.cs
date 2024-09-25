using Bb_Engine.Generator;
using Bb_Engine;
using System.Collections.Generic;
using System;

public static class Perft
{
    public static ulong RunPerft(List<ulong> boards, int depth, GameState gameState, bool legalMovesOnly = true)
    {
        List<MoveObject> moves = new(); 
        if (depth == 0)
        {
            return 1; // Base case: one leaf node reached
        }

        List<MoveObject> moves = MoveGenerator.GenerateMoves(boards, gameState, legalMovesOnly);
        ulong nodes = 0;

        foreach (var move in moves)
        {
            MoveHandlers.MakeMove(boards, move, gameState);
            nodes += RunPerft(boards, depth - 1, gameState, legalMovesOnly);
            MoveHandlers.UndoMove(boards, gameState);
        }

        return nodes;
    }

    public static void TestPerft(string fen, int depth, GameState gameState)
    {
        // Parse the FEN to initialize the board
        Fen.Read(fen);

        List<ulong> boards = Boards.GetBoards();
        gameState.Turn = Boards.InitialTurn;

        ulong nodes = 0;

        List<MoveObject> moves = MoveGenerator.GenerateMoves(boards, gameState, true);

        foreach (var move in moves)
        {
            MoveHandlers.MakeMove(boards, move, gameState);
            ulong childNodes = RunPerft(boards, depth - 1, gameState, true);
            MoveHandlers.UndoMove(boards, gameState);

            Console.WriteLine($"{move.ToString()}: {childNodes}");
            nodes += childNodes;
        }

        Console.WriteLine($"\nTotal nodes: {nodes}");
    }
}

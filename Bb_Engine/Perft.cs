using Bb_Engine.Generator;
using Bb_Engine;
using System.Collections.Generic;
using System;

public static class Perft
{
    public static ulong RunPerft(List<ulong> boards, int depth, GameState gameState, bool legalMovesOnly = true)
    {
        if (depth == 0)
        {
            return 1;
        }

        List<MoveObject> moves = MoveGenerator.GenerateMoves(boards, gameState, legalMovesOnly);
        ulong nodes = 0;

        foreach (var move in moves)
        {
            // Clone gameState before making the move
            GameState clonedGameState = new GameState(gameState);

            MoveHandlers.MakeMove(boards, move, clonedGameState);
            nodes += RunPerft(boards, depth - 1, clonedGameState, legalMovesOnly);
            MoveHandlers.UndoMove(boards, clonedGameState);
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
            // Clone gameState before making the move
            GameState clonedGameState = new GameState(gameState);

            MoveHandlers.MakeMove(boards, move, clonedGameState);
            ulong childNodes = RunPerft(boards, depth - 1, clonedGameState, true);
            MoveHandlers.UndoMove(boards, clonedGameState);

            Console.WriteLine($"{move.ToString()}: {childNodes}");
            nodes += childNodes;
        }

        Console.WriteLine($"\nTotal nodes: {nodes}");
    }

}

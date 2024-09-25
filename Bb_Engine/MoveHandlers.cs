using System.Collections.Generic;

namespace Bb_Engine
{
    internal static class MoveHandlers
    {
        private static Stack<GameStateSnapshot> gameStateHistory = new Stack<GameStateSnapshot>();
        private static Stack<MoveObject> moveHistory = new Stack<MoveObject>();

        public static void MakeMove(List<ulong> boards, MoveObject move, GameState gameState)
        {
            // Save current game state
            var snapshot = new GameStateSnapshot(gameState);
            gameStateHistory.Push(snapshot);

            ulong fromMask = 1UL << move.startPosition;
            ulong toMask = 1UL << move.EndSquare;

            // Identify and move the piece
            for (int i = 0; i < boards.Count; i++)
            {
                if ((boards[i] & fromMask) != 0)
                {
                    // Handle captures before moving the piece
                    for (int j = 0; j < boards.Count; j++)
                    {
                        if ((boards[j] & toMask) != 0 && j != i)
                        {
                            boards[j] &= ~toMask; // Remove captured piece
                            move.capturedPiece = j;
                            break;
                        }
                    }

                    // Now move the piece
                    if (move.IsCastling)
                    {
                        HandleCastling(i, move, boards);
                    }
                    else
                    {
                        boards[i] &= ~fromMask; // Remove piece from start square
                        boards[i] |= toMask;    // Place piece on destination square
                    }

                    break;
                }
            }

            moveHistory.Push(move);

            // Update game state
            UpdateGameStateAfterMove(move, gameState);

            // Switch turn
            gameState.Turn = 1 - gameState.Turn;
        }

        public static void UndoMove(List<ulong> boards, GameState gameState)
        {
            if (moveHistory.Count == 0) return;

            MoveObject move = moveHistory.Pop();
            GameStateSnapshot snapshot = gameStateHistory.Pop();

            // Restore game state
            gameState.RestoreFromSnapshot(snapshot);

            ulong fromMask = 1UL << move.startPosition;
            ulong toMask = 1UL << move.EndSquare;

            for (int i = 0; i < boards.Count; i++)
            {
                if ((boards[i] & toMask) != 0)
                {
                    // Undo move
                    boards[i] &= ~toMask;
                    boards[i] |= fromMask;

                    // Restore captured piece
                    if (move.capturedPiece.HasValue)
                    {
                        boards[move.capturedPiece.Value] |= toMask;
                    }

                    break;
                }
            }
        }

        private static void HandleCastling(int pieceIndex, MoveObject move, List<ulong> boards)
        {
            // Implement castling logic as needed
        }

        private static void UpdateGameStateAfterMove(MoveObject move, GameState gameState)
        {
            if (move.startPosition == 4) // White King moved
            {
                gameState.WhiteCastleKingSide = false;
                gameState.WhiteCastleQueenSide = false;
            }
            else if (move.startPosition == 60) // Black King moved
            {
                gameState.BlackCastleKingSide = false;
                gameState.BlackCastleQueenSide = false;
            }
            else if (move.startPosition == 0 || move.startPosition == 7) // White Rook moved
            {
                if (move.startPosition == 0)
                {
                    gameState.WhiteCastleQueenSide = false;
                }
                else
                {
                    gameState.WhiteCastleKingSide = false;
                }
            }
            else if (move.startPosition == 56 || move.startPosition == 63) // Black Rook moved
            {
                if (move.startPosition == 56)
                {
                    gameState.BlackCastleQueenSide = false;
                }
                else
                {
                    gameState.BlackCastleKingSide = false;
                }
            }
        }
    }
}

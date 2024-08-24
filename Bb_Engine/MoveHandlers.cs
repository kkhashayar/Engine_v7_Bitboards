using System.Collections.Generic;

namespace Bb_Engine
{
    internal static class MoveHandlers
    {
        private static Stack<GameStateSnapshot> gameStateHistory = new Stack<GameStateSnapshot>();
        private static Stack<MoveObject> moveHistory = new Stack<MoveObject>();

        public static void MakeMove(List<ulong> boards, MoveObject move)
        {
            // Save current game state
            var snapshot = new GameStateSnapshot
            {
                Turn = GameState.Turn,
                WhiteCastleKingSide = GameState.WhiteCastleKingSide,
                WhiteCastleQueenSide = GameState.WhiteCastleQueenSide,
                BlackCastleKingSide = GameState.BlackCastleKingSide,
                BlackCastleQueenSide = GameState.BlackCastleQueenSide
            };
            gameStateHistory.Push(snapshot);

            ulong fromMask = 1UL << move.startPosition;
            ulong toMask = 1UL << move.EndSquare;

            // Identify and move the piece
            for (int i = 0; i < boards.Count; i++)
            {
                if ((boards[i] & fromMask) != 0)
                {
                    // Handle castling
                    if (move.IsCastling)
                    {
                        HandleCastling(i, move, boards);
                    }
                    else
                    {
                        boards[i] &= ~fromMask; // Remove piece from start square
                        boards[i] |= toMask;    // Place piece on destination square
                    }

                    // Handle captures
                    for (int j = 0; j < boards.Count; j++)
                    {
                        if ((boards[j] & toMask) != 0 && j != i)
                        {
                            boards[j] &= ~toMask; // Remove captured piece
                            move.capturedPiece = j;
                            break;
                        }
                    }

                    break;
                }
            }

            moveHistory.Push(move);

            // Update game state
            UpdateGameStateAfterMove(move);

            // Switch turn
            GameState.Turn = GameState.Turn == 0 ? 1 : 0;
        }

        public static void UndoMove(List<ulong> boards)
        {
            if (moveHistory.Count == 0) return;

            MoveObject move = moveHistory.Pop();
            GameStateSnapshot snapshot = gameStateHistory.Pop();

            // Restore game state
            GameState.Turn = snapshot.Turn;
            GameState.WhiteCastleKingSide = snapshot.WhiteCastleKingSide;
            GameState.WhiteCastleQueenSide = snapshot.WhiteCastleQueenSide;
            GameState.BlackCastleKingSide = snapshot.BlackCastleKingSide;
            GameState.BlackCastleQueenSide = snapshot.BlackCastleQueenSide;

            ulong fromMask = 1UL << move.startPosition;
            ulong toMask = 1UL << move.EndSquare;

            for (int i = 0; i < boards.Count; i++)
            {
                if ((boards[i] & toMask) != 0)
                {
                    if (move.IsCastling)
                    {
                        UndoCastling(i, move, boards);
                    }
                    else
                    {
                        boards[i] &= ~toMask;
                        boards[i] |= fromMask;
                    }

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
            if (pieceIndex == 5) // White King
            {
                if (move.EndSquare == 62) // White king-side castle
                {
                    boards[1] = boards[1] & ~0x0000000000000008UL | 0x0800000000000000UL;
                }
                else if (move.EndSquare == 58) // White queen-side castle
                {
                    boards[1] = boards[1] & ~0x0000000000000010UL | 0x0100000000000000UL;
                }
            }
            else if (pieceIndex == 11) // Black King
            {
                if (move.EndSquare == 6) // Black king-side castle
                {
                    boards[7] = boards[7] & ~0x0000000000000008UL | 0x0800000000000000UL;
                }
                else if (move.EndSquare == 2) // Black queen-side castle
                {
                    boards[7] = boards[7] & ~0x0000000000000010UL | 0x0100000000000000UL;
                }
            }
        }

        private static void UndoCastling(int pieceIndex, MoveObject move, List<ulong> boards)
        {
            if (pieceIndex == 5) // White King
            {
                if (move.EndSquare == 62)
                {
                    boards[1] = boards[1] & ~0x0800000000000000UL | 0x0000000000000008UL;
                }
                else if (move.EndSquare == 58)
                {
                    boards[1] = boards[1] & ~0x0100000000000000UL | 0x0000000000000010UL;
                }
            }
            else if (pieceIndex == 11) // Black King
            {
                if (move.EndSquare == 6)
                {
                    boards[7] = boards[7] & ~0x0800000000000000UL | 0x0000000000000008UL;
                }
                else if (move.EndSquare == 2)
                {
                    boards[7] = boards[7] & ~0x0100000000000000UL | 0x0000000000000010UL;
                }
            }
        }

        private static void UpdateGameStateAfterMove(MoveObject move)
        {
            if (move.startPosition == 4) // White King moved
            {
                GameState.WhiteCastleKingSide = false;
                GameState.WhiteCastleQueenSide = false;
            }
            else if (move.startPosition == 60) // Black King moved
            {
                GameState.BlackCastleKingSide = false;
                GameState.BlackCastleQueenSide = false;
            }
            else if (move.startPosition == 0 || move.startPosition == 7) // White Rook moved
            {
                if (move.startPosition == 0)
                {
                    GameState.WhiteCastleQueenSide = false;
                }
                else
                {
                    GameState.WhiteCastleKingSide = false;
                }
            }
            else if (move.startPosition == 56 || move.startPosition == 63) // Black Rook moved
            {
                if (move.startPosition == 56)
                {
                    GameState.BlackCastleQueenSide = false;
                }
                else
                {
                    GameState.BlackCastleKingSide = false;
                }
            }
        }
    }
}

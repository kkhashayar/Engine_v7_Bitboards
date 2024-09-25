using System.Numerics;
using System.Collections.Generic;

namespace Bb_Engine.Generator
{
    internal static class Pawns
    {
        // File masks for edge detection
        private const ulong FileA = 0x0101010101010101UL;
        private const ulong FileH = 0x8080808080808080UL;
        private const ulong NotFileA = ~FileA;
        private const ulong NotFileH = ~FileH;
        private const ulong Rank2 = 0x000000000000FF00UL;
        private const ulong Rank7 = 0x00FF000000000000UL;
        private const ulong Rank8 = 0xFF00000000000000UL;
        private const ulong Rank1 = 0x00000000000000FFUL;

        // Promotion pieces
        private static readonly PieceType[] PromotionPieces = new PieceType[]
        {
            PieceType.Queen,
            PieceType.Rook,
            PieceType.Bishop,
            PieceType.Knight
        };

        public static List<MoveObject> GetWhitePawns(List<ulong> boards, GameState gameState)
        {
            List<MoveObject> allMoves = new();
            ulong whitePawns = boards[0];
            ulong blackPieces = boards[6] | boards[7] | boards[8] | boards[9] | boards[10] | boards[11];
            ulong emptySquares = ~(boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5] | blackPieces);

            // Single Pushes
            ulong singlePushes = (whitePawns << 8) & emptySquares;
            ulong promotions = singlePushes & Rank8;
            ulong quietMoves = singlePushes & ~Rank8;

            // Add promotion moves
            AddPawnPromotions(promotions, -8, PieceType.Pawn, ref allMoves, isCapture: false);

            // Add quiet single pushes
            AddPawnMoves(quietMoves, -8, PieceType.Pawn, MoveType.Quiet, ref allMoves);

            // Double Pushes
            ulong doublePushes = ((whitePawns & Rank2) << 16) & emptySquares & (emptySquares << 8);
            AddPawnMoves(doublePushes, -16, PieceType.Pawn, MoveType.DoublePawnPush, ref allMoves);

            // Captures
            ulong leftCaptures = (whitePawns << 7) & blackPieces & NotFileH;
            ulong rightCaptures = (whitePawns << 9) & blackPieces & NotFileA;

            // Add promotion captures
            ulong leftPromotionCaptures = leftCaptures & Rank8;
            ulong rightPromotionCaptures = rightCaptures & Rank8;

            AddPawnPromotions(leftPromotionCaptures, -7, PieceType.Pawn, ref allMoves, isCapture: true);
            AddPawnPromotions(rightPromotionCaptures, -9, PieceType.Pawn, ref allMoves, isCapture: true);

            // Add normal captures
            AddPawnMoves(leftCaptures & ~Rank8, -7, PieceType.Pawn, MoveType.Capture, ref allMoves);
            AddPawnMoves(rightCaptures & ~Rank8, -9, PieceType.Pawn, MoveType.Capture, ref allMoves);

            // En Passant Captures
            if (gameState.EnPassantSquare != -1)
            {
                ulong enPassantMask = 1UL << gameState.EnPassantSquare;

                // Left en passant
                ulong leftEnPassant = (whitePawns << 7) & enPassantMask & NotFileH;
                if (leftEnPassant != 0)
                {
                    int fromSquare = BitOperations.TrailingZeroCount(leftEnPassant) - 7;
                    MoveObject move = new MoveObject
                    {
                        startPosition = fromSquare,
                        EndSquare = gameState.EnPassantSquare,
                        MoveType = MoveType.EnPassant,
                        PieceType = PieceType.Pawn
                    };
                    allMoves.Add(move);
                }

                // Right en passant
                ulong rightEnPassant = (whitePawns << 9) & enPassantMask & NotFileA;
                if (rightEnPassant != 0)
                {
                    int fromSquare = BitOperations.TrailingZeroCount(rightEnPassant) - 9;
                    MoveObject move = new MoveObject
                    {
                        startPosition = fromSquare,
                        EndSquare = gameState.EnPassantSquare,
                        MoveType = MoveType.EnPassant,
                        PieceType = PieceType.Pawn
                    };
                    allMoves.Add(move);
                }
            }

            return allMoves;
        }

        public static List<MoveObject> GetBlackPawns(List<ulong> boards, GameState gameState)
        {
            List<MoveObject> allMoves = new();
            ulong blackPawns = boards[6];
            ulong whitePieces = boards[0] | boards[1] | boards[2] | boards[3] | boards[4] | boards[5];
            ulong emptySquares = ~(blackPawns | boards[7] | boards[8] | boards[9] | boards[10] | boards[11] | whitePieces);

            // Single Pushes
            ulong singlePushes = (blackPawns >> 8) & emptySquares;
            ulong promotions = singlePushes & Rank1;
            ulong quietMoves = singlePushes & ~Rank1;

            // Add promotion moves
            AddPawnPromotions(promotions, 8, PieceType.Pawn, ref allMoves, isCapture: false);

            // Add quiet single pushes
            AddPawnMoves(quietMoves, 8, PieceType.Pawn, MoveType.Quiet, ref allMoves);

            // Double Pushes
            ulong doublePushes = ((blackPawns & Rank7) >> 16) & emptySquares & (emptySquares >> 8);
            AddPawnMoves(doublePushes, 16, PieceType.Pawn, MoveType.DoublePawnPush, ref allMoves);

            // Captures
            ulong leftCaptures = (blackPawns >> 9) & whitePieces & NotFileA;
            ulong rightCaptures = (blackPawns >> 7) & whitePieces & NotFileH;

            // Add promotion captures
            ulong leftPromotionCaptures = leftCaptures & Rank1;
            ulong rightPromotionCaptures = rightCaptures & Rank1;

            AddPawnPromotions(leftPromotionCaptures, 9, PieceType.Pawn, ref allMoves, isCapture: true);
            AddPawnPromotions(rightPromotionCaptures, 7, PieceType.Pawn, ref allMoves, isCapture: true);

            // Add normal captures
            AddPawnMoves(leftCaptures & ~Rank1, 9, PieceType.Pawn, MoveType.Capture, ref allMoves);
            AddPawnMoves(rightCaptures & ~Rank1, 7, PieceType.Pawn, MoveType.Capture, ref allMoves);

            // En Passant Captures
            if (gameState.EnPassantSquare != -1)
            {
                ulong enPassantMask = 1UL << gameState.EnPassantSquare;

                // Left en passant
                ulong leftEnPassant = (blackPawns >> 9) & enPassantMask & NotFileA;
                if (leftEnPassant != 0)
                {
                    int fromSquare = BitOperations.TrailingZeroCount(leftEnPassant) + 9;
                    MoveObject move = new MoveObject
                    {
                        startPosition = fromSquare,
                        EndSquare = gameState.EnPassantSquare,
                        MoveType = MoveType.EnPassant,
                        PieceType = PieceType.Pawn
                    };
                    allMoves.Add(move);
                }

                // Right en passant
                ulong rightEnPassant = (blackPawns >> 7) & enPassantMask & NotFileH;
                if (rightEnPassant != 0)
                {
                    int fromSquare = BitOperations.TrailingZeroCount(rightEnPassant) + 7;
                    MoveObject move = new MoveObject
                    {
                        startPosition = fromSquare,
                        EndSquare = gameState.EnPassantSquare,
                        MoveType = MoveType.EnPassant,
                        PieceType = PieceType.Pawn
                    };
                    allMoves.Add(move);
                }
            }

            return allMoves;
        }

        private static void AddPawnMoves(ulong bitboard, int offset, PieceType pieceType, MoveType moveType, ref List<MoveObject> moves)
        {
            while (bitboard != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(bitboard);
                int fromSquare = toSquare + offset;

                MoveObject move = new MoveObject
                {
                    startPosition = fromSquare,
                    EndSquare = toSquare,
                    PieceType = pieceType,
                    MoveType = moveType
                };
                moves.Add(move);

                bitboard &= bitboard - 1;
            }
        }

        private static void AddPawnPromotions(ulong bitboard, int offset, PieceType pieceType, ref List<MoveObject> moves, bool isCapture)
        {
            MoveType moveType = isCapture ? MoveType.Capture : MoveType.Quiet;

            while (bitboard != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(bitboard);
                int fromSquare = toSquare + offset;

                foreach (var promotionPiece in PromotionPieces)
                {
                    MoveObject move = new MoveObject
                    {
                        startPosition = fromSquare,
                        EndSquare = toSquare,
                        PieceType = pieceType,
                        MoveType = moveType,
                        PromotionPiece = promotionPiece
                    };
                    moves.Add(move);
                }

                bitboard &= bitboard - 1;
            }
        }

        public static ulong GetWhitePawnAttacks(List<ulong> boards)
        {
            ulong whitePawns = boards[0];
            ulong attacks = 0UL;

            attacks |= (whitePawns << 7) & NotFileH; // Left attacks
            attacks |= (whitePawns << 9) & NotFileA; // Right attacks

            return attacks;
        }

        public static ulong GetBlackPawnAttacks(List<ulong> boards)
        {
            ulong blackPawns = boards[6];
            ulong attacks = 0UL;

            attacks |= (blackPawns >> 7) & NotFileA; // Left attacks
            attacks |= (blackPawns >> 9) & NotFileH; // Right attacks

            return attacks;
        }
    }
}

using System.Numerics;
using System.Collections.Generic;

namespace Bb_Engine.Generator
{
    internal static class Pawns
    {
        private const ulong FileA = 0x0101010101010101UL;
        private const ulong FileH = 0x8080808080808080UL;
        private const ulong NotFileA = ~FileA;
        private const ulong NotFileH = ~FileH;

        private const ulong Rank1 = 0x00000000000000FFUL;
        private const ulong Rank2 = 0x000000000000FF00UL;
        private const ulong Rank7 = 0x00FF000000000000UL;
        private const ulong Rank8 = 0xFF00000000000000UL;

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
            AddPawnMoves(singlePushes & ~Rank8, 8, PieceType.Pawn, MoveType.Quiet, ref allMoves);

            // Promotions
            ulong promotions = (singlePushes & Rank8);
            AddPawnPromotions(promotions, 8, PieceType.Pawn, ref allMoves, isCapture: false);

            // Double Pushes
            ulong doublePushes = ((whitePawns & Rank2) << 16) & emptySquares & (emptySquares << 8);
            AddPawnMoves(doublePushes, 16, PieceType.Pawn, MoveType.DoublePawnPush, ref allMoves);

            // Captures
            ulong leftCaptures = (whitePawns << 7) & blackPieces & NotFileH;
            ulong rightCaptures = (whitePawns << 9) & blackPieces & NotFileA;
            AddPawnMoves(leftCaptures & ~Rank8, 7, PieceType.Pawn, MoveType.Capture, ref allMoves);
            AddPawnMoves(rightCaptures & ~Rank8, 9, PieceType.Pawn, MoveType.Capture, ref allMoves);

            // Promotion Captures
            ulong leftPromotionCaptures = (leftCaptures & Rank8);
            ulong rightPromotionCaptures = (rightCaptures & Rank8);
            AddPawnPromotions(leftPromotionCaptures, 7, PieceType.Pawn, ref allMoves, isCapture: true);
            AddPawnPromotions(rightPromotionCaptures, 9, PieceType.Pawn, ref allMoves, isCapture: true);

            // En Passant
            if (gameState.EnPassantSquare != -1)
            {
                ulong enPassantMask = 1UL << gameState.EnPassantSquare;
                ulong leftEnPassant = (whitePawns << 7) & enPassantMask & NotFileH;
                ulong rightEnPassant = (whitePawns << 9) & enPassantMask & NotFileA;
                AddEnPassantMoves(leftEnPassant, 7, gameState.EnPassantSquare, ref allMoves);
                AddEnPassantMoves(rightEnPassant, 9, gameState.EnPassantSquare, ref allMoves);
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
            AddPawnMoves(singlePushes & ~Rank1, -8, PieceType.Pawn, MoveType.Quiet, ref allMoves);

            // Promotions
            ulong promotions = (singlePushes & Rank1);
            AddPawnPromotions(promotions, -8, PieceType.Pawn, ref allMoves, isCapture: false);

            // Double Pushes
            ulong doublePushes = ((blackPawns & Rank7) >> 16) & emptySquares & (emptySquares >> 8);
            AddPawnMoves(doublePushes, -16, PieceType.Pawn, MoveType.DoublePawnPush, ref allMoves);

            // Captures
            ulong leftCaptures = (blackPawns >> 9) & whitePieces & NotFileA;
            ulong rightCaptures = (blackPawns >> 7) & whitePieces & NotFileH;
            AddPawnMoves(leftCaptures & ~Rank1, -9, PieceType.Pawn, MoveType.Capture, ref allMoves);
            AddPawnMoves(rightCaptures & ~Rank1, -7, PieceType.Pawn, MoveType.Capture, ref allMoves);

            // Promotion Captures
            ulong leftPromotionCaptures = (leftCaptures & Rank1);
            ulong rightPromotionCaptures = (rightCaptures & Rank1);
            AddPawnPromotions(leftPromotionCaptures, -9, PieceType.Pawn, ref allMoves, isCapture: true);
            AddPawnPromotions(rightPromotionCaptures, -7, PieceType.Pawn, ref allMoves, isCapture: true);

            // En Passant
            if (gameState.EnPassantSquare != -1)
            {
                ulong enPassantMask = 1UL << gameState.EnPassantSquare;
                ulong leftEnPassant = (blackPawns >> 9) & enPassantMask & NotFileA;
                ulong rightEnPassant = (blackPawns >> 7) & enPassantMask & NotFileH;
                AddEnPassantMoves(leftEnPassant, -9, gameState.EnPassantSquare, ref allMoves);
                AddEnPassantMoves(rightEnPassant, -7, gameState.EnPassantSquare, ref allMoves);
            }

            return allMoves;
        }

        private static void AddPawnMoves(ulong bitboard, int offset, PieceType pieceType, MoveType moveType, ref List<MoveObject> moves)
        {
            while (bitboard != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(bitboard);
                int fromSquare = toSquare - offset;
                if (fromSquare < 0 || fromSquare > 63)
                {
                    bitboard &= bitboard - 1;
                    continue;
                }

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
            MoveType moveType = isCapture ? MoveType.CapturePromotion : MoveType.Promotion;
            while (bitboard != 0)
            {
                int toSquare = BitOperations.TrailingZeroCount(bitboard);
                int fromSquare = toSquare - offset;
                if (fromSquare < 0 || fromSquare > 63)
                {
                    bitboard &= bitboard - 1;
                    continue;
                }

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

        private static void AddEnPassantMoves(ulong bitboard, int offset, int enPassantSquare, ref List<MoveObject> moves)
        {
            while (bitboard != 0)
            {
                int fromSquare = BitOperations.TrailingZeroCount(bitboard) - offset;
                if (fromSquare < 0 || fromSquare > 63)
                {
                    bitboard &= bitboard - 1;
                    continue;
                }

                MoveObject move = new MoveObject
                {
                    startPosition = fromSquare,
                    EndSquare = enPassantSquare,
                    MoveType = MoveType.EnPassant,
                    PieceType = PieceType.Pawn
                };
                moves.Add(move);
                bitboard &= bitboard - 1;
            }
        }
    }
}

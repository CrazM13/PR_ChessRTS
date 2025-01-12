using Godot;
using System;
using System.Collections.Generic;

public partial class KnightMovement : ChessMovement {

	private static readonly Vector2I[] KnightMoves = new Vector2I[] {
		new Vector2I(2, 1),
		new Vector2I(2, -1),
		new Vector2I(-2, 1),
		new Vector2I(-2, -1),
		new Vector2I(1, 2),
		new Vector2I(1, -2),
		new Vector2I(-1, 2),
		new Vector2I(-1, -2),
	};

	public override Vector2I[] GetMovementOptions(ChessPiece piece) {

		List<Vector2I> totalMoves = new List<Vector2I>();

		foreach (Vector2I move in KnightMoves) {
			if (!IsTakenByTeam(piece.BoardPosition + move, piece.Team)) totalMoves.Add(piece.BoardPosition + move);
		}

		return totalMoves.ToArray();

	}
}

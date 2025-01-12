using Godot;
using System;
using System.Collections.Generic;

public partial class KingMovement : ChessMovement {

	private static readonly Vector2I[] kingMoves = new Vector2I[] {
		new Vector2I(0, 1),
		new Vector2I(0, -1),
		new Vector2I(1, 0),
		new Vector2I(-1, 0),
		new Vector2I(1, 1),
		new Vector2I(1, -1),
		new Vector2I(-1, 1),
		new Vector2I(-1, -1),
	};

	public override Vector2I[] GetMovementOptions(ChessPiece piece) {
		List<Vector2I> totalMoves = new List<Vector2I>();

		foreach (Vector2I move in kingMoves) {
			if (!IsTakenByTeam(piece.BoardPosition + move, piece.Team)) totalMoves.Add(piece.BoardPosition + move);
		}

		return totalMoves.ToArray();
	}
}

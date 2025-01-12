using Godot;
using System;

public abstract class ChessMovement {

	public abstract Vector2I[] GetMovementOptions(ChessPiece piece);
	public bool CanMoveTo(ChessPiece piece, Vector2I to) {
		Vector2I[] moves = GetMovementOptions(piece);
		for (int i = 0; i < moves.Length; i++) {
			if (moves[i] == to) {
				return true;
			}
		}

		return false;
	}

	public static bool IsTaken(Vector2I position) {
		return GameManager.Instance.GetPiece(position) != null;
	}

	public static bool IsTakenByTeam(Vector2I position, Teams team) {
		ChessPiece piece = GameManager.Instance.GetPiece(position);
		if (piece != null) {
			return piece.Team == team;
		}
		return false;
	}

}

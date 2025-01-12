using Godot;
using System;
using System.Collections.Generic;

public partial class QueenMovement : ChessMovement {

	public override Vector2I[] GetMovementOptions(ChessPiece piece) {
		List<Vector2I> totalMoves = new List<Vector2I>();

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(0, i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(0, -i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(i, 0);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(-i, 0);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(i, i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(-i, -i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(i, -i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		for (int i = 0; i < GameManager.MAX_MOVE; i++) {
			Vector2I newMove = piece.BoardPosition + new Vector2I(-i, i);
			if (!IsTakenByTeam(newMove, piece.Team)) totalMoves.Add(newMove);
			if (IsTaken(newMove)) {
				break;
			}
		}

		return totalMoves.ToArray();
	}

}

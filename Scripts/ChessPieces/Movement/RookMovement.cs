using Godot;
using System;

public partial class RookMovement : ChessMovement {

	public override bool CanMoveTo(Vector2I from, Vector2I to) {
		int xDiff = Mathf.Abs(from.X - to.X);
		int yDiff = Mathf.Abs(to.Y - from.Y);

		return (xDiff <= GameManager.MAX_MOVE && yDiff <= GameManager.MAX_MOVE) && ((xDiff != 0 && yDiff == 0) || (xDiff == 0 && yDiff != 0));
	}

	public override Vector2I[] GetMovementOptions(Vector2I position) {
		Vector2I[] totalMoves = new Vector2I[4 * GameManager.MAX_MOVE];

		for (int i = 0, c = 0; i < totalMoves.Length; i += 4, c++) {
			totalMoves[i + 0] = position + new Vector2I(c, 0);
			totalMoves[i + 1] = position + new Vector2I(-c, 0);
			totalMoves[i + 2] = position + new Vector2I(0, -c);
			totalMoves[i + 3] = position + new Vector2I(0, c);
		}

		return totalMoves;
	}
}

using Godot;
using System;

public partial class BishopMovement : ChessMovement {
	public override bool CanMoveTo(Vector2I from, Vector2I to) {
		int xDiff = Mathf.Abs(from.X - to.X);
		int yDiff = Mathf.Abs(to.Y - from.Y);

		return xDiff <= GameManager.MAX_MOVE && xDiff == yDiff;
	}

	public override Vector2I[] GetMovementOptions(Vector2I position) {
		Vector2I[] totalMoves = new Vector2I[4 * GameManager.MAX_MOVE];

		for (int i = 0, c = 0; i < totalMoves.Length; i += 4, c++) {
			totalMoves[i + 0] = position + new Vector2I(c, c);
			totalMoves[i + 1] = position + new Vector2I(-c, c);
			totalMoves[i + 2] = position + new Vector2I(c, -c);
			totalMoves[i + 3] = position + new Vector2I(-c, -c);
		}

		return totalMoves;
	}
}

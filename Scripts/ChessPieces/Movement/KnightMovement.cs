using Godot;
using System;

public partial class KnightMovement : ChessMovement {
	public override bool CanMoveTo(Vector2I from, Vector2I to) {
		int xDiff = Mathf.Abs(from.X - to.X);
		int yDiff = Mathf.Abs(to.Y - from.Y);

		return (xDiff == 2 && yDiff == 1) || (xDiff == 1 && yDiff == 2);
	}

	public override Vector2I[] GetMovementOptions(Vector2I position) {

		return new Vector2I[] {
			position + new Vector2I(2, 1),
			position + new Vector2I(2, -1),
			position + new Vector2I(-2, 1),
			position + new Vector2I(-2, -1),
			position + new Vector2I(1, 2),
			position + new Vector2I(1, -2),
			position + new Vector2I(-1, 2),
			position + new Vector2I(-1, -2),
		};

	}
}

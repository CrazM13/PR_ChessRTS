using Godot;
using System;

public partial class PawnMovement : ChessMovement {

	public override bool CanMoveTo(Vector2I from, Vector2I to) {
		if (Mathf.Abs(from.X - to.X) == 1 && (Mathf.Abs(from.Y - to.Y) == 0)) {
			return true;
		} else if ((Mathf.Abs(from.X - to.X) == 0) && (Mathf.Abs(from.Y - to.Y) == 1)) {
			return true;
		}

		return false;
	}

	public override Vector2I[] GetMovementOptions(Vector2I position) {
		return new Vector2I[] {
			position + new Vector2I(0, 1),
			position + new Vector2I(0, -1),
			position + new Vector2I(1, 0),
			position + new Vector2I(-1, 0)
		};
	}
}

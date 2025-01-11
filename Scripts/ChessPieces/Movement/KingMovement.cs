using Godot;
using System;

public partial class KingMovement : ChessMovement {
	public override bool CanMoveTo(Vector2I from, Vector2I to) {
		if (from == to) return false;
		return Mathf.Abs(from.X - to.X) <= 1 && Mathf.Abs(from.Y - to.Y) <= 1;
	}

	public override Vector2I[] GetMovementOptions(Vector2I position) {
		return new Vector2I[] {
			position + new Vector2I(0, 1),
			position + new Vector2I(0, -1),
			position + new Vector2I(1, 1),
			position + new Vector2I(1, -1),
			position + new Vector2I(1, 0),
			position + new Vector2I(-1, 1),
			position + new Vector2I(-1, -1),
			position + new Vector2I(-1, 0),
		};
	}
}

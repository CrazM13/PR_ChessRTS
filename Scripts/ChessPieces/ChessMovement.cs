using Godot;
using System;

public abstract class ChessMovement {

	public abstract Vector2I[] GetMovementOptions(Vector2I position);
	public abstract bool CanMoveTo(Vector2I from, Vector2I to);

}

using Godot;
using System;

public partial class GameManager : Node {

	public const float SQUARE_SIZE = 16;
	public const int MAX_MOVE = 8;
	public const float LOCK_TIME = 1;

	public static Vector2I GlobalToBoard(Vector2 globalPosition) {
		int x = Mathf.FloorToInt(globalPosition.X / SQUARE_SIZE);
		int y = Mathf.FloorToInt(globalPosition.Y / SQUARE_SIZE);

		return new Vector2I(x, y);
	}

	public static Vector2 BoardToGlobal(Vector2I boardPosition) {
		float x = (boardPosition.X * SQUARE_SIZE) + (0.5f * SQUARE_SIZE);
		float y = (boardPosition.Y * SQUARE_SIZE) + (0.5f * SQUARE_SIZE);

		return new Vector2(x, y);
	}

}

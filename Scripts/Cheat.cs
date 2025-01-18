using Godot;
using System;

public partial class Cheat : Node {

	public override void _Process(double delta) {
		base._Process(delta);

		if (Input.IsKeyPressed(Key.Key1)) {
			GameManager.Instance.SetNextSpawn(Teams.WHITE, PieceTypes.KNIGHT);
		}

		if (Input.IsKeyPressed(Key.Key2)) {
			GameManager.Instance.SetNextSpawn(Teams.WHITE, PieceTypes.BISHOP);
		}

		if (Input.IsKeyPressed(Key.Key3)) {
			GameManager.Instance.SetNextSpawn(Teams.WHITE, PieceTypes.ROOK);
		}

		if (Input.IsKeyPressed(Key.Key4)) {
			GameManager.Instance.SetNextSpawn(Teams.WHITE, PieceTypes.QUEEN);
		}

	}

}

using Godot;
using System;

public partial class OpponentAI : Node {

	[Export] private float thinkInterval;

	private float timeUntilAction = 0;

	private RandomNumberGenerator rng = new RandomNumberGenerator();

	public override void _Ready() {
		base._Ready();
		timeUntilAction = thinkInterval;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		timeUntilAction -= (float) delta;
		if (timeUntilAction <= 0) {
			timeUntilAction += thinkInterval;

			ChessPiece[] pieces = GameManager.Instance.GetAllPieces();

			int selected = rng.RandiRange(0, pieces.Length - 1);
			if (pieces[selected].Team != Teams.WHITE) {
				pieces[selected].MoveRandomly();
			}

		}

	}

}

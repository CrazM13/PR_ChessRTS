using Godot;
using System;

public partial class TimerDisplay : Node2D {

	public float Duration { get; set; } = 1f;

	private float timeRemaining = 0;

	public override void _Ready() {
		base._Ready();

		timeRemaining = Duration;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		timeRemaining -= (float) delta;

		if (timeRemaining < 0) {
			QueueFree();
		} else {
			QueueRedraw();
		}
	}

	public override void _Draw() {
		base._Draw();

		float start = Mathf.Pi * 1.5f;
		float end = Mathf.Pi * 2;

		DrawArc(Position, GameManager.SQUARE_SIZE * 0.25f, start, end + start, 16, Colors.Gray, GameManager.SQUARE_SIZE * 0.125f);
		DrawArc(Position, GameManager.SQUARE_SIZE * 0.25f, start, ((timeRemaining / Duration) * (end)) + start, 16, Colors.White, GameManager.SQUARE_SIZE * 0.125f);

	}

}

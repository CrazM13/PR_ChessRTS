using Godot;
using System;

public partial class GameAudio : Node {

	[Export] private AudioStreamPlayer2D[] players;
	private int head = 0;

	public override void _Ready() {
		base._Ready();

		GameManager.Instance.Audio = this;
	}

	public override void _ExitTree() {
		base._ExitTree();

		if (GameManager.Instance.Audio == this) GameManager.Instance.Audio = null;
	}

	public void PlayAt(Vector2I boardPosition) {
		players[head].Stop();
		players[head].GlobalPosition = GameManager.BoardToGlobal(boardPosition);
		players[head].Play();

		head = (head + 1) % players.Length;
	}

}

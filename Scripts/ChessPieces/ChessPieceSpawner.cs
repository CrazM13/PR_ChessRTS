using Godot;
using System;

public partial class ChessPieceSpawner : Node2D {

	private ChessPiece king;
	[Export] private float spawnInterval = 10;
	[Export] private Teams team;

	[Export] public ChessPieceSet chessPieceSet;

	private float timeUntilSpawn;

	public override void _Ready() {
		base._Ready();

		chessPieceSet.Init();

		king = chessPieceSet.Spawn(PieceTypes.KING, GameManager.GlobalToBoard(this.GlobalPosition));
		king.Team = team;
		AddChild(king);
		timeUntilSpawn = spawnInterval;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		timeUntilSpawn -= (float) delta;
		if (timeUntilSpawn <= 0) {
			
			if (IsInstanceValid(king)) {
				AttemptSpawn();

				timeUntilSpawn = spawnInterval;
			} else {
				QueueFree();
			}

			
		}

	}

	private void AttemptSpawn() {
		Vector2I[] positions = king.Movement.GetMovementOptions(king);

		for (int i = 0; i < positions.Length; i++) {
			if (GameManager.Instance.GetPiece(positions[i]) == null) {
				Spawn(positions[i]);
				break;
			}
		}

	}

	private void Spawn(Vector2I position) {
		ChessPiece newPiece = chessPieceSet.Spawn(GameManager.Instance.GetNextSpawn(team), position);

		if (newPiece != null) {
			newPiece.Team = team;
			AddSibling(newPiece);
		}

		GameManager.Instance.SetNextSpawn(team, PieceTypes.PAWN);
	}

}

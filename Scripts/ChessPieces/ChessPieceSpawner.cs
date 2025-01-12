using Godot;
using System;

public partial class ChessPieceSpawner : Node2D {

	private ChessPiece king;
	[Export] private float spawnInterval = 10;
	[Export] private Teams team;

	[ExportGroup("Sprite Paths")]
	[Export] private string kingSprite;
	[Export] private string queenSprite;
	[Export] private string rookSprite;
	[Export] private string bishopSprite;
	[Export] private string knightSprite;
	[Export] private string pawnSprite;

	private float timeUntilSpawn;

	private ChessPieceBuilder queenBuilder;
	private ChessPieceBuilder rookBuilder;
	private ChessPieceBuilder bishopBuilder;
	private ChessPieceBuilder knightBuilder;
	private ChessPieceBuilder pawnBuilder;
	

	public override void _Ready() {
		base._Ready();

		queenBuilder = new ChessPieceBuilder(pawnSprite, new QueenMovement());
		rookBuilder = new ChessPieceBuilder(pawnSprite, new RookMovement());
		bishopBuilder = new ChessPieceBuilder(pawnSprite, new BishopMovement());
		knightBuilder = new ChessPieceBuilder(pawnSprite, new KnightMovement());
		pawnBuilder = new ChessPieceBuilder(pawnSprite, new PawnMovement());

		king = new ChessPieceBuilder(kingSprite, new KingMovement()).Build(GameManager.GlobalToBoard(this.GlobalPosition));
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
		ChessPiece newPiece = null;

		switch (GameManager.Instance.GetNextSpawn(team)) {
			case PieceTypes.PAWN:
				newPiece = pawnBuilder.Build(position);
				break;
			case PieceTypes.KNIGHT:
				newPiece = knightBuilder.Build(position);
				break;
			case PieceTypes.BISHOP:
				newPiece = bishopBuilder.Build(position);
				break;
			case PieceTypes.ROOK:
				newPiece = rookBuilder.Build(position);
				break;
			case PieceTypes.QUEEN:
				newPiece = queenBuilder.Build(position);
				break;
		}

		if (newPiece != null) {
			newPiece.Team = team;
			AddSibling(newPiece);
		}
		
	}

}

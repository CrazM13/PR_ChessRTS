using Godot;
using System;

public class ChessPieceBuilder {

	public Texture2D Sprite { get; set; }
	public ChessMovement MovementType { get; set; }
	public PieceTypes Reward { get; set; }

	public ChessPieceBuilder(string spritePath, ChessMovement movementType) {
		this.Sprite = ResourceLoader.Load<Texture2D>(spritePath);
		this.MovementType = movementType;
	}

	public ChessPieceBuilder AddReward(PieceTypes reward) {
		this.Reward = reward;

		return this;
	}

	public ChessPiece Build(Vector2I position) {
		ChessPiece piece = new ChessPiece();
		piece.BoardPosition = position;
		piece.GlobalPosition = GameManager.BoardToGlobal(piece.BoardPosition);
		piece.Texture = this.Sprite;
		piece.Movement = this.MovementType;
		piece.Reward = this.Reward;
		
		return piece;
	}

}

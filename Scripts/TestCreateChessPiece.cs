using Godot;
using System;

public partial class TestCreateChessPiece : Node {

	public override void _Ready() {
		base._Ready();

		ChessPieceBuilder chessPieceBuilder = new ChessPieceBuilder("res://Assets/Textures/TestPiece.png", new RookMovement());
		AddChild(chessPieceBuilder.Build(Vector2I.Zero));
	}

}

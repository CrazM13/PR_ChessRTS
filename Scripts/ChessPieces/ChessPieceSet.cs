using Godot;
using System;

[GlobalClass]
public partial class ChessPieceSet : Resource {

	[ExportGroup("Sprite Paths")]
	[Export] private string kingSprite;
	[Export] private string kingSpriteOverlay;
	[Export] private string queenSprite;
	[Export] private string queenSpriteOverlay;
	[Export] private string rookSprite;
	[Export] private string rookSpriteOverlay;
	[Export] private string bishopSprite;
	[Export] private string bishopSpriteOverlay;
	[Export] private string knightSprite;
	[Export] private string knightSpriteOverlay;
	[Export] private string pawnSprite;
	[Export] private string pawnSpriteOverlay;

	private ChessPieceBuilder kingBuilder;
	private ChessPieceBuilder queenBuilder;
	private ChessPieceBuilder rookBuilder;
	private ChessPieceBuilder bishopBuilder;
	private ChessPieceBuilder knightBuilder;
	private ChessPieceBuilder pawnBuilder;

	private bool isInitialized = false;
	public void Init() {
		if (isInitialized) return;

		kingBuilder = new ChessPieceBuilder(kingSprite, kingSpriteOverlay).AddMovement(new KingMovement()).AddReward(PieceTypes.QUEEN);
		queenBuilder = new ChessPieceBuilder(queenSprite, queenSpriteOverlay).AddMovement(new QueenMovement()).AddReward(PieceTypes.QUEEN);
		rookBuilder = new ChessPieceBuilder(rookSprite, rookSpriteOverlay).AddMovement(new RookMovement()).AddReward(PieceTypes.QUEEN);
		bishopBuilder = new ChessPieceBuilder(bishopSprite, bishopSpriteOverlay).AddMovement(new BishopMovement()).AddReward(PieceTypes.ROOK);
		knightBuilder = new ChessPieceBuilder(knightSprite, knightSpriteOverlay).AddMovement(new KnightMovement()).AddReward(PieceTypes.BISHOP);
		pawnBuilder = new ChessPieceBuilder(pawnSprite, pawnSpriteOverlay).AddMovement(new PawnMovement()).AddReward(PieceTypes.KNIGHT);

		isInitialized = true;
	}

	public ChessPiece Spawn(PieceTypes piece, Vector2I position) {
		ChessPiece newPiece = null;

		switch (piece) {
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
			case PieceTypes.KING:
				newPiece = kingBuilder.Build(position);
				break;
		}

		return newPiece;
	}

}

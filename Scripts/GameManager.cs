using Godot;
using System;
using System.Collections.Generic;

public class GameManager {

	public const float SQUARE_SIZE = 16;
	public const int MAX_MOVE = 8;
	public const float LOCK_TIME = 1;

	private static GameManager instance = null;
	public static GameManager Instance {
		get {
			instance ??= new GameManager();
			return instance;
		}
	}

	public MovementVisualization Visualizer { get; set; }

	#region Piece Management
	private List<ChessPiece> pieces = new List<ChessPiece>();
	private Dictionary<Teams, PieceTypes> nextSpawns = new Dictionary<Teams, PieceTypes>();

	public void Register(ChessPiece piece) {
		pieces.Add(piece);
	}

	public void Deregister(ChessPiece piece) {
		pieces.Remove(piece);
	}

	public ChessPiece GetPiece(Vector2I position) {
		return pieces.Find((p) => p.BoardPosition == position);
	}

	public ChessPiece[] GetAllPieces() {
		return pieces.ToArray();
	}

	public PieceTypes GetNextSpawn(Teams team) {
		if (!nextSpawns.ContainsKey(team)) {
			nextSpawns.Add(team, PieceTypes.PAWN);
		}

		return nextSpawns[team];
	}

	public void SetNextSpawn(Teams team, PieceTypes piece) {
		if (!nextSpawns.ContainsKey(team)) {
			nextSpawns.Add(team, piece);
		} else {
			nextSpawns[team] = piece;
		}
	}
	#endregion



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

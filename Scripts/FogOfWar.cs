using Godot;
using System;
using System.Collections.Generic;

public class FogOfWar {

	private static FogOfWar instance;
	public static FogOfWar Instance {
		get {
			instance ??= new FogOfWar();
			return instance;
		}
	}

	private List<Vector2I> visabilityPoints;

	private FogOfWar() {
		visabilityPoints = new List<Vector2I>();
	}

	public void UpdateVisabilityPoints(Teams team) {
		visabilityPoints.Clear();
		ChessPiece[] pieces = GameManager.Instance.GetAllPieces();

		foreach (ChessPiece piece in pieces) {
			if (piece.Team == team) {
				visabilityPoints.Add(piece.BoardPosition);
			}
		}
	}

	public float GetVisability(Vector2I position) {

		float nearestDistance = float.MaxValue;
		int index = -1;

		for (int i = 0; i < visabilityPoints.Count; i++) {
			float newDist = visabilityPoints[i].DistanceSquaredTo(position);
			if (newDist < nearestDistance) {
				nearestDistance = newDist;
				index = i;
			}
		}

		if (index == -1) {
			return 0;
		}

		if (nearestDistance > GameManager.SIGHT_DISTANCE) {
			return 0;
		} else {
			return 1 - (nearestDistance / GameManager.SIGHT_DISTANCE);
		}

	}

}

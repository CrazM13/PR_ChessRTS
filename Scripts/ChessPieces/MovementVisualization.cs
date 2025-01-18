using Godot;
using System;
using System.Collections.Generic;

public partial class MovementVisualization : Node2D {

	[Export] public Color movementSpaceColour = Colors.Green;
	[Export] public Color captureSpaceColour = Colors.Red;

	public enum Visual {
		NONE,
		MOVEMENT,
		CAPTURE
	}

	private Dictionary<Vector2I, Visual> visualData = new Dictionary<Vector2I, Visual>();

	public override void _Ready() {
		base._Ready();

		GameManager.Instance.Visualizer = this;
	}

	public override void _ExitTree() {
		base._ExitTree();

		if (GameManager.Instance.Visualizer == this) GameManager.Instance.Visualizer = null;
	}

	public void ClearVisuals() {
		visualData.Clear();

		QueueRedraw();
	}

	public void AddVisual(Vector2I position, Visual visual) {
		visualData.Add(position, visual);

		QueueRedraw();
	}

	public override void _Draw() {
		base._Draw();

		foreach (KeyValuePair<Vector2I, Visual> item in visualData) {
			switch (item.Value) {
				case Visual.MOVEMENT:
					DrawMovement(item.Key);
					break;
				case Visual.CAPTURE:
					DrawCapture(item.Key);
					break;
			}
		}

	}

	private void DrawMovement(Vector2I position) {
		DrawSquare(position, movementSpaceColour);
	}

	private void DrawCapture(Vector2I position) {
		DrawSquare(position, captureSpaceColour);
	}

	private void DrawSquare(Vector2I position, Color color) {

		Vector2 halfSquare = new Vector2(GameManager.SQUARE_SIZE, GameManager.SQUARE_SIZE) * 0.5f;
		Vector2 globalPosition = GameManager.BoardToGlobal(position);

		DrawRect(new Rect2(globalPosition - halfSquare, halfSquare * 2f), color, true);
	}

}

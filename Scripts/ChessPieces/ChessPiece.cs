using Godot;
using System;

public partial class ChessPiece : Sprite2D {

	public Vector2I BoardPosition { get; set; }
	public ChessMovement Movement { get; set; }

	private bool isMoving = false;
	private bool isLocked = false;

	public override void _Process(double delta) {
		base._Process(delta);

		if (isMoving) {
			this.GlobalPosition = GetGlobalMousePosition();

			if (!Input.IsMouseButtonPressed(MouseButton.Left)) {
				Vector2I newPosition = GameManager.GlobalToBoard(this.GlobalPosition);
				if (Movement.CanMoveTo(BoardPosition, newPosition)) {
					BoardPosition = newPosition;

					isLocked = true;
					this.SelfModulate = Colors.Gray;
					GetTree().CreateTimer(GameManager.LOCK_TIME).Timeout += () => {
						isLocked = false;
						this.SelfModulate = Colors.White;
					};
				}

				this.Offset = Vector2.Zero;
				GlobalPosition = GameManager.BoardToGlobal(BoardPosition);
				isMoving = false;
			}
		}

	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);

		if (@event is InputEventMouseButton mouseEvent) {
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed) {
				Vector2 mousePos = GetLocalMousePosition();
				if (IsPixelOpaque(mousePos)) {
					if (isLocked) {
						// TODO
					} else {
						isMoving = true;
						this.Offset = -mousePos;
						this.GlobalPosition = GetGlobalMousePosition();
					}
				}
			}
		}

	}

}

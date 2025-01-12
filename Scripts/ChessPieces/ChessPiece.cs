using Godot;
using System;

public partial class ChessPiece : Sprite2D {

	public Vector2I BoardPosition { get; set; }
	public ChessMovement Movement { get; set; }
	public Teams Team { get; set; }

	private bool isMoving = false;
	private bool isLocked = false;

	public override void _Ready() {
		base._Ready();

		this.SelfModulate = TeamUtils.GetTeamColour(Team);
		GameManager.Instance.Register(this);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		AttemptContinueDrag();

	}

	public override void _Input(InputEvent @event) {
		base._Input(@event);

		if (@event is InputEventMouseButton mouseEvent) {
			AttemptStartDrag(mouseEvent);
		}

	}

	private void AttemptStartDrag(InputEventMouseButton mouseEvent) {
		if (Team == Teams.WHITE) {
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed) {
				Vector2 mousePos = GetLocalMousePosition();
				if (mousePos.DistanceSquaredTo(Vector2.Zero) < GameManager.SQUARE_SIZE * GameManager.SQUARE_SIZE * 0.25f) {
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

	private void AttemptContinueDrag() {
		if (Team == Teams.WHITE) {
			if (isMoving) {
				this.GlobalPosition = GetGlobalMousePosition();

				if (!Input.IsMouseButtonPressed(MouseButton.Left)) {
					Vector2I newPosition = GameManager.GlobalToBoard(this.GlobalPosition);
					if (Movement.CanMoveTo(this, newPosition)) {

						ChessPiece taken = GameManager.Instance.GetPiece(newPosition);
						if (taken != null) {
							GameManager.Instance.Deregister(taken);
							taken.QueueFree();
						}

						BoardPosition = newPosition;

						isLocked = true;
						this.SelfModulate = Colors.Gray.Lerp(TeamUtils.GetTeamColour(Team), 0.5f);
						GetTree().CreateTimer(GameManager.LOCK_TIME).Timeout += () => {
							isLocked = false;
							this.SelfModulate = TeamUtils.GetTeamColour(Team);
						};
					}

					this.Offset = Vector2.Zero;
					GlobalPosition = GameManager.BoardToGlobal(BoardPosition);
					isMoving = false;
				}
			}
		}
	}

}

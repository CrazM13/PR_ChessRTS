using Godot;
using System;

public partial class ChessPiece : Sprite2D {

	public Vector2I BoardPosition { get; set; }
	public ChessMovement Movement { get; set; }
	public Teams Team { get; set; }
	public PieceTypes Reward { get; set; }

	public Texture2D Overlay { get; set; }

	private bool isMoving = false;
	private bool isLocked = false;

	private Vector2I? moveTo = null;

	public override void _Ready() {
		base._Ready();

		GameManager.Instance.Register(this);
		GlobalPosition = GameManager.BoardToGlobal(BoardPosition);

		if (Team == Teams.WHITE) {
			FogOfWar.Instance.UpdateVisabilityPoints(Teams.WHITE);
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		AttemptContinueDrag();

		if (moveTo.HasValue) {
			Vector2 globalMoveTo = GameManager.BoardToGlobal(moveTo.Value);
			this.GlobalPosition = this.GlobalPosition.MoveToward(globalMoveTo, (float) delta * 100);
			if (this.GlobalPosition == globalMoveTo) {
				GameManager.Instance.Audio.PlayAt(moveTo.Value);
				moveTo = null;
			}
		}

		float alpha = FogOfWar.Instance.GetVisability(BoardPosition);
		this.Modulate = new Color(this.Modulate.R, this.Modulate.G, this.Modulate.B, alpha);
	}

	public void MoveRandomly() {
		if (isLocked) return;
		RandomNumberGenerator rng = new RandomNumberGenerator();

		Vector2I[] moves = Movement.GetMovementOptions(this);

		int selected = rng.RandiRange(0, moves.Length - 1);

		ChessPiece taken = GameManager.Instance.GetPiece(moves[selected]);
		if (taken != null) {
			TakePiece(taken);
		}

		BoardPosition = moves[selected];
		moveTo = BoardPosition;

		LockPiece();
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

				GameManager.Instance.Visualizer.ClearVisuals();

				if (!Input.IsMouseButtonPressed(MouseButton.Left)) {
					
					Vector2I newPosition = GameManager.GlobalToBoard(this.GlobalPosition);
					if (Movement.CanMoveTo(this, newPosition)) {
						GameManager.Instance.Audio.PlayAt(newPosition);

						ChessPiece taken = GameManager.Instance.GetPiece(newPosition);
						if (taken != null) {
							TakePiece(taken);
						}

						BoardPosition = newPosition;
						LockPiece();

						FogOfWar.Instance.UpdateVisabilityPoints(Team);
					}

					this.Offset = Vector2.Zero;
					GlobalPosition = GameManager.BoardToGlobal(BoardPosition);
					isMoving = false;
				} else {
					DisplayMovement();
				}
			}
		}
	}

	private void TakePiece(ChessPiece taken) {
		PieceTypes reward = taken.Reward;
		PieceTypes nextSpawn = GameManager.Instance.GetNextSpawn(Team);
		if (nextSpawn < reward) {
			GameManager.Instance.SetNextSpawn(Team, reward);
		}

		GameManager.Instance.Deregister(taken);
		taken.QueueFree();
	}

	private void DisplayMovement() {
		Vector2I[] movementOptipons = Movement.GetMovementOptions(this);
		for (int i = 0; i < movementOptipons.Length; i++) {
			Vector2I position = movementOptipons[i];
			if (ChessMovement.IsTaken(position)) {
				GameManager.Instance.Visualizer.AddVisual(position, MovementVisualization.Visual.CAPTURE);
			} else {
				GameManager.Instance.Visualizer.AddVisual(position, MovementVisualization.Visual.MOVEMENT);
			}
		}
	}

	public override void _Draw() {
		base._Draw();

		DrawTexture(Overlay, this.GetRect().Position, TeamUtils.GetTeamColour(Team));
	}

	private void LockPiece() {
		isLocked = true;
		this.SelfModulate = Colors.Gray;
		AddChild(new TimerDisplay() { Duration = GameManager.LOCK_TIME, SelfModulate = TeamUtils.GetTeamColour(Team) });
		GetTree().CreateTimer(GameManager.LOCK_TIME).Timeout += () => {
			isLocked = false;
			this.SelfModulate = Colors.White;
		};
	}

}

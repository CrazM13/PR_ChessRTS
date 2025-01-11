using Godot;
using System;

public partial class RTSCamera : Node2D {

	[Export] private Camera2D camera;

	private bool isMiddleMouseDown = false;
	private Vector2 fixedMousePos = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {

		

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {

		if (!isMiddleMouseDown) {
			if (Input.IsMouseButtonPressed(MouseButton.Middle)) {
				isMiddleMouseDown = true;
				fixedMousePos = GetGlobalMousePosition();
			}
		} else {
			if (Input.IsMouseButtonPressed(MouseButton.Middle)) {
				Vector2 newMousePos = GetGlobalMousePosition();
				Vector2 difference = newMousePos - fixedMousePos;
				this.GlobalPosition -= difference;
			} else {
				isMiddleMouseDown = false;
			}
		}

		if (Input.IsActionJustReleased("zoom_in")) {
			camera.Zoom += Vector2.One;
		} else if (Input.IsActionJustReleased("zoom_out")) {
			Vector2 newZoom = camera.Zoom - Vector2.One;
			if (newZoom.X < 1) {
				newZoom = Vector2.One;
			}
			camera.Zoom = newZoom;
		}

	}
}

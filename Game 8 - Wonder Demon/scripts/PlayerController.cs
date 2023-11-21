using Godot;
using System;

public class PlayerController : KinematicBody2D
{
	private const float Speed = 200f; // Adjust the movement speed as needed
	private Vector2 velocity = Vector2.Zero;
	private Vector2 gravity = new Vector2(0, 800); // Adjust the gravity as needed

	private enum PlayerState
	{
		Platformer,
		WallWalking
	}

	private PlayerState currentState = PlayerState.Platformer;

	public override void _Process(float delta)
	{
		switch (currentState)
		{
			case PlayerState.Platformer:
				ProcessPlatformer(delta);
				break;
			case PlayerState.WallWalking:
				ProcessWallWalking(delta);
				break;
		}
	}

	private void ProcessPlatformer(float delta)
	{
		velocity.y += gravity.y * delta; // Apply gravity

		if (Input.IsActionPressed("ui_right"))
			velocity.x = Speed;
		else if (Input.IsActionPressed("ui_left"))
			velocity.x = -Speed;
		else
			velocity.x = 0;

		if (IsOnFloor() && Input.IsActionJustPressed("ui_up"))
			velocity.y = -Mathf.Sqrt(2 * gravity.y * 100); // Jumping impulse

		velocity = MoveAndSlide(velocity, new Vector2(0, -1));

		if (Input.IsActionJustPressed("change_perspective"))
			currentState = PlayerState.WallWalking;
	}

	private void ProcessWallWalking(float delta)
	{
		velocity.x = 0; // Prevent sideways movement while wall walking

		if (Input.IsActionPressed("ui_up"))
			velocity.y = -Speed;
		else if (Input.IsActionPressed("ui_down"))
			velocity.y = Speed;
		else
			velocity.y = 0;

		if (Input.IsActionJustPressed("change_perspective"))
			currentState = PlayerState.Platformer;

		velocity = MoveAndSlide(velocity, new Vector2(1, 0)); // Slide along the wall
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Scancode == (int)KeyList.Escape)
			GetTree().Quit(); // Exit the game on Esc key (for testing)
	}
}

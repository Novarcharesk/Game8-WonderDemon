using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = 980;

	public enum PlayerState
	{
		Platformer,
		WallWalking
	}

	private PlayerState currentState = PlayerState.Platformer;

	// Public property to store a reference to the WonderButton
	public WonderButton WonderButtonReference { get; set; }

	public override void _Ready()
	{
		//currentState = PlayerState.WallWalking;
	}

	public override void _Process(double delta) // Change from _PhysicsProcess to _Process
	{
		if (currentState == PlayerState.Platformer)
		{
			//normal 'left/right' movement
			Vector2 velocity = Velocity;

			// Add the gravity.
			if (!IsOnFloor())
				velocity.Y += gravity * (float)delta;

			// Handle Jump.
			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
				velocity.Y = JumpVelocity;

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
		else if (currentState == PlayerState.WallWalking)
		{
			//wall walking state
			GD.Print("Hello");

			//normal 'left/right' movement
			Vector2 velocity = Velocity;

			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}

			if (direction != Vector2.Zero)
			{
				velocity.Y = direction.Y * Speed;
			}
			else
			{
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
	}

	public void ToggleLayer()
	{
		GD.Print("Works");
		// Switch between collision layers (assuming 1 and 2 are your desired layers)
		if (CollisionLayer == 1)
		{
			CollisionLayer = 2;
			currentState = PlayerState.WallWalking;
		}
		else
		{
			CollisionLayer = 1;
			currentState = PlayerState.Platformer;
		}
	}
}

using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -550.0f;
	public float gravity = 980;

	public enum PlayerState
	{
		Platformer,
		WallWalking
	}

	private PlayerState currentState = PlayerState.Platformer;
	private int maxHealth = 3;
	private int currentHealth;

	public override void _Ready()
	{
		// Initialize health
		currentHealth = maxHealth;
	}

	public override void _Process(double delta)
	{
		// Check if the player is on layer 2
		if (CollisionLayer == 2)
		{
			GD.Print("Player is on Layer 2");
			// You can add any specific actions you want when the player is on layer 2
		}

		// Debug information about collision layers and masks
		//GD.Print("Player CollisionLayer:", CollisionLayer);
		//GD.Print("Player CollisionMask:", CollisionMask);

		// Check for player defeat condition
		if (currentHealth <= 0)
		{
			GD.Print("Player Defeated");
			// You can add any specific actions for player defeat
			return;
		}

		if (currentState == PlayerState.Platformer)
		{
			Vector2 velocity = Velocity;

			if (!IsOnFloor())
				velocity.Y += gravity * (float)delta;

			if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
				velocity.Y = JumpVelocity;

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
			Vector2 velocity = Velocity;

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

	public void ToggleLayer(bool isOnFloor)
	{
		GD.Print("ToggleLayer Called");
		// Switch between collision layers and masks
		if (CollisionLayer == 1 && isOnFloor)
		{
			GD.Print("Switching to WallWalking");
			CollisionLayer = 2;
			CollisionMask = 2; // Set the collision mask to include Layer 2
			currentState = PlayerState.WallWalking;
		}
		else if (CollisionLayer == 2 && !isOnFloor)
		{
			GD.Print("Switching to Platformer");
			CollisionLayer = 1;
			CollisionMask = 1; // Set the collision mask to include Layer 1
			currentState = PlayerState.Platformer;
		}
	}

	public void TakeDamage(int damage)
	{
		GD.Print("Player took damage: ", damage);
		currentHealth -= damage;

		// Check for player defeat condition
		if (currentHealth <= 0)
		{
			GD.Print("Player Defeated");
			// You can add any specific actions for player defeat
		}
	}
}

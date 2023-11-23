using Godot;
using System;

public partial class GameManager : Node
{
	public const int MaxPlayerHealth = 3;
	public int playerHealth = MaxPlayerHealth;

	public bool isGameOver = false;

	public Area2D winArea;
	public CharacterBody2D player;
	public Area2D enemy;
	public float recoilForce = 200.0f;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (isGameOver)
		{
			// Game over logic, e.g., display game over screen or handle game over state
			GD.Print("Game Over");
		}
		else
		{
			// Game logic while the game is active
		}
	}

	private void _on_WinArea_body_entered(Node body)
	{
		if (body is CharacterBody2D)
		{
			// Player entered the win area, trigger win state
			GD.Print("You Win!");
			isGameOver = true;
		}
	}

	private void _on_Enemy_body_entered(Node body)
	{
		if (body is CharacterBody2D)
		{
			// Player collided with the enemy
			GD.Print("Player collided with enemy");

			// Reduce player health
			playerHealth -= 1;
			GD.Print("Player Health: ", playerHealth);

			// Check for game over condition
			if (playerHealth <= 0)
			{
				GD.Print("Game Over - Player Defeated");
				isGameOver = true;
			}
		}
	}
}

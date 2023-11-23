using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	private const float MoveDistance = 100.0f;
	private const float MoveSpeed = 50.0f;

	private Vector2 initialPosition;
	private bool movingRight = true;

	public override void _Ready()
	{
		initialPosition = GlobalPosition;
	}

	// No _Process method in this partial class
}

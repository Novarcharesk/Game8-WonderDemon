using Godot;
using System;

public partial class WonderButton : Node
{
	// Reference to the player node (assign in the Inspector)
	public Player player;

	private void OnBodyEntered(Node2D body)
	{
		// Assuming the player node has a method to toggle its layer
		if (player != null)
		{
			player.ToggleLayer();
		}
		
		GD.Print("Works");
	}
}

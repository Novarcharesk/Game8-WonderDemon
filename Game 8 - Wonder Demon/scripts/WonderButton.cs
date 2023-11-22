using Godot;
using System;

public partial class WonderButton : Node
{
	// Reference to the player node (assign in the Inspector)
	[Export] public bool IsOnFloor;

	
	private void _on_area_2d_body_entered(Node2D body)
	{
		// Check if the entering body is the player
		if (body is Player playerNode)
		{
			// Assuming the player node has a method to toggle its layer
			if (playerNode != null)
			{
				playerNode.ToggleLayer(IsOnFloor);
			}
		}
	}
}

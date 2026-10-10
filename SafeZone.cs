using Godot;
using System;

public partial class SafeZone : Area2D
{
	
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	public override void _Process(double delta)
	{
		
	}
	
	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player")
		{
			var node = GetTree().CurrentScene as Level1;
			node.InSafeArea = true;
		}
	}
	
	private void OnBodyExited(Node2D body)
	{
		if (body.Name == "Player")
		{
			var node = GetTree().CurrentScene as Level1;
			node.InSafeArea = false;
		}
	}
}
